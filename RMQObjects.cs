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
        public int send_bytes { get; set; }
        public SendBytesDetails send_bytes_details { get; set; }
        public int recv_bytes { get; set; }
        public RecvBytesDetails recv_bytes_details { get; set; }
    }

    public class ClusterLink
    {
        public Stats stats { get; set; }
        public string name { get; set; }
    }

    public class MetricsGcQueueLength
    {
        public int connection_closed { get; set; }
        public int channel_closed { get; set; }
        public int consumer_deleted { get; set; }
        public int exchange_deleted { get; set; }
        public int queue_deleted { get; set; }
        public int vhost_deleted { get; set; }
        public int node_node_deleted { get; set; }
        public int channel_consumer_deleted { get; set; }
    }



    ///////////////////////////////////////////////////////////////////////////////
    // Node Object
    ///////////////////////////////////////////////////////////////////////////////
    public class NodeObject
    {
        public string name { get; set; }

        public int mem_used { get; set; }

        public int mem_limit { get; set; }

        public double disk_free { get; set; }

        public int fd_total { get; set; }

        public int fd_used { get; set; }

        public int sockets_used { get; set; }

        public int sockets_total { get; set; }

        public bool running { get; set; }

        public List<object> partitions { get; set; }

        public bool mem_alarm { get; set; }

        public bool disk_free_alarm { get; set; }

        /*
                public string os_pid { get; set; }
                public int disk_free_limit { get; set; }
                public int proc_total { get; set; }
                public string rates_mode { get; set; }
                public int uptime { get; set; }
                public int run_queue { get; set; }
                public int processors { get; set; }
                public List<ExchangeType> exchange_types { get; set; }
                public List<AuthMechanism> auth_mechanisms { get; set; }
                public List<Application> applications { get; set; }
                public List<Context> contexts { get; set; }
                public string log_file { get; set; }
                public string sasl_log_file { get; set; }
                public string db_dir { get; set; }
                public List<string> config_files { get; set; }
                public int net_ticktime { get; set; }
                public List<string> enabled_plugins { get; set; }
                public string type { get; set; }
                public MemUsedDetails mem_used_details { get; set; }
                public FdUsedDetails fd_used_details { get; set; }
                public SocketsUsedDetails sockets_used_details { get; set; }
                public int proc_used { get; set; }
                public ProcUsedDetails proc_used_details { get; set; }
                public DiskFreeDetails disk_free_details { get; set; }
                public int gc_num { get; set; }
                public GcNumDetails gc_num_details { get; set; }
                public long gc_bytes_reclaimed { get; set; }
                public GcBytesReclaimedDetails gc_bytes_reclaimed_details { get; set; }
                public int context_switches { get; set; }
                public ContextSwitchesDetails context_switches_details { get; set; }
                public int io_read_count { get; set; }
                public IoReadCountDetails io_read_count_details { get; set; }
                public int io_read_bytes { get; set; }
                public IoReadBytesDetails io_read_bytes_details { get; set; }
                public double io_read_avg_time { get; set; }
                public IoReadAvgTimeDetails io_read_avg_time_details { get; set; }
                public int io_write_count { get; set; }
                public IoWriteCountDetails io_write_count_details { get; set; }
                public int io_write_bytes { get; set; }
                public IoWriteBytesDetails io_write_bytes_details { get; set; }
                public double io_write_avg_time { get; set; }
                public IoWriteAvgTimeDetails io_write_avg_time_details { get; set; }
                public int io_sync_count { get; set; }
                public IoSyncCountDetails io_sync_count_details { get; set; }
                public double io_sync_avg_time { get; set; }
                public IoSyncAvgTimeDetails io_sync_avg_time_details { get; set; }
                public double io_seek_count { get; set; }
                public IoSeekCountDetails io_seek_count_details { get; set; }
                public double io_seek_avg_time { get; set; }
                public IoSeekAvgTimeDetails io_seek_avg_time_details { get; set; }
                public int io_reopen_count { get; set; }
                public IoReopenCountDetails io_reopen_count_details { get; set; }
                public int mnesia_ram_tx_count { get; set; }
                public MnesiaRamTxCountDetails mnesia_ram_tx_count_details { get; set; }
                public int mnesia_disk_tx_count { get; set; }
                public MnesiaDiskTxCountDetails mnesia_disk_tx_count_details { get; set; }
                public int msg_store_read_count { get; set; }
                public MsgStoreReadCountDetails msg_store_read_count_details { get; set; }
                public int msg_store_write_count { get; set; }
                public MsgStoreWriteCountDetails msg_store_write_count_details { get; set; }
                public int queue_index_journal_write_count { get; set; }
                public QueueIndexJournalWriteCountDetails queue_index_journal_write_count_details { get; set; }
                public int queue_index_write_count { get; set; }
                public QueueIndexWriteCountDetails queue_index_write_count_details { get; set; }
                public int queue_index_read_count { get; set; }
                public QueueIndexReadCountDetails queue_index_read_count_details { get; set; }
                public int io_file_handle_open_attempt_count { get; set; }
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
        
        public int deliver_get { get; set; }

        public int ack { get; set; }

        public int redeliver { get; set; }

        public int deliver_no_ack { get; set; }

        public int deliver { get; set; }

        public int get_no_ack { get; set; }

        public int get { get; set; }

        public int publish { get; set; }

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
        public int q1 { get; set; }
        public int q2 { get; set; }
        public List<object> delta { get; set; }
        public int q3 { get; set; }
        public int q4 { get; set; }
        public int len { get; set; }
        public string target_ram_count { get; set; }
        public int next_seq_id { get; set; }
        public double avg_ingress_rate { get; set; }
        public double avg_egress_rate { get; set; }
        public int avg_ack_ingress_rate { get; set; }
        public int avg_ack_egress_rate { get; set; }
        public int mirror_seen { get; set; }
        public int mirror_senders { get; set; }
    }

    public class GarbageCollection
    {
        public int minor_gcs { get; set; }
        public int fullsweep_after { get; set; }
        public int min_heap_size { get; set; }
        public int min_bin_vheap_size { get; set; }
        public int max_heap_size { get; set; }
    }

    public class Exchange
    {
        public string vhost { get; set; }
        public string name { get; set; }
    }

    public class IncomingStats
    {
        public PublishDetails publish_details { get; set; }
        public int publish { get; set; }
    }

    public class Incoming
    {
        public Exchange exchange { get; set; }
        public IncomingStats stats { get; set; }
    }

    public class ChannelDetails
    {
        public string peer_host { get; set; }
        public int peer_port { get; set; }
        public string connection_name { get; set; }
        public string user { get; set; }
        public int number { get; set; }
        public string node { get; set; }
        public string name { get; set; }
    }

    public class Stats2
    {
        public DeliverGetDetails deliver_get_details { get; set; }
        public int deliver_get { get; set; }
        public AckDetails ack_details { get; set; }
        public int ack { get; set; }
        public RedeliverDetails redeliver_details { get; set; }
        public int redeliver { get; set; }
        public DeliverNoAckDetails deliver_no_ack_details { get; set; }
        public int deliver_no_ack { get; set; }
        public DeliverDetails deliver_details { get; set; }
        public int deliver { get; set; }
        public GetNoAckDetails get_no_ack_details { get; set; }
        public int get_no_ack { get; set; }
        public GetDetails get_details { get; set; }
        public int get { get; set; }
    }

    public class Delivery
    {
        //public List<ChannelDetails> channel_details { get; set; }
        public Stats2 stats { get; set; }
    }

    public class QueueObject
    {
        public int messages { get; set; }

        public string vhost { get; set; }

        public string name { get; set; }
        
        public List<Incoming> incoming { get; set; }

        public List<Delivery> deliveries { get; set; }

        public string state { get; set; }

        /*
        public MessageStats message_stats { get; set; }
        public MessagesDetails messages_details { get; set; }
        public int messages_ready { get; set; }
        public MessagesReadyDetails messages_ready_details { get; set; }
        public MessagesUnacknowledgedDetails messages_unacknowledged_details { get; set; }
        public int messages_unacknowledged { get; set; }
        public ReductionsDetails reductions_details { get; set; }
        public int reductions { get; set; }
        public string node { get; set; }
        public Arguments arguments { get; set; }
        public bool exclusive { get; set; }
        public bool auto_delete { get; set; }
        public bool durable { get; set; }
        public int message_bytes_paged_out { get; set; }
        public int messages_paged_out { get; set; }
        public BackingQueueStatus backing_queue_status { get; set; }
        public object head_message_timestamp { get; set; }
        public int message_bytes_persistent { get; set; }
        public int message_bytes_ram { get; set; }
        public int message_bytes_unacknowledged { get; set; }
        public int message_bytes_ready { get; set; }
        public int message_bytes { get; set; }
        public int messages_persistent { get; set; }
        public int messages_unacknowledged_ram { get; set; }
        public int messages_ready_ram { get; set; }
        public int messages_ram { get; set; }
        public GarbageCollection garbage_collection { get; set; }
        public string state { get; set; }
        public List<string> recoverable_slaves { get; set; }
        public List<string> synchronised_slave_nodes { get; set; }
        public List<string> slave_nodes { get; set; }
        public int memory { get; set; }
        public object consumer_utilisation { get; set; }
        public int consumers { get; set; }
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
        public int reductions { get; set; }
        public RecvOctDetails recv_oct_details { get; set; }
        public int recv_oct { get; set; }
        public SendOctDetails send_oct_details { get; set; }
        public int send_oct { get; set; }
        public long connected_at { get; set; }
        public ClientProperties client_properties { get; set; }
        public int channel_max { get; set; }
        public int frame_max { get; set; }
        public int timeout { get; set; }
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
        public int peer_port { get; set; }
        public int port { get; set; }        
        public string node { get; set; }
        public string type { get; set; }
        public GarbageCollection garbage_collection { get; set; }
        public int channels { get; set; }
        public string state { get; set; }
        public int send_pend { get; set; }
        public int send_cnt { get; set; }
        public int recv_cnt { get; set; }
        */
    }

}
