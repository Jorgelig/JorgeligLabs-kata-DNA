using JorgeligLabs.Kata.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JorgeligLabs.Kata.DNA.Core.Services
{
    public class EvaluationService : IEvaluationService
    {
        private const string AllowedCharacters = "ATCG";
        private const int MaxContinuosCharacterAllowedCount = 4;
        public EvaluationService()
        {

        }

        internal bool IsValidSecuence(string[] secuence)
        {
            var secuenceLength = secuence.Length;
            var validLinesCounter = secuence.Where(i => IsValidLine(i)).Count();
            
            return validLinesCounter > 0 && validLinesCounter == secuenceLength;
        }

        internal bool IsValidLine(string line)
        {
            var validCharCounter = line.ToList().Where(i => AllowedCharacters.Contains(i)).Count();
            var secuenceLength = line.Length;
           
            return validCharCounter > 0 && validCharCounter == secuenceLength;
        }

        internal bool IsSymetricMatrix(string[] secuence) => secuence?.Length == secuence?[0]?.Length;

        internal string? GetFirstHorizontalLine(string[] secuence) => GetHorizontalLine(secuence, 0);
        internal string? GetHorizontalLine(string[] secuence, int rowIndex) => secuence[rowIndex];

        internal string? GetFirstVerticalLine(string[] secuence) => GetVerticalLine(secuence, 0);

        internal string? GetVerticalLine(string[] secuence, int columnIndex)
        {
            var line = "";
            for (int i = 0; i < secuence[columnIndex].Length; i++)
                line = line + secuence[i].Substring(columnIndex, 1);
            return line;
        }

        /// <summary>
        /// "\"
        /// </summary>
        /// <param name="secuence"></param>
        /// <returns></returns>
        internal string? GetInvertedDiagonalLine(string[] secuence)
        {
            var line = "";
            for (int i = 0; i < secuence.Length; i++)
                line = line + secuence[i].Substring(i, 1);
            return line;
        }

        /// <summary>
        /// "/"
        /// </summary>
        /// <param name="secuence"></param>
        /// <returns></returns>
        internal string? GetDiagonalLine(string[] secuence)
        {
            var line = "";
            int rowIndex = 0;
            int columnIndex = secuence[rowIndex].Length - 1;
            for (int i = columnIndex; columnIndex >= 0; columnIndex--)
            {
                line += secuence[rowIndex].Substring(columnIndex, 1);
                rowIndex++;
            }

            return line;
        }

        internal int GetContinuosCharacterCount(string line)
        {
            var count = 0;
            var maxLength = line.Length - 1;
            
            for (int i = 0; i < maxLength; i++)
            {
                var char1 = line.Substring(i, 1);
                var char2 = line.Substring(i + 1, 1);
                if (char1 == char2)
                    count++;
            }
            return count;
        }

        internal int HorizontalMutationsCount(string[] dna)
        {
            var maxHorizontalLines = dna?.Length;
            var horizontalMutations = 0;
            for (int i = 0; i < maxHorizontalLines - 1; i++)
            {
                var line = GetHorizontalLine(dna, i);
                if(GetContinuosCharacterCount(line) >= MaxContinuosCharacterAllowedCount)
                    horizontalMutations++;
            }
            return horizontalMutations;
        }

        internal int VerticalMutationsCount(string[] dna)
        {
            var maxVerticalLines = dna[0]?.Length;
            var verticalMutations = 0;
            for (int i = 0; i < maxVerticalLines; i++)
            {
                var line = GetVerticalLine(dna, i);
                if(GetContinuosCharacterCount(line) >= MaxContinuosCharacterAllowedCount)
                    verticalMutations++;
            }
            return verticalMutations;
        }

        internal int ObliqueMutationsCount(string[] dna)
        {
            var obliqueMutations = 0;
            var diagonalLine = GetDiagonalLine(dna);
            var invertedDiagonalLine = GetInvertedDiagonalLine(dna);
            if (GetContinuosCharacterCount(diagonalLine) >= MaxContinuosCharacterAllowedCount)
                obliqueMutations++;
            if(GetContinuosCharacterCount(invertedDiagonalLine) >= MaxContinuosCharacterAllowedCount)
                obliqueMutations++; 
            return obliqueMutations;
        }

        public bool HasMutation(string[] dna)
        {
            if(!IsSymetricMatrix(dna) || !IsValidSecuence(dna)) throw new ArgumentException(nameof(dna));
            var mutationsCount = HorizontalMutationsCount(dna) + VerticalMutationsCount(dna) + ObliqueMutationsCount(dna);
            return mutationsCount > 0;
        }
    }

    public enum DirectionType
    {
        Horizontal,
        Vertical,
        Diagonal,
        DiagonalInverted
    }
}
