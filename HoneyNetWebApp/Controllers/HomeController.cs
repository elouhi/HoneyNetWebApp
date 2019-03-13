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
        public async Task<IEnumerable<Dictionary<string, Models.WebNodeModel>>> GetResult(string stringSearch)
        {
            return await Task.Run(() => new NodeService(_repository.GetNodeList(stringSearch), _repository)
            .NodeListToModelList());
        }

        //return await Task.Run(() => new NodeService(_repository.GetNodeList(stringSearch), _repository)
        //  .NodeListToModelList());

        /* public IActionResult Index()
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
         }*/


        /* Null check for stringSearch not working, showing as null in debugger,
         * not entering loops to create nodeDict-->nodeList
         * */
        [HttpGet]        
        public async Task <IActionResult> Index(string stringSearch)
        {

            //Multiple selection boxes
            /*  IndexViewModels catViewModel = new IndexViewModels();
              var categories = nodeList.First().GetCategories()
                  .Select(c => new
                  {
                      CategoryID = c.CategoryID                    
                  }).ToList() ;
              ViewBag.Categories = new MultiSelectList(categories, "CaregoryID");
              */
            // var query = from item in nodeDict.GetResult(stringSearch)
            //           select item;

            //return View(await query.ToListAsync());
            // List<WebNodeModel> nodes = Search(searchString).ToList();
            // ViewBag.Search = await Search(searchString);

           

            // Debug.WriteLine(searchResults.ToString());
            // ViewData["Search"] = await Search(stringSearch, new HomeController(_repository)); 
             
            return View();
        }

       [HttpGet]
        public async Task<IActionResult> Search(string stringSearch) 
        {
            HomeController nodeDict = new HomeController(_repository);
            var nodeList = new List<WebNodeModel>();
                if (!String.IsNullOrEmpty(stringSearch))
                {                    
                    var nodes = await nodeDict.GetResult(stringSearch);
                    //Return nodeDict to View as a nodeList (<LIST>)
                    foreach (var item in nodes)
                    {
                        foreach (var x in item)
                        {
                            nodeList.Add(x.Value);
                        }
                    }               
                    var viewModel = new IndexNodeList(nodeList.ToList()).NodeList;
                    return View(viewModel.ToList());
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
