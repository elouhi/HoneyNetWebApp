using HoneyNetWebApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoneyNetWebApp.Services
{
    public class MongoNodeRepository : HoneyNetWebApp.Services.IMongoNodeRepository
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<HoneyNetWebApp.Models.WebNodeModel> _nodeCollection;
        static private string connectionString = "mongodb+srv://dbuser:sd6group@cluster0-zrowc.gcp.mongodb.net/test?retryWrites=true";

        public MongoNodeRepository()
        {          
            _client = new MongoClient(connectionString);           
            _database = _client.GetDatabase("data");
            _nodeCollection = _database.GetCollection<HoneyNetWebApp
                .Models.WebNodeModel>("Apr1");
        }

        public async Task<List<HoneyNetWebApp.Models.WebNodeModel>> GetAllNodes()
        {
            return await _nodeCollection.Find(new BsonDocument()).ToListAsync();
        }

        //Returns a list of nodes based on the name and value of the field
        public async Task<List<HoneyNetWebApp.Models.WebNodeModel>> GetNodeByField(string fieldName, string fieldValue){
            var filter = Builders<HoneyNetWebApp.Models.WebNodeModel>.Filter.Eq(fieldName, fieldValue);
            var result = await _nodeCollection.Find(filter).ToListAsync();
            return result;
        }

        //Returns a list of node base on the one index to another for paging
        public async Task<List<HoneyNetWebApp.Models.WebNodeModel>> GetNodesPaging(int startingFrom, int count)
        {
            var result = await _nodeCollection.Find(new BsonDocument())
                .Skip(startingFrom).Limit(count).ToListAsync();
            return result;
        }

        //Returns a list of nodes based on the name and value of the field
        public async Task<List<HoneyNetWebApp.Models.WebNodeModel>> GetNodeByMultiiField(string fieldNameFirst, string fieldValueFirst
            , string fieldNameSecond, string fieldValueSecond)
        {
            var filter = Builders<HoneyNetWebApp.Models.WebNodeModel>.Filter.Eq(fieldNameFirst, fieldValueFirst);
            var filterSecond = Builders<HoneyNetWebApp.Models.WebNodeModel>.Filter.Eq(fieldNameSecond, fieldValueSecond);
            var result = await _nodeCollection.Find(filter & filterSecond).ToListAsync();
            return result;
        }
    }

    public interface IMongoNodeRepository
    {
        //MongoNodeRepository(string connectionString);
        Task<List<HoneyNetWebApp.Models.WebNodeModel>> GetAllNodes();
        Task<List<HoneyNetWebApp.Models.WebNodeModel>> GetNodeByField(string fieldName, string fieldValue);
        Task<List<HoneyNetWebApp.Models.WebNodeModel>> GetNodesPaging(int startingFrom, int count);
        Task<List<HoneyNetWebApp.Models.WebNodeModel>> GetNodeByMultiiField(string fieldNameFirst, string fieldValueFirst
            , string fieldNameSecond, string fieldValueSecond);        
    }
}
