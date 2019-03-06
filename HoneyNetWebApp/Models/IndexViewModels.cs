using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoneyNetWebApp.Models
{
    public class IndexViewModels
    {
        public IndexViewModels()
        {
        }

        public IndexViewModels(int[] catageoryID, MultiSelectList categories)
        {
            CatageoryID = catageoryID;
            Categories = categories;
        }

        public int[] CatageoryID { get; set; }
        public MultiSelectList Categories { get; set; }
    }
}
