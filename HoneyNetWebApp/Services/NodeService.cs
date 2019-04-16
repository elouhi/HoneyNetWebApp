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

       
    }
    
   


    public interface INodeService
    {          
        Task<DataTable> ConvertToDataTable<T>(IList<T> data);
    }

}
