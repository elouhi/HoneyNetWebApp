using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using HoneyNetWebApp.Models;
using HoneyNetWebApp.Controllers;
using RestSharp.Authenticators;
using FireSharp.Response;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using System.Text;

namespace HoneyNetWebApp.Services
{
    

    public class NodeRepository : HoneyNetWebApp.Services.INodeRepository
    {
        /*
        private const string _basePath = "https://honeynet-d9bd4.firebaseio.com/";
        private const string FirebaseSecret = "AIzaSyAZ-KsUVmqkYQN89yc37kIVOYfpS1pX7fo";
        private static IFirebaseClient _client;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = FirebaseSecret,
            BasePath = _basePath
            
        };
       public string GetLiveStream(string param)
        {          
            _client = new FirebaseClient(config);
            EventStreamResponse response = await _client.OnAsync(param)
            return ("");
        }*/
       
        
        
        //Get search result w/out dropdown param
        public IRestResponse GetNodeList(string param)
        {
            var client = new RestClient
            {
                BaseUrl = new Uri("https://honeynet-d9bd4.firebaseio.com/data/"),
                Authenticator = new HttpBasicAuthenticator("dbuser@gmail.com", "dbuser")
             };
            
            var request = new RestRequest(param, Method.GET)
            {
                RequestFormat = DataFormat.Json
            };

            var response = client.Execute(request);
            if(response.Content == null)
            {
                throw new Exception(response.ErrorMessage);
            }
            return response;            
        }

        //Get search result w/dropdown selection
        //Uses orderBy &equalTo REST API requests
        //**Need to add extra search box/param due to Month unless data tree is flattened
        public IRestResponse GetNodeList(string param, string catID)
        {
            StringBuilder _orderByReq = new StringBuilder();
            if (!String.IsNullOrWhiteSpace(catID))
            {
                _orderByReq.Append(".json?orderBy=\""+catID+ "\"&equalTo=\""+param+"\"");
            }
            
                var client = new RestClient
            {
                BaseUrl = new Uri("https://honeynet-d9bd4.firebaseio.com/data/01Mar"),
                Authenticator = new HttpBasicAuthenticator("dbuser@gmail.com", "dbuser")
            };

            var request = new RestRequest(_orderByReq.ToString(), Method.GET)
            {
                RequestFormat = DataFormat.Json
            };

            var response = client.Execute(request);
            if (response.Content == null)
            {
                throw new Exception(response.ErrorMessage);
            }
            return response;
        }

        List<WebNodeModel> INodeRepository.Search(string stringSearch, HomeController nodeDict)
        {
            throw new NotImplementedException();
        }
    }
    

    public interface INodeRepository
    {
        IRestResponse GetNodeList(string param);
        IRestResponse GetNodeList(string param, string catID);
        List<WebNodeModel> Search(string stringSearch, HomeController nodeDict);
    }

}
