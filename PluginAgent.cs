using System;
using System.Collections.Generic;
using System.Reflection;
using NewRelic.Platform.Sdk;
using NewRelic.Platform.Sdk.Utils;
using NewRelic.Platform.Sdk.Processors;

namespace Org.Healthwise.NewRelic.RabbitMQ
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

        // Create Dictionary of EpochProcessors to track rate over time for unknown number of items
        private Dictionary<string, IProcessor> processors = new Dictionary<string, IProcessor>();

        // Provides logging for Plugin
        private Logger log = Logger.GetLogger(typeof(PluginAgent).Name);

        int ClusterHealth = 1;  // 0 - Healthy, 1 - Unknown, 2 - Warning, 3 - Critical


        /// <summary>
        /// Constructor for Agent Class
        /// Accepts name and other parameters from plugin.json file
        /// </summary>
        /// <param name="name"></param>
        public PluginAgent(string name, string host, int port, string username, string password)
        {
            this.name = name;
            this.RMQ = new RMQ(host, port, username, password);
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
            ClusterHealth = 0; // Default to a 'healthy' cluster state until proven otherwise
            PollNodes();  // Poll Nodes
            PollQueues(); // Poll Queue metrics
            PollCluster();  // Final Cluster Poll
        }

        private void PollNodes()
        {          
            int num_nodes = 0;
            int num_running_nodes = 0;
            int num_net_partitions = 0; // across all nodes, should NEVER be more than 0.
            try {
                // Get the Node Objects From the RabbitMQ Cluster.
                List<NodeObject> nodes = RMQ.fetchRMQObject<List<NodeObject>>("/api/nodes");

                foreach (var node in nodes)
                {
                    float memory_used_percentage = ((float)node.mem_used / node.mem_limit) * 100;
                    log.Info("[Reporting Metric] [nodes/{0}/memory_used_percentage] [{1}]", node.name, memory_used_percentage);
                    ReportMetric("nodes/" + node.name + "/memory_used_percentage", "Percentage", memory_used_percentage);
                   
                    log.Info("[Reporting Metric] [nodes/{0}/disk_free] [{1}]", node.name, node.disk_free);
                    ReportMetric("nodes/" + node.name + "/disk_free", "Gigabytes", node.disk_free);

                    float fd_used_percentage = ((float)node.fd_used / node.fd_total) * 100;
                    log.Info("[Reporting Metric] [nodes/{0}/fd_used_percentage] [{1}]", node.name, fd_used_percentage);
                    ReportMetric("nodes/" + node.name + "/fd_used_percentage", "Percent", fd_used_percentage);

                    float sockets_used_percentage = ((float)node.sockets_used / node.sockets_total) * 100;
                    log.Info("[Reporting Metric] [nodes/{0}/sockets_used_percentage] [{1}]", node.name, sockets_used_percentage);
                    ReportMetric("nodes/" + node.name + "/sockets_used_percentage", "Percent", sockets_used_percentage);

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
                }
            }
            catch (Exception e)
            {
                log.Error("Exception Thrown: '{0}'", e.Message);
                if (ClusterHealth < 1)
                {
                    ClusterHealth = 1;  // 0 - Healthy, 1 - Unknown, 2 - Warning, 3 - Critical
                    log.Error("Doomsday clock advanced. ({0}) - Unable to fetch node information for the RabbitMQ cluster", ClusterHealth);
                }
            }

            // 0 - Healthy, 1 - Unknown, 2 - Warning, 3 - Critical
            if (ClusterHealth < 1 && num_nodes == 0)
            {
                ClusterHealth = 1;  // Unknown State (We are receiving a node count of zero)
                log.Error("Doomsday clock advanced. ({0}) - Unknown State (We are receiving a node count of zero)", ClusterHealth);
            }

            if (ClusterHealth < 2 && num_running_nodes < num_nodes)
            {
                ClusterHealth = 2; // Warning (we've lost nodes)
                log.Error("Doomsday clock advanced. ({0}) - Warning (we've lost nodes)", ClusterHealth);
            }

            if (num_running_nodes == 0)
            {
                ClusterHealth = 3;  // Critical (we have no healthy nodes)
                log.Error("Doomsday clock advanced. ({0}) - Critical (we have no healthy nodes)", ClusterHealth);
            }

            if (ClusterHealth < 3 && num_net_partitions != 0)
            {
                ClusterHealth = 3;  // 0 - Unknown, 1 - Healthy, 2 - Warning, 3 - Critical
                log.Error("Doomsday clock advanced. ({0}) - Network paritions seen: ({1})", ClusterHealth, num_net_partitions);
            }

            // Report our metrics to New Relic
            ReportMetric("global/nodes/total", "count", num_nodes);
            ReportMetric("global/nodes/failed", "count", num_nodes - num_running_nodes);
            ReportMetric("global/nodes/partitions", "count", num_net_partitions);

            log.Info("[Reporting Metric] [global/nodes/total] [{0}]", num_nodes);
            log.Info("[Reporting Metric] [global/nodes/failed] [{0}]", num_nodes - num_running_nodes);
            log.Info("[Reporting Metric] [global/nodes/partitions] [{0}]", num_net_partitions);

        }

        private void PollQueues()
        {
            int num_queues = 0;
            try
            {
                // Get the Node Objects From the RabbitMQ Cluster.
                List<QueueObject> queues = RMQ.fetchRMQObject<List<QueueObject>>("/api/queues");
                foreach (var queue in queues)
                {
                    num_queues++;
                    log.Info("queue seen: ({0})", queue.name);
                }
            }           
            catch (Exception e)
            {
                log.Error("Exception Thrown: '{0}'", e.Message);
                if (ClusterHealth < 1)
                {
                    ClusterHealth = 1;  // 0 - Healthy, 1 - Unknown, 2 - Warning, 3 - Critical
                    log.Error("Doomsday clock advanced. ({0}) - Unable to fetch queue information for the RabbitMQ cluster", ClusterHealth);
                }
            }
            log.Info("Queues seen: ({0})", num_queues);

        }

        private void PollCluster()
        {            
            try
            {
                // Get the Overview object from the RabbitMQ Cluster
                string MyClusterName = RMQ.fetchRMQObject<ClusterName>("/api/cluster-name").name;
                // log.Info("RabbitMQ Cluster Name: ({0})", MyClusterName);
            }
            catch (Exception e)
            {
                log.Error("Exception Thrown: '{0}'", e.Message);
                if (ClusterHealth < 1)
                {
                    ClusterHealth = 1;  // 0 - Healthy, 1 - Unknown, 2 - Warning, 3 - Critical
                    log.Error("Doomsday clock advanced. ({0}) - Unable to fetch cluster name for the RabbitMQ cluster", ClusterHealth);
                }                    
            }
            log.Info("[Reporting Metric] [global/health] [{0}]", ClusterHealth);
            ReportMetric("global/health", "count", ClusterHealth);
        }
    }
}
