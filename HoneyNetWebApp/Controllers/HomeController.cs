﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HoneyNetWebApp.Models;
using HoneyNetWebApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HoneyNetWebApp.Controllers
{
    public class HomeController : Controller
    {
        //Need to remove, wasting CPU cycles and memory calling repo twice
        private HoneyNetWebApp.Services.IMongoNodeRepository _repository;
        private HoneyNetWebApp.Services.INodeService _service;
        //private string stringSearch = "data/01Mar.json?";

        public HomeController(HoneyNetWebApp.Services.IMongoNodeRepository repository, HoneyNetWebApp.Services.INodeService service)
        {
            _service = service;
            _repository = repository;
        }

        // Deserialize JSON List -> OBJ List: /<controller>/
        public async Task<List<Models.WebNodeModel>> GetResult(string fieldName, string stringSearch)
        {
            if (!String.IsNullOrWhiteSpace(stringSearch))
            {
                return await Task.Run(() => _repository
                    .GetNodeByField(fieldName, stringSearch));                   
            }
            else
            {
                return await Task.Run(() => _repository.GetAllNodes());
            }                
        }

        // Deserialize JSON List -> OBJ List: /<controller>/
        public async Task<List<Models.WebNodeModel>> GetResultWithDate(string fieldName, string stringSearch, DateTime? start, DateTime? end)
        {
            if (!String.IsNullOrWhiteSpace(stringSearch))
            {
                return await Task.Run(() => _repository
                    .GetNodesByDateRange(fieldName, stringSearch, start, end));
            }
            else
            {
                return await Task.Run(() => _repository.GetAllNodesWithDate(start, end));
            }
        }



        /* Null check for stringSearch not working, showing as null in debugger,
         * not entering loops to create nodeDict-->nodeList
         * Changed from async Task to normal IActionResult method
         * */
        [HttpGet]
        public IActionResult Index(string stringSearch)
        {
            var catList = new WebNodeModel().GetCategories();
            ViewBag.CatagoryID = new SelectList(catList, "CatagoryID");
            ViewBag.EventStream = false;
            return View();
        }




        [HttpGet]
        public async Task<IActionResult> Search(string stringSearch, string CatagoryID, bool? EventStream, DateTime? startDate, DateTime? endDate) 
        {
            HomeController nodeDict = new HomeController(_repository, _service);            
            var catList = new WebNodeModel().GetCategories();
            ViewBag.CatagoryID = new SelectList(catList, "CatagoryID");          
            
            ViewBag.EventStream = false;
            if (!String.IsNullOrEmpty(stringSearch))
            {
                //if(EventStream != null){
                //ViewBag.EventStream = Convert.ToBoolean(EventStream);
                var viewModel = new IndexNodeList();
                if (!String.IsNullOrWhiteSpace(CatagoryID))
                {//Check to see if you can have params with? to overload a single getResult method
                    if (startDate != null && endDate != null)
                    {
                        var nodeList = await nodeDict.GetResultWithDate(CatagoryID.ToLower(), stringSearch, startDate, endDate);
                        //Converts the nodeList to a Json object for the google map markers
                        ViewBag.MapMarkers = JsonConvert.SerializeObject(nodeList);
                        //Return View as a nodeList (<LIST>)                      
                        return View(await nodeDict._service.ConvertToDataTable(nodeList));
                    }
                    else
                    {
                        var nodeList = await nodeDict.GetResult(CatagoryID.ToLower(), stringSearch);
                        //Return View as a nodeList (<LIST>)     
                        ViewBag.MapMarkers = JsonConvert.SerializeObject(nodeList);
                        return View(await nodeDict._service.ConvertToDataTable(nodeList));
                    }
                }
                else
                {
                    if (startDate != null && endDate != null)
                    {
                        var nodeList = await nodeDict.GetResultWithDate(null, stringSearch, startDate, endDate);
                        //Return View as a nodeList (<LIST>)        
                        ViewBag.MapMarkers = JsonConvert.SerializeObject(nodeList);
                        return View(await nodeDict._service.ConvertToDataTable(nodeList));
                    }                                       
                }                                      
             }
            //Default empty ssearch, returns entire collection 
            var defaultNodeList = await nodeDict.GetResult(null, stringSearch);
            //Return View as a defaultNodeList (<LIST>)  
            ViewBag.MapMarkers = JsonConvert.SerializeObject(defaultNodeList);
            return View(await nodeDict._service.ConvertToDataTable(defaultNodeList));
        }
        

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Real Time Stream



    }
}
