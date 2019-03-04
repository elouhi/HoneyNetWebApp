using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;

namespace HoneyNetWebApp.Services 
{
    public class NodeService : INodeService
    {
        private readonly INodeRepository _repository;
        private IRestResponse _NodeList;
           

        public NodeService(IRestResponse NodeList, INodeRepository repository)
        {
            _NodeList = NodeList;
            _repository = repository;
        }

        public IEnumerable<Dictionary<string, HoneyNetWebApp.Models.WebNodeModel>> NodeListToModelList()
        {
          yield return JsonConvert.DeserializeObject<Dictionary<string, Models.WebNodeModel>>(_NodeList.Content);
        }  
    }

    public interface INodeService
    {
        IEnumerable<Dictionary<string, Models.WebNodeModel>> NodeListToModelList();
    }

}
