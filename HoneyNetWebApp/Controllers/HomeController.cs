using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HoneyNetWebApp.Models;
using HoneyNetWebApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HoneyNetWebApp.Controllers
{
    public class HomeController : Controller
    {
        //Need to remove, wasting CPU cycles and memory calling repo twice
        private HoneyNetWebApp.Services.IMongoNodeRepository _repository;
        //private string stringSearch = "data/01Mar.json?";

        public HomeController(HoneyNetWebApp.Services.IMongoNodeRepository repository)
        {
            _repository = repository;
        }

        // Deserialize JSON List -> OBJ List: /<controller>/
        public async Task<List<Models.WebNodeModel>> GetResult(string stringSearch, string fieldName)
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
               


        /* Null check for stringSearch not working, showing as null in debugger,
         * not entering loops to create nodeDict-->nodeList
         * */
        [HttpGet]        
        public async Task <IActionResult> Index(string stringSearch)
        {
            var catList = new WebNodeModel().GetCategories();
            ViewBag.CatagoryID = new SelectList(catList, "CatagoryID");
            ViewBag.EventStream = false;
            return View();
        }

       [HttpGet]
        public async Task<IActionResult> Search(string stringSearch, string CatagoryID, bool? EventStream) 
        {
            HomeController nodeDict = new HomeController(_repository);            
            var catList = new WebNodeModel().GetCategories();
            ViewBag.CatagoryID = new SelectList(catList, "CatagoryID");
            ViewBag.EventStream = false;
            if (!String.IsNullOrEmpty(stringSearch))
            {
                //if(EventStream != null){
                //ViewBag.EventStream = Convert.ToBoolean(EventStream);
                var viewModel = new IndexNodeList();                   
                if (!String.IsNullOrWhiteSpace(CatagoryID))
                {
                    var nodeList = await nodeDict.GetResult(CatagoryID.ToLower(), stringSearch);
                    //Return View as a nodeList (<LIST>)
                    return View(nodeList);
                }
                else
                {
                    var nodeList = await nodeDict.GetResult(null, stringSearch);
                    //Return View as a nodeList (<LIST>)
                    return View(nodeList);
                }                                      
             }                    
            return View(await nodeDict.GetResult(CatagoryID, stringSearch));
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
