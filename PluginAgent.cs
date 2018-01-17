using System;
using System.Collections.Generic;
using System.Reflection;
using NewRelic.Platform.Sdk;
using NewRelic.Platform.Sdk.Utils;

namespace org.healthwise.newrelic.rabbitmq
{
    class PluginAgent : Agent
    {
        public override string Guid
        {
            get
            {
                return "org.healthwise.newrelic.rabbitmq";
            }
        }

        public override string Version
        {
            get
            {
                return Assembly.GetEntryAssembly().GetName().Version.ToString();
            }
        }

        // Name of Agent
        private string name;

        // RabbitMQ Object
        private RMQ RMQ;

        // Provides logging for Plugin
        private Logger log = Logger.GetLogger(typeof(PluginAgent).Name);

        int ClusterHealth = 1;  // 1 - Healthy, 2 - Warning, 3 - Critical

        /// <summary>
        /// Constructor for Agent Class
        /// Accepts name and other parameters from plugin.json file
        /// </summary>
        /// <param name="name"></param>
        public PluginAgent(string protocol, string name, string host, int port, string username, string password)
        {
            this.name = name;
            this.RMQ = new RMQ(protocol, host, port, username, password);
        }

        /// <summary>
        /// Returns a human-readable string to differentiate different hosts/entities in the New Relic UI
        /// </summary>
        public override string GetAgentName()
        {
            return this.name;
        }

        /// <summary>
        /// This is where logic for fetching and reporting metrics should exist.  
        /// Call off to a REST head, SQL DB, virtually anything you can programmatically 
        /// get metrics from and then call ReportMetric.
        /// </summary>
        public override void PollCycle()
        {
            ClusterHealth = 1; // Default to a 'healthy' cluster state until proven otherwise
            PollNodes();       // Poll Nodes
            PollQueues();      // Poll Queues
            PollConnections(); // Poll Connections
            PollCluster();     // Poll Cluster Details
        }

