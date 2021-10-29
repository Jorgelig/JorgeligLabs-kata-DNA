using JorgeligLabs.Kata.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JorgeligLabs.Kata.DNA.Core.Services
{
    internal class EvaluationService : IEvaluationService
    {
        private const string AllowedCharacters = "ATCG";
        public EvaluationService()
        {

        }

        public bool? IsValidSecuence(string[] secuence)
        {
            var secuenceLength = secuence.Length;
            var validLinesCounter = secuence.Where(i => IsValidLine(i)).Count();
            
            return validLinesCounter > 0 && validLinesCounter == secuenceLength;
        }

        public bool IsValidLine(string line)
        {
            var validCharCounter = line.ToList().Where(i => AllowedCharacters.Contains(i)).Count();
            var secuenceLength = line.Length;
           
            return validCharCounter > 0 && validCharCounter == secuenceLength;
        }
    }
}
