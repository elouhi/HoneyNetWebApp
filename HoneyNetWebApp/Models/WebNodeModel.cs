using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HoneyNetWebApp.Models
{
    public class WebNodeModel
    {
        [BsonId]
        public ObjectId Id { get; set; }   
        
        [BsonElement("dpt")]
        public string DPT { get; set; }

        [BsonElement("proto")]
        public string PROTO { get; set; }

        [BsonElement("srcip")]
        [BsonDefaultValue("N/A")]
        public string SRC { get; set; }

        [BsonElement("ttl")]
        public string TTL { get; set; }

        [BsonElement("windowsize")]
        public string Window { get; set; }   
        
        [BsonElement("nodename")]
        public string Node { get; set; }     
        
        [BsonElement("time")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Time {get; set;}

        [BsonElement("countrycode")]
        public string countryCode { get; set; }

        [BsonElement("city")]
        public string City { get; set; }
        //Bson Type is a Double, cannot deserialize into a string type,
        //Not able to consume a null as a double either
      //  [BsonRepresentation(BsonType.Double)]
        [BsonElement("lat")]              
        public object Lat { get; set; }
       //[BsonRepresentation(BsonType.Double)]
        [BsonElement("long")]
        public object Long { get; set; }

        [BsonElement("rdns")]
        public string Rdns { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("code")]
        public string Code { get; set; }

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

        //Add net missing fields
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",
                DPT, PROTO, SRC, TTL, Window, DPT, Node,
                Time);
        }

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
            {//("N/A {0}" + ex)
                return test = null;
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
 
 