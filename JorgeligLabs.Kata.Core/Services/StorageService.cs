using JorgeligLabs.Kata.DNA.Core.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace JorgeligLabs.Kata.DNA.Core.Services
{
    public class StorageService : IStorageService
    {
        public IMutationModel[] GetHumans()
        {
            throw new NotImplementedException();
        }

        public IMutationModel[] GetMutants()
        {
            throw new NotImplementedException();
        }

        public void InsertOrUpdate(IMutationModel model)
        {
            var entity = new MutationModel
            {
                Adn = model.Adn,
            };
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
