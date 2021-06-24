using System;
using System.Collections.Generic;
using Catalog.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IInMemItemsRepository
    {
        private const string databaseName = "CatalogDB";

        private const string collectionName = "items";

        private readonly IMongoCollection<Item> itemsCollection;

        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(collectionName);
        }

        public async Task CreateItemAsync(Item item)
        {
            await itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, id);

            await itemsCollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);

            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);

            await itemsCollection.ReplaceOneAsync(filter, item);
        }


        ///Mongo DB Nuget Package Installation from Nuget
        /// ******dotnet add package MongoDB.Driver******
        /// To configure docker for Mongo DB
        ///***** docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
    }
}