using System;
using System.Collections.Generic;
using System.Text;

namespace JorgeligLabs.Kata.Core.Interfaces
{
    internal interface IEvaluationService
    {
        public bool? IsValidSecuence(string[] secuence);
        public bool IsValidLine(string line);
    }
}