        private void PollNodes()
        {          
            int num_nodes = 0;          // total number of nodes in the cluster
            int num_running_nodes = 0;  // number of nodes in a 'running' state
            int num_net_partitions = 0; // across all nodes, should NEVER be more than 0.
            List<NodeObject> nodes = new List<NodeObject>();

            //log.Info("Polling nodes..");
            try {
                nodes = RMQ.fetchRMQObject<List<NodeObject>>("/api/nodes");  // Get the Node Objects From the RabbitMQ Cluster.
            }
            catch (Exception e)
            {
                // Then we are unable to get the node information from the cluster // CRITICAL
                ClusterHealth = 3;
                log.Error("Exception Thrown: '{0}'", e.Message);
                log.Error("Stacktrace: '{0}'", e.StackTrace);
                log.Error("Doomsday clock advanced. ({0}) - Unable to fetch node information for the RabbitMQ cluster", ClusterHealth);
            }

            foreach (NodeObject node in nodes)
            {
                float memory_used_percentage = ((float)node.mem_used / node.mem_limit) * 100;
                float fd_used_percentage = ((float)node.fd_used / node.fd_total) * 100;
                float sockets_used_percentage = ((float)node.sockets_used / node.sockets_total) * 100;

                if (!float.IsNaN(memory_used_percentage) && !float.IsNaN(fd_used_percentage) && !float.IsNaN(sockets_used_percentage))
                {
                    log.Info("[Reporting Metric] [nodes/{0}/memory_used_percentage] [{1}]", node.name, memory_used_percentage);
                    ReportMetric("nodes/" + node.name + "/memory_used_percentage", "Percentage", memory_used_percentage);

                    log.Info("[Reporting Metric] [nodes/{0}/disk_free] [{1}]", node.name, node.disk_free);
                    ReportMetric("nodes/" + node.name + "/disk_free", "Gigabytes", (float)node.disk_free);

                    log.Info("[Reporting Metric] [nodes/{0}/fd_used_percentage] [{1}]", node.name, fd_used_percentage);
                    ReportMetric("nodes/" + node.name + "/fd_used_percentage", "Percentage", fd_used_percentage);

                    log.Info("[Reporting Metric] [nodes/{0}/sockets_used_percentage] [{1}]", node.name, sockets_used_percentage);
                    ReportMetric("nodes/" + node.name + "/sockets_used_percentage", "Percentage", sockets_used_percentage);
                }

                // Aggregate running node total
                num_nodes++;
                if (node.running == true)
                {
                    num_running_nodes++;
                }

                if (node.partitions != null)
                {
                    num_net_partitions += node.partitions.Count;
                }

                log.Info("[Reporting Metric] [nodes/{0}/mem_alarm] [{1}]", node.name, node.mem_alarm);               
                ReportMetric("nodes/" + node.name + "/mem_alarm", "count", node.mem_alarm ? 1 : 0);

                log.Info("[Reporting Metric] [nodes/{0}/disk_free_alarm] [{1}]", node.name, node.disk_free_alarm);
                ReportMetric("nodes/" + node.name + "/disk_free_alarm", "count", node.disk_free_alarm ? 1 : 0);
            }

            // The node count is zero.  This means that the cluster was not accessible. // CRITICAL
            if (num_nodes == 0 && ClusterHealth < 3)
            {
                ClusterHealth = 3;  // Unknown State (We are receiving a node count of zero)
                log.Error("Doomsday clock advanced. ({0}) - Unknown State (We are receiving a node count of zero)", ClusterHealth);
            }
            
            // Then we also have no healthy nodes.  Only report this is we are not in a CRTICAL state. // CRITICAL
            if (num_running_nodes == 0 && ClusterHealth < 3)
            {
                ClusterHealth = 3;  // Critical (we have no healthy nodes)
                log.Error("Doomsday clock advanced. ({0}) - Critical (we have no healthy nodes)", ClusterHealth);
            }

            // We were able to connect to the cluster, but are running in a degraded state. // WARNING
            if (num_running_nodes < num_nodes && ClusterHealth < 2)
            {
                ClusterHealth = 2; // Warning (we've lost nodes)
                log.Error("Doomsday clock advanced. ({0}) - Warning (we've lost nodes)", ClusterHealth);
            }
            
            // Then we have network partions // CRITICAL
            if (num_net_partitions != 0)
            {
                ClusterHealth = 3;  // We have network partitions, oh no..
                log.Error("Doomsday clock advanced. ({0}) - Network paritions seen: ({1})", ClusterHealth, num_net_partitions);
            }

            log.Info("[Reporting Metric] [global/nodes/total] [{0}]", num_nodes);
            log.Info("[Reporting Metric] [global/nodes/failed] [{0}]", num_nodes - num_running_nodes);
            log.Info("[Reporting Metric] [global/nodes/partitions] [{0}]", num_net_partitions);

            // Report our metrics to New Relic
            ReportMetric("global/nodes/total", "count", num_nodes);
            ReportMetric("global/nodes/failed", "count", num_nodes - num_running_nodes);
            ReportMetric("global/nodes/partitions", "count", num_net_partitions);
        }

