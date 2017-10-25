using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.healthwise.newrelic.rabbitmq
{
    ///////////////////////////////////////////////////////////////////////////////
    // Objects for /api/overview and /api/nodes
    ///////////////////////////////////////////////////////////////////////////////

    public class ExchangeType
    {
        public string name { get; set; }
        public string description { get; set; }
        public bool enabled { get; set; }
        public string internal_purpose { get; set; }
    }

    public class AuthMechanism
    {
        public string name { get; set; }
        public string description { get; set; }
        public bool enabled { get; set; }
    }

    public class Application
    {
        public string name { get; set; }
        public string description { get; set; }
        public string version { get; set; }
    }

    public class SslOpts
    {
        public string cacertfile { get; set; }
        public string certfile { get; set; }
        public string keyfile { get; set; }
    }

    public class Context
    {
        public string description { get; set; }
        public string path { get; set; }
        public string port { get; set; }
        public string ssl { get; set; }
        public SslOpts ssl_opts { get; set; }
    }

    public class MemUsedDetails
    {
        public double rate { get; set; }
    }

    public class FdUsedDetails
    {
        public double rate { get; set; }
    }

    public class SocketsUsedDetails
    {
        public double rate { get; set; }
    }

    public class ProcUsedDetails
    {
        public double rate { get; set; }
    }

    public class DiskFreeDetails
    {
        public double rate { get; set; }
    }

    public class GcNumDetails
    {
        public double rate { get; set; }
    }

    public class GcBytesReclaimedDetails
    {
        public double rate { get; set; }
    }

    public class ContextSwitchesDetails
    {
        public double rate { get; set; }
    }

    public class IoReadCountDetails
    {
        public double rate { get; set; }
    }

    public class IoReadBytesDetails
    {
        public double rate { get; set; }
    }

    public class IoReadAvgTimeDetails
    {
        public double rate { get; set; }
    }

    public class IoWriteCountDetails
    {
        public double rate { get; set; }
    }

    public class IoWriteBytesDetails
    {
        public double rate { get; set; }
    }

    public class IoWriteAvgTimeDetails
    {
        public double rate { get; set; }
    }

    public class IoSyncCountDetails
    {
        public double rate { get; set; }
    }

    public class IoSyncAvgTimeDetails
    {
        public double rate { get; set; }
    }

    public class IoSeekCountDetails
    {
        public double rate { get; set; }
    }

    public class IoSeekAvgTimeDetails
    {
        public double rate { get; set; }
    }

    public class IoReopenCountDetails
    {
        public double rate { get; set; }
    }

    public class MnesiaRamTxCountDetails
    {
        public double rate { get; set; }
    }

    public class MnesiaDiskTxCountDetails
    {
        public double rate { get; set; }
    }

    public class MsgStoreReadCountDetails
    {
        public double rate { get; set; }
    }

    public class MsgStoreWriteCountDetails
    {
        public double rate { get; set; }
    }

    public class QueueIndexJournalWriteCountDetails
    {
        public double rate { get; set; }
    }

    public class QueueIndexWriteCountDetails
    {
        public double rate { get; set; }
    }

    public class QueueIndexReadCountDetails
    {
        public double rate { get; set; }
    }

    public class IoFileHandleOpenAttemptCountDetails
    {
        public double rate { get; set; }
    }

    public class IoFileHandleOpenAttemptAvgTimeDetails
    {
        public double rate { get; set; }
    }

    public class SendBytesDetails
    {
        public double rate { get; set; }
    }

    public class RecvBytesDetails
    {
        public double rate { get; set; }
    }

    public class Stats
    {
        public long send_bytes { get; set; }
        public SendBytesDetails send_bytes_details { get; set; }
        public long recv_bytes { get; set; }
        public RecvBytesDetails recv_bytes_details { get; set; }
    }

    public class ClusterLink
    {
        public Stats stats { get; set; }
        public string name { get; set; }

        /*
        public string peer_addr { get; set; }
        public long peer_port { get; set; }
        public string sock_addr { get; set; }
        public long sock_port { get; set; }
        public long recv_bytes { get; set; }
        public long send_bytes { get; set; }
        */
    }

    public class MetricsGcQueueLength
    {
        public long connection_closed { get; set; }
        public long channel_closed { get; set; }
        public long consumer_deleted { get; set; }
        public long exchange_deleted { get; set; }
        public long queue_deleted { get; set; }
        public long vhost_deleted { get; set; }
        public long node_node_deleted { get; set; }
        public long channel_consumer_deleted { get; set; }
    }



    ///////////////////////////////////////////////////////////////////////////////
    // Node Object
    ///////////////////////////////////////////////////////////////////////////////
    public class NodeObject
    {
        public string name { get; set; }

        public long mem_used { get; set; }

        public long mem_limit { get; set; }

        public double disk_free { get; set; }

        public long fd_total { get; set; }

        public long fd_used { get; set; }

        public long sockets_used { get; set; }

        public long sockets_total { get; set; }

        public bool running { get; set; }

        public List<object> partitions { get; set; }

        public bool mem_alarm { get; set; }

        public bool disk_free_alarm { get; set; }

        /*
                public string os_pid { get; set; }
                public long disk_free_limit { get; set; }
                public long proc_total { get; set; }
                public string rates_mode { get; set; }
                public long uptime { get; set; }
                public long run_queue { get; set; }
                public long processors { get; set; }
                public List<ExchangeType> exchange_types { get; set; }
                public List<AuthMechanism> auth_mechanisms { get; set; }
                public List<Application> applications { get; set; }
                public List<Context> contexts { get; set; }
                public string log_file { get; set; }
                public string sasl_log_file { get; set; }
                public string db_dir { get; set; }
                public List<string> config_files { get; set; }
                public long net_ticktime { get; set; }
                public List<string> enabled_plugins { get; set; }
                public string type { get; set; }
                public MemUsedDetails mem_used_details { get; set; }
                public FdUsedDetails fd_used_details { get; set; }
                public SocketsUsedDetails sockets_used_details { get; set; }
                public long proc_used { get; set; }
                public ProcUsedDetails proc_used_details { get; set; }
                public DiskFreeDetails disk_free_details { get; set; }
                public long gc_num { get; set; }
                public GcNumDetails gc_num_details { get; set; }
                public long gc_bytes_reclaimed { get; set; }
                public GcBytesReclaimedDetails gc_bytes_reclaimed_details { get; set; }
                public long context_switches { get; set; }
                public ContextSwitchesDetails context_switches_details { get; set; }
                public long io_read_count { get; set; }
                public IoReadCountDetails io_read_count_details { get; set; }
                public long io_read_bytes { get; set; }
                public IoReadBytesDetails io_read_bytes_details { get; set; }
                public double io_read_avg_time { get; set; }
                public IoReadAvgTimeDetails io_read_avg_time_details { get; set; }
                public long io_write_count { get; set; }
                public IoWriteCountDetails io_write_count_details { get; set; }
                public long io_write_bytes { get; set; }
                public IoWriteBytesDetails io_write_bytes_details { get; set; }
                public double io_write_avg_time { get; set; }
                public IoWriteAvgTimeDetails io_write_avg_time_details { get; set; }
                public long io_sync_count { get; set; }
                public IoSyncCountDetails io_sync_count_details { get; set; }
                public double io_sync_avg_time { get; set; }
                public IoSyncAvgTimeDetails io_sync_avg_time_details { get; set; }
                public double io_seek_count { get; set; }
                public IoSeekCountDetails io_seek_count_details { get; set; }
                public double io_seek_avg_time { get; set; }
                public IoSeekAvgTimeDetails io_seek_avg_time_details { get; set; }
                public long io_reopen_count { get; set; }
                public IoReopenCountDetails io_reopen_count_details { get; set; }
                public long mnesia_ram_tx_count { get; set; }
                public MnesiaRamTxCountDetails mnesia_ram_tx_count_details { get; set; }
                public long mnesia_disk_tx_count { get; set; }
                public MnesiaDiskTxCountDetails mnesia_disk_tx_count_details { get; set; }
                public long msg_store_read_count { get; set; }
                public MsgStoreReadCountDetails msg_store_read_count_details { get; set; }
                public long msg_store_write_count { get; set; }
                public MsgStoreWriteCountDetails msg_store_write_count_details { get; set; }
                public long queue_index_journal_write_count { get; set; }
                public QueueIndexJournalWriteCountDetails queue_index_journal_write_count_details { get; set; }
                public long queue_index_write_count { get; set; }
                public QueueIndexWriteCountDetails queue_index_write_count_details { get; set; }
                public long queue_index_read_count { get; set; }
                public QueueIndexReadCountDetails queue_index_read_count_details { get; set; }
                public long io_file_handle_open_attempt_count { get; set; }
                public IoFileHandleOpenAttemptCountDetails io_file_handle_open_attempt_count_details { get; set; }
                public double io_file_handle_open_attempt_avg_time { get; set; }
                public IoFileHandleOpenAttemptAvgTimeDetails io_file_handle_open_attempt_avg_time_details { get; set; }
                public List<ClusterLink> cluster_links { get; set; }
                public MetricsGcQueueLength metrics_gc_queue_length { get; set; }
        */
    }

    public class ClusterName
    {
        public string name { get; set; }
    }


    ///////////////////////////////////////////////////////////////////////////////
    // Queue Object
    ///////////////////////////////////////////////////////////////////////////////
    public class MessagesDetails
    {
        public double rate { get; set; }
    }

    public class MessagesUnacknowledgedDetails
    {
        public double rate { get; set; }
    }

    public class MessagesReadyDetails
    {
        public double rate { get; set; }
    }

    public class ReductionsDetails
    {
        public double rate { get; set; }
    }

    public class DeliverGetDetails
    {
        public double rate { get; set; }
    }

    public class AckDetails
    {
        public double rate { get; set; }
    }

    public class RedeliverDetails
    {
        public double rate { get; set; }
    }

    public class DeliverNoAckDetails
    {
        public double rate { get; set; }
    }

    public class DeliverDetails
    {
        public double rate { get; set; }
    }

    public class GetNoAckDetails
    {
        public double rate { get; set; }
    }

    public class GetDetails
    {
        public double rate { get; set; }
    }

    public class PublishDetails
    {
        public double rate { get; set; }
    }

    public class MessageStats
    {
        
        public long deliver_get { get; set; }

        public long ack { get; set; }

        public long redeliver { get; set; }

        public long deliver_no_ack { get; set; }

        public long deliver { get; set; }

        public long get_no_ack { get; set; }

        public long get { get; set; }

        public long publish { get; set; }

        public PublishDetails publish_details { get; set; }

        public DeliverGetDetails deliver_get_details { get; set; }

        public AckDetails ack_details { get; set; }

        public RedeliverDetails redeliver_details { get; set; }

        public DeliverNoAckDetails deliver_no_ack_details { get; set; }

        public DeliverDetails deliver_details { get; set; }

        public GetNoAckDetails get_no_ack_details { get; set; }

        public GetDetails get_details { get; set; }

        /*
        public DeliverGetDetails deliver_get_details { get; set; }
        public AckDetails ack_details { get; set; }
        public RedeliverDetails redeliver_details { get; set; }
        public DeliverNoAckDetails deliver_no_ack_details { get; set; }
        public DeliverDetails deliver_details { get; set; }
        public GetNoAckDetails get_no_ack_details { get; set; }
        public GetDetails get_details { get; set; }       
        */
    }

    public class Arguments
    {
        [JsonProperty("x-ha-policy")]
        public string x_ha_policy { get; set; }

        [JsonProperty("x-internal-purpose")]
        public string x_internal_purpose { get; set; }
    }

    public class BackingQueueStatus
    {
        public string mode { get; set; }
        public long q1 { get; set; }
        public long q2 { get; set; }
        public List<object> delta { get; set; }
        public long q3 { get; set; }
        public long q4 { get; set; }
        public long len { get; set; }
        public string target_ram_count { get; set; }
        public long next_seq_id { get; set; }
        public double avg_ingress_rate { get; set; }
        public double avg_egress_rate { get; set; }
        public long avg_ack_ingress_rate { get; set; }
        public long avg_ack_egress_rate { get; set; }
        public long mirror_seen { get; set; }
        public long mirror_senders { get; set; }
    }

    public class GarbageCollection
    {
        public long minor_gcs { get; set; }
        public long fullsweep_after { get; set; }
        public long min_heap_size { get; set; }
        public long min_bin_vheap_size { get; set; }
        public long max_heap_size { get; set; }
    }

    public class Exchange
    {
        public string vhost { get; set; }
        public string name { get; set; }
    }

    public class IncomingStats
    {
        public PublishDetails publish_details { get; set; }
        public long publish { get; set; }
    }

    public class Incoming
    {
        public Exchange exchange { get; set; }
        public IncomingStats stats { get; set; }
    }

    public class ChannelDetails
    {
        public string peer_host { get; set; }
        public long peer_port { get; set; }
        public string connection_name { get; set; }
        public string user { get; set; }
        public long number { get; set; }
        public string node { get; set; }
        public string name { get; set; }
    }

    public class Stats2
    {
        public DeliverGetDetails deliver_get_details { get; set; }
        public long deliver_get { get; set; }
        public AckDetails ack_details { get; set; }
        public long ack { get; set; }
        public RedeliverDetails redeliver_details { get; set; }
        public long redeliver { get; set; }
        public DeliverNoAckDetails deliver_no_ack_details { get; set; }
        public long deliver_no_ack { get; set; }
        public DeliverDetails deliver_details { get; set; }
        public long deliver { get; set; }
        public GetNoAckDetails get_no_ack_details { get; set; }
        public long get_no_ack { get; set; }
        public GetDetails get_details { get; set; }
        public long get { get; set; }
    }

    public class Delivery
    {
        //public List<ChannelDetails> channel_details { get; set; }
        public Stats2 stats { get; set; }
    }

    public class QueueObject
    {
        public long messages { get; set; }

        public string vhost { get; set; }

        public string name { get; set; }
        
        public List<Incoming> incoming { get; set; }

        public List<Delivery> deliveries { get; set; }

        public string state { get; set; }

        /*
        public MessageStats message_stats { get; set; }
        public MessagesDetails messages_details { get; set; }
        public long messages_ready { get; set; }
        public MessagesReadyDetails messages_ready_details { get; set; }
        public MessagesUnacknowledgedDetails messages_unacknowledged_details { get; set; }
        public long messages_unacknowledged { get; set; }
        public ReductionsDetails reductions_details { get; set; }
        public long reductions { get; set; }
        public string node { get; set; }
        public Arguments arguments { get; set; }
        public bool exclusive { get; set; }
        public bool auto_delete { get; set; }
        public bool durable { get; set; }
        public long message_bytes_paged_out { get; set; }
        public long messages_paged_out { get; set; }
        public BackingQueueStatus backing_queue_status { get; set; }
        public object head_message_timestamp { get; set; }
        public long message_bytes_persistent { get; set; }
        public long message_bytes_ram { get; set; }
        public long message_bytes_unacknowledged { get; set; }
        public long message_bytes_ready { get; set; }
        public long message_bytes { get; set; }
        public long messages_persistent { get; set; }
        public long messages_unacknowledged_ram { get; set; }
        public long messages_ready_ram { get; set; }
        public long messages_ram { get; set; }
        public GarbageCollection garbage_collection { get; set; }
        public string state { get; set; }
        public List<string> recoverable_slaves { get; set; }
        public List<string> synchronised_slave_nodes { get; set; }
        public List<string> slave_nodes { get; set; }
        public long memory { get; set; }
        public object consumer_utilisation { get; set; }
        public long consumers { get; set; }
        public object exclusive_consumer_tag { get; set; }
        public string policy { get; set; }
        */
    }


    ///////////////////////////////////////////////////////////////////////////////
    // Connection Details
    ///////////////////////////////////////////////////////////////////////////////

    public class RecvOctDetails
    {
        public double rate { get; set; }
    }

    public class SendOctDetails
    {
        public double rate { get; set; }
    }

    public class Capabilities
    {
        public bool publisher_confirms { get; set; }
        public bool consumer_cancel_notify { get; set; }
        public bool exchange_exchange_bindings { get; set; }

        [JsonProperty("basic.nack")]
        public bool basic_nack { get; set; }

        [JsonProperty("connection.blocked")]
        public bool connection_blocked { get; set; }
        public bool authentication_failure_close { get; set; }
    }

    public class ClientProperties
    {
        public Capabilities capabilities { get; set; }
        public string product { get; set; }
        public string platform { get; set; }
        public string version { get; set; }
        public string information { get; set; }
    }

    public class ConnectionObject
    {
        public string name { get; set; }

        /*
        public ReductionsDetails reductions_details { get; set; }
        public long reductions { get; set; }
        public RecvOctDetails recv_oct_details { get; set; }
        public long recv_oct { get; set; }
        public SendOctDetails send_oct_details { get; set; }
        public long send_oct { get; set; }
        public long connected_at { get; set; }
        public ClientProperties client_properties { get; set; }
        public long channel_max { get; set; }
        public long frame_max { get; set; }
        public long timeout { get; set; }
        public string vhost { get; set; }
        public string user { get; set; }
        public string protocol { get; set; }
        public object ssl_hash { get; set; }
        public string ssl_cipher { get; set; }
        public string ssl_key_exchange { get; set; }
        public string ssl_protocol { get; set; }
        public string auth_mechanism { get; set; }
        public object peer_cert_validity { get; set; }
        public object peer_cert_issuer { get; set; }
        public object peer_cert_subject { get; set; }
        public bool ssl { get; set; }
        public string peer_host { get; set; }
        public string host { get; set; }
        public long peer_port { get; set; }
        public long port { get; set; }        
        public string node { get; set; }
        public string type { get; set; }
        public GarbageCollection garbage_collection { get; set; }
        public long channels { get; set; }
        public string state { get; set; }
        public long send_pend { get; set; }
        public long send_cnt { get; set; }
        public long recv_cnt { get; set; }
        */
    }

}
