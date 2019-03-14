using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HoneyNetWebApp.Models;
using Newtonsoft.Json.Linq;
using HoneyNetWebApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HoneyNetWebApp.Controllers
{
    public class HomeController : Controller
    {
        
        private INodeRepository _repository;
        //private string stringSearch = "data/01Mar.json?";

        public HomeController(INodeRepository repository)
        {
            _repository = repository;
        }

        // Deserialize JSON List -> OBJ List: /<controller>/
        public async Task<IEnumerable<Dictionary<string, Models.WebNodeModel>>> GetResult(string stringSearch, string catID)
        {
            if (!String.IsNullOrWhiteSpace(catID))
            {
                return await Task.Run(() => new NodeService(_repository
                    .GetNodeList(stringSearch, catID), _repository)
                    .NodeListToModelList());
            }
            else
            {
                return await Task.Run(() => new NodeService(_repository
                    .GetNodeList(stringSearch), _repository)
                    .NodeListToModelList());
            }                
        }
               


        /* Null check for stringSearch not working, showing as null in debugger,
         * not entering loops to create nodeDict-->nodeList
         * */
        [HttpGet]        
        public async Task <IActionResult> Index(string stringSearch)
        {
            var catList = new WebNodeModel().GetCategories();
            ViewBag.CatagoryID = new SelectList(catList, "CatagoryID");
            return View();
        }

       [HttpGet]
        public async Task<IActionResult> Search(string stringSearch, string CatagoryID) 
        {
            HomeController nodeDict = new HomeController(_repository);
            var nodeList = new List<WebNodeModel>();
            var catList = new WebNodeModel().GetCategories();
            ViewBag.CatagoryID = new SelectList(catList, "CatagoryID");
            if (!String.IsNullOrEmpty(stringSearch))
                {
                    var viewModel = new IndexNodeList();                   
                if (!String.IsNullOrWhiteSpace(CatagoryID))
                {
                    var nodes = await nodeDict.GetResult(stringSearch, CatagoryID);
                    //Return nodeDict to View as a nodeList (<LIST>)
                    foreach (var item in nodes)
                    {
                        foreach (var x in item)
                        {
                            nodeList.Add(x.Value);
                        }
                    }
                }
                else
                {
                    var nodes = await nodeDict.GetResult(stringSearch, null);
                    //Return nodeDict to View as a nodeList (<LIST>)
                    foreach (var item in nodes)
                    {
                        foreach (var x in item)
                        {
                            nodeList.Add(x.Value);
                        }
                    }
                }                
                    viewModel.NodeList = nodeList.ToList();
                    return View(viewModel.NodeList.ToList());
                }                    
            return View();
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
    }
}
