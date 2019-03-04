﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using HoneyNetWebApp.Models;

namespace HoneyNetWebApp.Services
{
    public class NodeRepository : HoneyNetWebApp.Services.INodeRepository
    {

        static string auth = "AIzaSyAZ-KsUVmqkYQN89yc37kIVOYfpS1pX7fo";         
        static string authUrl = "honeynet-d9bd4.firebaseapp.com";

        public IRestResponse GetNodeList(string param)
        {
            var client = new RestClient
            {
                BaseUrl = new Uri("https://honeynet-d9bd4.firebaseio.com")
            };
            var request = new RestRequest(param, Method.GET)
            {
                RequestFormat = DataFormat.Json
            }; 
            IRestResponse response = client.Execute(request);
            return response;            
        }       
    }

    public interface INodeRepository
    {
        IRestResponse GetNodeList(string param);
    }

}
