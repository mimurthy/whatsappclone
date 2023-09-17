using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseService
{
    public class MongoDbDatabase : IDatabaseAdapter
    {
        public async Task<bool> InsertChatMessage(ChatMessage msg)
        {
            var chatMessageDocument = msg.ToBsonDocument();
            await GetCollection("WhatsApp", "Users").InsertOneAsync(chatMessageDocument);
            return true;
        }

        private IMongoCollection<BsonDocument> GetCollection(string databaseName, string collectionName)
        {
            var client = new MongoClient();
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<BsonDocument>(collectionName);
            return collection;
        }
    }
}
