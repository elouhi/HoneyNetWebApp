using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using HoneyNetWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using HoneyNetWebApp.Controllers;

namespace HoneyNetWebApp.Services 
{
    public class NodeService : INodeService
    {
        
        private List<HoneyNetWebApp.Models.WebNodeModel> _nodeList;

        public NodeService()
        {
            List<HoneyNetWebApp.Models.WebNodeModel> _nodeList;
       
        }

        public async Task<IEnumerable<HoneyNetWebApp.Models.WebNodeModel>> HeaderSort(List<HoneyNetWebApp.Models.WebNodeModel> nodeList, string Sort_Order)
        {
            if (!String.IsNullOrWhiteSpace(Sort_Order))
            {
                switch (Sort_Order)
                {
                    case "DPT":
                        return nodeList.OrderBy(node => node.DPT).ToList();
                    case "PROTO":
                        return nodeList.OrderBy(node => node.PROTO).ToList();
                    case "SRC":
                        return nodeList.OrderBy(node => node.SRC).ToList();
                    case "TTL":
                        return nodeList.OrderBy(node => node.TTL).ToList();
                    case "Window":
                        return nodeList.OrderBy(node => node.Window).ToList();
                    case "Node":
                        return nodeList.OrderBy(node => node.Node).ToList();
                    case "Time":
                        return nodeList.OrderBy(node => node.Time).ToList();
                    case "CC":
                        return nodeList.OrderBy(node => node.countryCode).ToList();
                    case "City":
                        return nodeList.OrderBy(node => node.City).ToList();
                    case "Lat":
                        return nodeList.OrderBy(node => node.Lat).ToList();
                    case "Long":
                        return nodeList.OrderBy(node => node.Long).ToList();
                    case "RDNS":
                        return nodeList.OrderBy(node => node.Rdns).ToList();
                    case "Type":
                        return nodeList.OrderBy(node => node.Type).ToList();
                    case "Code":
                        return nodeList.OrderBy(node => node.Code).ToList();
                    default:
                        break;
                }
            }
            return nodeList;
        }
       
    }
    

    public interface INodeService
    {      
        Task<IEnumerable<HoneyNetWebApp.Models.WebNodeModel>> HeaderSort(List<HoneyNetWebApp.Models.WebNodeModel> nodeList, string Sort_Order);
    }

}
