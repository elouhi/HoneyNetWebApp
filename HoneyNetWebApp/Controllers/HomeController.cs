using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HoneyNetWebApp.Models;
using Newtonsoft.Json.Linq;
using HoneyNetWebApp.Services;

namespace HoneyNetWebApp.Controllers
{
    public class HomeController : Controller
    {
        private INodeRepository _repository;
      
        public HomeController(INodeRepository repository)
        {
            _repository = repository;
        }

        // Deserialize JSON List -> OBJ List: /<controller>/
        public IEnumerable<Dictionary<string, Models.WebNodeModel>> GetResult()
        {
            return new NodeService(_repository.GetNodeList("/data/01Mar.json"), _repository).NodeListToModelList();
        }
                
        public IActionResult Index()
        {
            
            List<WebNodeModel> nodeList = new List<WebNodeModel>();
            HomeController nodeDict = new HomeController(_repository);
            foreach(var item in nodeDict.GetResult())
            {
                foreach ( var x in item)
                {
                    nodeList.Add(x.Value);
                    //ViewData["Message"] += x.ToString();
                }
                
            }

            return View(nodeList); 
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

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
