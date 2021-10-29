using System;
using System.Collections.Generic;
using System.Text;

namespace JorgeligLabs.Kata.DNA.Core.Interfaces
{
    public interface IStorageService
    {
        public void InsertOrUpdate(IMutationModel model);
        public IMutationModel[] GetMutants();
        public IMutationModel[] GetHumans();
    }

    public interface IMutationModel
    {
        public string[] Adn { get;set;}
        public bool IsMutant { get; set; }  
    }
}