        private void PollQueues()
        {
            int num_queues = 0;
            double deliver_get_details = 0;
            double publish_details = 0;
            int down_nodes = 0;
            List<QueueObject> queues = new List<QueueObject>();

            //log.Info("Polling queues..");
            try
            {                
                queues = RMQ.fetchRMQObject<List<QueueObject>>("/api/queues");  // Get the Node Objects From the RabbitMQ Cluster.
            }           
            catch (Exception e)
            {
                // Then we are unable to get the node information from the cluster // CRITICAL
                ClusterHealth = 3;
                log.Error("Exception Thrown: '{0}'", e.Message);
                log.Error("Stacktrace: '{0}'", e.StackTrace);
                log.Error("Doomsday clock advanced. ({0}) - Unable to fetch queue information for the RabbitMQ cluster", ClusterHealth);
            }

            foreach (var queue in queues)
            {
                num_queues++;
                String encoded_vhost = System.Uri.EscapeDataString(queue.vhost);  // URL Encode the vhost

                if (queue.state == "down")
                {
                    down_nodes++;
                }
                else {
                    QueueObject detailedqueue = RMQ.fetchRMQObject<QueueObject>("/api/queues/" + encoded_vhost + "/" + queue.name);
                    if (detailedqueue != null)
                    {
                        foreach (var deliveries_collection_item in detailedqueue.deliveries)
                        {
                            if (deliveries_collection_item != null)
                            {
                                var myDetailsStats = deliveries_collection_item.stats;
                                deliver_get_details += myDetailsStats.deliver_get_details.rate;
                            }
                        }
                        foreach (var incoming_collection_item in detailedqueue.incoming)
                        {
                            if (incoming_collection_item != null)
                            {
                                var myDetailsStats = incoming_collection_item.stats;
                                publish_details += myDetailsStats.publish_details.rate;
                            }
                        }
                        log.Info("[Reporting Metric] [queues/{0}/{1}/deliveries/stats/deliver_get_details/rate] [{2}]", queue.vhost, queue.name, deliver_get_details);
                        log.Info("[Reporting Metric] [queues/{0}/{1}/incoming/stats/publish_details/rate] [{2}]", queue.vhost, queue.name, publish_details);
                        ReportMetric("queues/" + queue.name + "/message/delivery/rate", "count", (float)deliver_get_details);
                        ReportMetric("queues/" + queue.name + "/message/incoming/rate", "count", (float)publish_details);
                    }
                    log.Info("[Reporting Metric] [queues/{0}/messages] [{1}]", queue.name, queue.messages);
                    ReportMetric("queues/" + queue.name + "/messages", "count", queue.messages);

                    deliver_get_details = 0;
                    publish_details = 0;
                }
            }

            log.Info("[Reporting Metric] [queues/down] [{0}]", down_nodes);
            ReportMetric("queues/down", "count", down_nodes);

            log.Info("[Reporting Metric] [queues/total] [{0}]", num_queues);
            ReportMetric("queues/total", "count", num_queues);
        }

        private void PollConnections()
        {
            int num_connections = 0;
            // log.Info("Polling connections..");
            try {
                // Get the Connection objects From the RabbitMQ Cluster.
                List<ConnectionObject> connections = RMQ.fetchRMQObject<List<ConnectionObject>>("/api/connections");
                foreach (var connection in connections)
                {
                    num_connections++;
                }
            }
            catch (Exception e)
            {
                log.Error("Exception Thrown: '{0}'", e.Message);
                log.Error("Stacktrace: '{0}'", e.StackTrace);
                ClusterHealth = 3;  // 1 - Healthy, 2 - Warning, 3 - Critical
                log.Error("Doomsday clock advanced. ({0}) - Unable to fetch connection information for the RabbitMQ cluster", ClusterHealth);
            }
            log.Info("[Reporting Metric] [connections/total] [{0}]", num_connections);
            ReportMetric("connections/total", "count", num_connections);
        }

        private void PollCluster()
        {
            try
            {
                // Get the Overview object from the RabbitMQ Cluster
                string MyClusterName = RMQ.fetchRMQObject<ClusterName>("/api/cluster-name").name;
                log.Info("RabbitMQ Cluster Name: ({0})", MyClusterName);
            }
            catch (Exception e)
            {
                ClusterHealth = 3;  // 0 - Healthy, 1 - Unknown, 2 - Warning, 3 - Critical
                log.Error("Exception Thrown: '{0}'", e.Message);
                log.Error("Stacktrace: '{0}'", e.StackTrace);
                log.Error("Doomsday clock advanced. ({0}) - Unable to fetch cluster name for the RabbitMQ cluster", ClusterHealth);
            }
            log.Info("[Reporting Metric] [global/health] [{0}]", ClusterHealth);
            ReportMetric("global/health", "count", ClusterHealth);
        }
    }
}
