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
            return await _nodeCollection.Find(new BsonDocument()).Limit(30).ToListAsync();
        }

        //Returns a list of nodes based on the name and value of the field
        public async Task<List<HoneyNetWebApp.Models.WebNodeModel>> GetNodeByField(string fieldName, string fieldValue){
            var filter = Builders<HoneyNetWebApp.Models.WebNodeModel>.Filter.Eq(fieldName, fieldValue);
            var result = await _nodeCollection.Find(filter).Limit(30).ToListAsync();
            return result;
        }

        //Returns a list of node base on the one index to another for paging
        public async Task<List<HoneyNetWebApp.Models.WebNodeModel>> GetNodesPaging(int startingFrom, int count)
        {
            var result = await _nodeCollection.Find(new BsonDocument())
                .Skip(startingFrom).Limit(count).Limit(30).ToListAsync();
            return result;
        }
        /*
         * Takes two field (DateTime) values, converts them to ticks which is the
         * number of 100-nanosecond intervals that have elapsed since 12:00:00 midnight, January 1, 0001
         * then queries the field name and search param for all entries that match the fieldValue inclusively between said dates
         * need to determine if .Ticks (Long) will work
         */
        public async Task<List<HoneyNetWebApp.Models.WebNodeModel>> GetNodesByDateRange (string fieldName, string fieldValue
            , DateTime? startDateTime, DateTime? endDateTime)
        {            
            var builder = Builders<HoneyNetWebApp.Models.WebNodeModel>.Filter;
            var filter = builder.Eq(fieldName, fieldValue) & builder.Lte("time", endDateTime) & builder.Gte("time", startDateTime);
            return await _nodeCollection.Find(filter).Limit(30).ToListAsync();
        }

        //Same as above but for entire collection for a searchterm
        public async Task<List<HoneyNetWebApp.Models.WebNodeModel>> GetAllNodesWithDate(string fieldValue, DateTime? startDateTime, DateTime? endDateTime)
        {
            var builder = Builders<HoneyNetWebApp.Models.WebNodeModel>.Filter;
            var filter = builder.Lte("time", endDateTime) & builder.Gte("time", startDateTime);
            return await _nodeCollection.Find(filter).Limit(30).ToListAsync();
        }

        //Returns a list of nodes based on the name and value of the field
        public async Task<List<HoneyNetWebApp.Models.WebNodeModel>> GetNodeByMultiiField(string fieldNameFirst, string fieldValueFirst
            , string fieldNameSecond, string fieldValueSecond)
        {
            var filter = Builders<HoneyNetWebApp.Models.WebNodeModel>.Filter.Eq(fieldNameFirst, fieldValueFirst);
            var filterSecond = Builders<HoneyNetWebApp.Models.WebNodeModel>.Filter.Eq(fieldNameSecond, fieldValueSecond);
            return  await _nodeCollection.Find(filter & filterSecond).ToListAsync();            
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
        Task<List<HoneyNetWebApp.Models.WebNodeModel>> GetNodesByDateRange(string fieldName, string fieldValue
            , DateTime? startDateTime, DateTime? endDateTime);
        Task<List<HoneyNetWebApp.Models.WebNodeModel>> GetAllNodesWithDate(string fieldValue, DateTime? startDateTime, DateTime? endDateTime);
    }
}
