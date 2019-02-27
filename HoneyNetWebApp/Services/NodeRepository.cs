using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace HoneyNetWebApp.Services
{
    public class NodeRepository : HoneyNetWebApp.Services.INodeRepository
    {   
        public IRestResponse GetNodeList()
        {
            var client = new RestClient
            {
                BaseUrl = new Uri("https://honeynet-81d04.firebaseio.com")
            };
            var request = new RestRequest("data/19Feb.json", Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            //request.Resource = "data/19Feb.json";
            IRestResponse response = client.Execute(request);
            return response;            
        }       
    }

    public interface INodeRepository
    {
        IRestResponse GetNodeList();
    }

}
