using System.Collections.Generic;
using System.Linq;
using RestSharp;
using SecAPI.APIClientBase;
using SecAPI.Models;

namespace SecAPI
{
    /// <summary>
    /// A Basic API Client for Cisco AMP for Endpoints API v1.  See https://api-docs.amp.cisco.com/
    /// </summary>
    public class AMPClientv1 : APIClient
    {
        public AMPClientv1(string uriBase, string username, string password) : base(uriBase, username, password)
        {

        }

        private CiscoAMPEndpointsv1.RootObject GETCallAMPAPI(string _resource)
        {

            var request = new RestRequest();
            request.Resource = _resource;
            return Execute<CiscoAMPEndpointsv1.RootObject>(request);

        }

        private CiscoAMPEndpointsv1.RootObject POSTCallAMPAPI(string _resource)
        {

            var request = new RestRequest(Method.POST);
            request.Resource = _resource;
            return Execute<CiscoAMPEndpointsv1.RootObject>(request);

        }


        //POST /v1/file_lists/{:file_list_guid}/files/{:sha256}
        public CiscoAMPEndpointsv1.RootObject postSHAtoFileList(string fileListGUID, string fileSHA256)
        {
            return POSTCallAMPAPI(string.Format("file_lists/{0}/files/{1}", fileListGUID, fileSHA256.Trim()));
        }




        public List<string> getFileListDetails(string _listGUID)
        {
            List<string> returnList = new List<string>();

            var request = new RestRequest();
            request.Resource = string.Format("file_lists/{0}/files", _listGUID);
            var shas = Execute<CiscoAMPEndpointsv1.FileListRootObject>(request);

            returnList.AddRange(shas.data.items.Select(i => i.sha256));


            while (shas.metadata.links.next != null)
            {
                string replaced = shas.metadata.links.next.ToLower().Replace(this._uriBase.ToLower(), ""); /// remove the uriBase or RestClient will prefix it again.  It requires UrlBase.
                var request2 = new RestRequest(replaced);
                shas = Execute<CiscoAMPEndpointsv1.FileListRootObject>(request2);
                returnList.AddRange(shas.data.items.Select(i => i.sha256));
            }

            return returnList;
        }



        public CiscoAMPEndpointsv1.RootObject getComputerByHostname(string hName, int returnLimit)
        {
            return GETCallAMPAPI(string.Format("computers?hostname[]={0}&limit={1}", hName, returnLimit));
        }


        /// <summary>
        /// Fetch list of computers that have observed activity with given query value
        /// </summary>
        /// <param name="queryValue">file name, SHA256, IP, or URL</param>
        /// <param name="returnLimit"></param>
        /// <returns>There is a hard limit of 5000 historical entries searched for this endpoint.</returns>
        public CiscoAMPEndpointsv1.RootObject getSearchActivity(string searchTerm, int returnLimit)
        {
            return GETCallAMPAPI(string.Format("computers/activity?q={0}&offset=0&limit={1}", searchTerm, returnLimit));
        }

        public CiscoAMPEndpointsv1.RootObject getEvents(int returnLimit)
        {
            return GETCallAMPAPI(string.Format("events?limit={0}", returnLimit));
        }

        public CiscoAMPEndpointsv1.RootObject getComputerByInternalIP(string searchIPTerm, int returnLimit)
        {
            return GETCallAMPAPI(string.Format("computers?internal_ip={0}&limit={1}", searchIPTerm, returnLimit));
        }

        public CiscoAMPEndpointsv1.RootObject getComputerByExternalIP(string searchIPTerm, int returnLimit)
        {
            return GETCallAMPAPI(string.Format("computers?external_ip={0}&limit={1}", searchIPTerm, returnLimit));
        }

        public CiscoAMPEndpointsv1.RootObject getSimpleCustomDetectionLists()
        {
            return GETCallAMPAPI("file_lists/simple_custom_detections");
        }



        /// <summary>
        /// Provides a list of all activities associated with a particular computer. This is analogous to the Device Trajectory on the FireAMP Console.
        /// </summary>
        /// <param name="connectorGUID"></param>
        /// <returns></returns>
        public CiscoAMPEndpointsv1.RootObject getComputerTrajectory(string connectorGUID)
        {
            return GETCallAMPAPI(string.Format("computers/{0}/trajectory", connectorGUID));
        }


        /// <summary>
        /// Provides a list of all activities associated with a particular computer. This is analogous to the Device Trajectory on the FireAMP Console.
        /// </summary>
        /// <param name="connectorGUID"></param>
        /// <param name="queryTerm">you can search for an IP Address, SHA256 or URL.</param>
        /// <param name="returnLimit"></param>
        /// <returns></returns>
        public CiscoAMPEndpointsv1.RootObject getComputerTrajectorySearch(string connectorGUID, string queryTerm, int returnLimit)
        {
            return GETCallAMPAPI(string.Format("computers/{0}/trajectory?q={1}&limit={2}", connectorGUID, queryTerm, returnLimit));
        }


    }



}
