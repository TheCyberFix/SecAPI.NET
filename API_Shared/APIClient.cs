using System;
using RestSharp;
using RestSharp.Authenticators;

namespace SecAPI.APIClientBase
{
    public class APIClient
    {

        private RestClient _client;
        private string _username;
        private string _password;
        public string _uriBase;


        public APIClient(string uriBase, string username, string password)
        {
            _uriBase = uriBase;
            _username = username;
            _password = password;   
        }


        public T Execute<T>(RestRequest request) where T : new()
        {
            _client = new RestClient();
            _client.Authenticator = new HttpBasicAuthenticator(_username, _password);
            _client.BaseUrl = new System.Uri(_uriBase);

            var response = _client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var APIClientException = new ApplicationException(message, response.ErrorException);
                throw APIClientException;
            }
            return response.Data;
        }



       
    }
}
