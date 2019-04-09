using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HoneyNetWebApp.Models
{
    //[DebuggerDisplay("WebNodeModel, {DPT, Protocol, IPAddress, TTL, Window, Day, Month, NodeLocation, Time, nq") ]
    public class WebNodeModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("dpt")]
        public string DPT { get; set; }
        [BsonElement("proto")]
        public string PROTO { get; set; }
        [BsonElement("srcip")]
        public string SRC { get; set; }
        [BsonElement("ttl")]
        public string TTL { get; set; }
        [BsonElement("windowsize")]
        public string Window { get; set; }
       /* [BsonElement("day")]
        public string Day { get; set; } 
        [BsonElement("month")]
        public string Month { get; set; }*/
        [BsonElement("nodename")]
        public string Node { get; set; }
        /*[BsonElement("time")]
        public string Time { get; set; }*/
        [BsonDateTimeOptions]
        public DateTime Date {get; set;}
        [BsonExtraElements]
        public IDictionary<string, object> _additionalData { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}",
                DPT, PROTO, SRC, TTL, Window, DPT, Node,
                Date);
        }


        /*typeof(WebNodeModel).GetFields()
                .Select(field => field.Name.ToString())
                .ToList();*/
        public List<string> GetCategories()
        {
            List<string> items = new List<string>();
            foreach ( var prop in new WebNodeModel().GetType().GetProperties())
            {
                items.Add(prop.Name);
            }
            return items;
        }
     
        //TESTING: For testing the deserialization of _additionalData purposes 
        public string NewString()
        {
            string test = "";
            try
            {
                this._additionalData.ToList().ForEach(k => test += (k.Key + ": "+ k.Value +" "));

            }catch(Exception ex)
            {
                return test = ("N/A {0}" + ex);
            }


            return test;
        }
    }

    public class IndexNodeList
    {
        public List<WebNodeModel> NodeList { get; set; }

        public IndexNodeList(List<WebNodeModel> nodeList)
        {
            this.NodeList = nodeList;
        }

        public IndexNodeList()
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    //Model for drop down list
    public class DropDown
    {

        public DropDown()
        {
        }

        public DropDown(int[] catageoryID)
        {
            CatageoryID = catageoryID;
            
        }
        public int[] CatageoryID { get; set; }
        
    }


}
 