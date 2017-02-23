using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecAPI.Models
{
    public class CiscoAMPEndpointsv1
    {

        public class Links
        {
            public string self { get; set; }
            public string next { get; set; }

          
        }

        public class PolicyLinks
        {
            public string policy { get; set; }
        }
        public class ItemLinks
        {
            public string file_list { get; set; }
        }


        public class Policy
        {
            public string name { get; set; }
            public string guid { get; set; }
            public PolicyLinks links { get; set; }
        }

        public class Item
        {
            public string sha256 { get; set; }
            public string description { get; set; }
            public string source { get; set; }
            public ItemLinks links { get; set; }
        }

        public class Data
        {
            public string name { get; set; }
            public string guid { get; set; }
            public List<Policy> policies { get; set; }
            public List<Item> items { get; set; }
        }

        public class Results
        {
            public int total { get; set; }
            public int current_item_count { get; set; }
            public int index { get; set; }
            public int items_per_page { get; set; }
        }

        public class Metadata
        {
            public Links links { get; set; }
            public Results results { get; set; }
        }

        public class ComputerLinks
        {
            public string computer { get; set; }
            public string trajectory { get; set; }
            public string group { get; set; }
        }

        public class Computer
        {
            public string connector_guid { get; set; }
            public string hostname { get; set; }
            public bool active { get; set; }
            public ComputerLinks links { get; set; }
            public string connector_version { get; set; }
            public string operating_system { get; set; }
            public List<string> internal_ips { get; set; }
            public string external_ip { get; set; }
            public string group_guid { get; set; }
            public List<NetworkAddress> network_addresses { get; set; }
            public Policy policy { get; set; }
        }

        public class Identity
        {
            public string sha256 { get; set; }
        }

        public class Identity2
        {
            public string sha256 { get; set; }
        }

        public class Parent
        {
            public string disposition { get; set; }
            public Identity2 identity { get; set; }
        }

        public class File
        {
            public string disposition { get; set; }
            public Identity identity { get; set; }
            public Parent parent { get; set; }
            public string file_name { get; set; }
            public string file_path { get; set; }

            public string file_type { get; set; }
        }

        public class Vulnerability
        {
            public string name { get; set; }
            public string score { get; set; }
        }

        public class Event
        {
            public string id { get; set; }
            public int timestamp { get; set; }
            public int timestamp_nanoseconds { get; set; }
            public string date { get; set; }
            public string event_type { get; set; }
            public int event_type_id { get; set; }
            public List<string> group_guids { get; set; }
            public string detection { get; set; }
            public string detection_id { get; set; }

            public NetworkInfo network_info { get; set; }

            public File file { get; set; }

            public string url { get; set; }

            public string normalized_url { get; set; }

        }

         
        public class NetworkInfo
        {
            public string dirty_url { get; set; }
            public string remote_ip { get; set; }
            public int remote_port { get; set; }
            public string local_ip { get; set; }
            public int local_port { get; set; }
            public Nfm nfm { get; set; }
            public Parent parent { get; set; }
        }

        public class Nfm
        {
            public string direction { get; set; }
            public string protocol { get; set; }
        }


        public class Datum
        {
            public object id { get; set; }
            public int timestamp { get; set; }
            public int timestamp_nanoseconds { get; set; }
            public string date { get; set; }
            public string event_type { get; set; }
            public int event_type_id { get; set; }
            public string detection { get; set; }
            public List<string> group_guids { get; set; }
            public Computer computer { get; set; }
            public List<Event> events { get; set; }
            public File file { get; set; }
            public List<Vulnerability> vulnerabilities { get; set; }



            public string connector_guid { get; set; }
            public string hostname { get; set; }
            public bool active { get; set; }
            public List<ComputerLinks> links { get; set; }


            public string connector_version { get; set; }
            public string operating_system { get; set; }
            public List<string> internal_ips { get; set; }
            public string external_ip { get; set; }
            public string group_guid { get; set; }

            public List<NetworkAddress> network_addresses { get; set; }
            public Policy policy { get; set; }
        }

        public class RootObject
        {
            public string version { get; set; }
            public Metadata metadata { get; set; }
            public List<Datum> data { get; set; }
        }


     
        public class FileListRootObject
        {
            public string version { get; set; }
            public Metadata metadata { get; set; }
            public Data data { get; set; }
        }

        public class NetworkAddress
        {
            public string mac { get; set; }
            public string ip { get; set; }
        }

     
    }
}
