using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace JorgeligLabs.Kata.DNA.Core.Interfaces
{
    [ExcludeFromCodeCoverage]
    public class StorageServiceOptions
    {
        public string MutationCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }   
    }
    public interface IStorageService
    {
        public IMutationModel InsertOrUpdate(string[] dna, bool isMutant);
        public IMutationModel[] GetMutants();
        public IMutationModel[] GetHumans();
    }

    public interface IMutationModel
    {
        public string[] Adn { get;set;}
        public bool IsMutant { get; set; }  
    }
}
