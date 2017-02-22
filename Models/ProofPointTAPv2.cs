using System.Collections.Generic;

namespace SecAPI.Models
{
    public class ProofPointTAPv2
    {

        public class ThreatsInfoMap
        {
            public string classification { get; set; }
            public string threatUrl { get; set; }
            public string threatTime { get; set; }
            public string threat { get; set; }
            public string threatID { get; set; }
            public string threatType { get; set; }
        }



        public class MessagesDelivered
        {
            public int spamScore { get; set; }
            public int phishScore { get; set; }
            public List<ThreatsInfoMap> threatsInfoMap { get; set; }
            public string messageTime { get; set; }
            public int imposterScore { get; set; }
            public int malwareScore { get; set; }
            public string QID { get; set; }
            public string GUID { get; set; }
            public string sender { get; set; }
            public List<string> recipient { get; set; }
            public string senderIP { get; set; }
            public string messageID { get; set; }
        }

        public class ThreatsInfoMap2
        {
            public string classification { get; set; }
            public string threatUrl { get; set; }
            public string threatTime { get; set; }
            public string threat { get; set; }
            public string threatID { get; set; }
            public string campaignID { get; set; }
            public string threatType { get; set; }
        }

        public class MessagesBlocked
        {
            public int spamScore { get; set; }
            public int phishScore { get; set; }
            public List<ThreatsInfoMap2> threatsInfoMap { get; set; }
            public string messageTime { get; set; }
            public int imposterScore { get; set; }
            public int malwareScore { get; set; }
            public string QID { get; set; }
            public string GUID { get; set; }
            public string sender { get; set; }
            public List<string> recipient { get; set; }
            public string senderIP { get; set; }
            public string messageID { get; set; }
        }

        public class EventsRootObject
        {
            public string queryEndTime { get; set; }
            public List<object> clicksPermitted { get; set; }
            public List<object> clicksBlocked { get; set; }
            public List<MessagesDelivered> messagesDelivered { get; set; }
            public List<MessagesBlocked> messagesBlocked { get; set; }
        }






        //Forensic API

        public class What
        {
            public string rule { get; set; }
            public string sha256 { get; set; }
            public string blacklisted { get; set; }
            public string md5 { get; set; }

            public string host { get; set; }
            public List<string> cnames { get; set; }
            public List<string> ips { get; set; }
            public List<string> nameservers { get; set; }
            public List<string> nameserversList { get; set; }

            public string url { get; set; }
            public string action { get; set; }
            public string ip { get; set; }

            public string port { get; set; }
            public string httpStatus { get; set; }

            public string type { get; set; }


        }

        public class Platform
        {
            public string name { get; set; }
            public string os { get; set; }
            public string version { get; set; }
        }

        public class Forensic
        {
            public string type { get; set; }
            public string display { get; set; }
            public bool malicious { get; set; }
            public string note { get; set; }
            public int time { get; set; }
            public What what { get; set; }
            public List<Platform> platforms { get; set; }
        }

        public class Report
        {
            public string scope { get; set; }
            public string type { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public List<Forensic> forensics { get; set; }
        }

        public class ForensicsRootObject
        {
            public string generated { get; set; }
            public List<Report> reports { get; set; }
        }



    }
}
