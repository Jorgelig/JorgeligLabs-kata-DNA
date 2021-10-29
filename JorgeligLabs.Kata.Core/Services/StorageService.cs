using JorgeligLabs.Kata.DNA.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace JorgeligLabs.Kata.DNA.Core.Services
{
    public class StorageService : IStorageService
    {
        private static MongoClient _client;
        private static IMongoCollection<MutationModel> mutations;
        public StorageService(IConfiguration config)
        {
            var connectionString = config
                .GetSection(nameof(StorageServiceOptions))
                .GetSection(nameof(StorageServiceOptions.ConnectionString)).Value;
            var databaseName = config
                .GetSection(nameof(StorageServiceOptions))
                .GetSection(nameof(StorageServiceOptions.DatabaseName)).Value;
            var mutationCollectionName = config
                .GetSection(nameof(StorageServiceOptions))
                .GetSection(nameof(StorageServiceOptions.MutationCollectionName)).Value;




            _client = new MongoClient(connectionString);
            IMongoDatabase database = _client.GetDatabase(databaseName);
            mutations = database.GetCollection<MutationModel>(mutationCollectionName);
        }

        public MutationModel? Get(string[] dna) => mutations.Find(item => item.Adn == dna).FirstOrDefault();

        public IMutationModel[] GetHumans()
        {
            var items = mutations.Find(item => item.IsMutant == false).ToList();
            return items.ToArray();
        }

        public IMutationModel[] GetMutants()
        {
            var items = mutations.Find(item => item.IsMutant == true).ToList();
            return items.ToArray();
        }

        public IMutationModel InsertOrUpdate(string[] dna, bool isMutant)
        {
            var entity = new MutationModel
            {
                Adn = dna,
                IsMutant = isMutant,
            };
            var saved = Get(dna);
            if (saved == null)
            {
                mutations.InsertOne(entity);
            }
            else
            {
                entity.Id = saved.Id;
                mutations.ReplaceOne(item => item.Id == saved.Id, entity);
            }

            return entity;
        }
    }

    public class MutationModel  : IMutationModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string[] Adn { get; set; }
        public bool IsMutant { get; set; }
        [BsonDateTimeOptions]
        public DateTime Created { get;set;} = DateTime.Now;
        [BsonDateTimeOptions]
        public DateTime Modified { get; set; } = DateTime.Now;

    }
}
