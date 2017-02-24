using System;
using RestSharp;
using SecAPI.APIClientBase;
using SecAPI.Models;

namespace SecAPI
{
    /// <summary>
    /// A simple API Client for ProofPoint TAP API v2.  See https://help.proofpoint.com/Threat_Insight_Dashboard/API_Documentation
    /// </summary>
    public class ProofpointTAPClientv2 : APIClient
    {
        public ProofpointTAPClientv2(string uriBase, string username, string password) : base(uriBase, username, password)
        {


        }

        private ProofPointTAPv2.EventsRootObject callPPEventsAPI(string _resource)
        {

            var request = new RestRequest();
            request.Resource = _resource;
            return Execute<ProofPointTAPv2.EventsRootObject>(request);

        }

        private ProofPointTAPv2.ForensicsRootObject callPPForensicsAPI(string _resource)
        {

            var request = new RestRequest();
            request.Resource = _resource;
            return Execute<ProofPointTAPv2.ForensicsRootObject>(request);

        }


        private ProofPointTAPv2.CampaignRootObject callPPCampaignAPI(string _resource)
        {
            var request = new RestRequest();
            request.Resource = _resource;
            return Execute<ProofPointTAPv2.CampaignRootObject>(request);
        }


        public ProofPointTAPv2.EventsRootObject getEvents(int sinceSeconds = 3600)
        {
            return callPPEventsAPI(string.Format("siem/all?format=json&sinceSeconds={0}", sinceSeconds));
        }


        public ProofPointTAPv2.ForensicsRootObject getForensics(string forensicsID, bool includeCampaignForensics)
        {
            return callPPForensicsAPI(string.Format("forensics?threatId={0}&includeCampaignForensics={1}", forensicsID, includeCampaignForensics));
        }

        public ProofPointTAPv2.CampaignRootObject getCampaign(string campaignID)
        {
            return callPPCampaignAPI(string.Format("campaign/{0}", campaignID));
        }
         

    }
}
