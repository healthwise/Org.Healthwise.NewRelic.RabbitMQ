# Org.Healthwise.NewRelic.RabbitMQ
New Relic Plugin for RabbitMQ Clusters

This plugin reports metrics about the RabbitMQ cluster being monitored.

Overview
	Cluster Status - Reports Health (1), Warning (2), and Critical (3) based on the state of the cluster.
	Number of connections to the cluster.
	Number of network paritions seen.

Nodes
	Number of Nodes
	Number of Failed Nodes

	Per Node Metrics for:
		Memory Used (percentage)
		Disk Free
		File Descriptions Used (percentage)
		Sockets Used (percentage)
		Memory Alarm (True/False)
		Disk Free Alarm (True/False)

Queues
	Total number of queues
	Queue Names
	Delivery Get Details (messages/second)
	Publish Details (messages/second)
	Number of Messages (count)

# Requirements
1. .Net 4.5

# Configuration / Installation
1. Download release and unzip on machine to handle monitoring.
2. Edit Config Files
    rename newrelic.template.json to newrelic.json
    Rename plugin.template.json to plugin.json
    Update settings in both config files for your environment
3. Run plugin.exe from Command line

Use NPI to install the plugin to register as a service

1. Run Command as admin: npi install org.healthwise.newrelic.rabbitmq
2. Follow on screen prompts
