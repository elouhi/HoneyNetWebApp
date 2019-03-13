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
       
        
        

        public IRestResponse GetNodeList(string param)
        {
            var client = new RestClient
            {
                BaseUrl = new Uri("https://honeynet-d9bd4.firebaseio.com/"),
                Authenticator = new HttpBasicAuthenticator("dbuser@gmail.com", "dbuser")
             };
            
            var request = new RestRequest(param, Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
             //request.AddParameter("text/json", ParameterType.RequestBody);
            var response = client.Execute(request);
            if(response.Content == null)
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
        List<WebNodeModel> Search(string stringSearch, HomeController nodeDict);
    }

}
