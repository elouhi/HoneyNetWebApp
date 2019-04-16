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
using System.Data;
using System.ComponentModel;

namespace HoneyNetWebApp.Services 
{
    public class NodeService : INodeService
    {
        
        private List<HoneyNetWebApp.Models.WebNodeModel> _nodeList;

        public NodeService()
        {
#pragma warning disable CS0168 // Variable is declared but never used
            List<HoneyNetWebApp.Models.WebNodeModel> _nodeList;       
#pragma warning restore CS0168 // Variable is declared but never used
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IEnumerable<HoneyNetWebApp.Models.WebNodeModel>> HeaderSort(List<HoneyNetWebApp.Models.WebNodeModel> nodeList, string Sort_Order)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
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
        //Need to figure out how to make this run async and not synchronously
        //Converts any list of objects to a dataTable
        public async Task<DataTable> ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);                          
            }
            foreach (T item in data)
            {                 
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {                 
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;                 
                }
                table.Rows.Add(row);
            }
            return table;
        }

        //Creates a list of map markers
        public async Task<string> nodeListToJsonString (List<WebNodeModel> nodeList)
        {
            return JsonConvert.SerializeObject(nodeList);
        }

    }
    
   


    public interface INodeService
    {      
        Task<IEnumerable<HoneyNetWebApp.Models.WebNodeModel>> HeaderSort(List<HoneyNetWebApp.Models.WebNodeModel> nodeList, string Sort_Order);
        Task<DataTable> ConvertToDataTable<T>(IList<T> data);
    }

}
