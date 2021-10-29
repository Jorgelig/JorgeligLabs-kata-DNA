using JorgeligLabs.Kata.Core.Interfaces;
using JorgeligLabs.Kata.DNA.Core.Services;
using Xunit;
using FluentAssertions;

namespace JorgeligLabs.Kata.DNA.TDD
{
    public class EvaluationServicesTest
    {
        static string InvalidLine = "XXXXXX";
        static string ValidLine = "ATGCGA";

        string[] InvalidSecuence = new string[] {
            InvalidLine,
            InvalidLine,
            InvalidLine,
            InvalidLine,
            InvalidLine,
            InvalidLine
            };

        string[] WithMutationSecuence = new string[] {
            "ATGCGA",
            "CAGTGC",
            "TTATGT",
            "AGAAGG",
            "CCCCTA",
            "TCACTG"
            };

        IEvaluationService _target = new EvaluationService();

        public EvaluationServicesTest()
        {
            
        }

        [Fact]
        public void IsValidLine_InvalidLine()
        {
            var isValid = _target.IsValidLine(InvalidLine);
            isValid.Should().BeFalse();
        }

        [Fact]
        public void IsValidLine_ValidLine()
        {
            var isValid = _target.IsValidLine(ValidLine);
            isValid.Should().BeTrue(); 
        }

        [Fact]
        public void IsValidSecuence_InvalidSecuence()
        {
            var isValidSecuence = _target.IsValidSecuence(InvalidSecuence);
            isValidSecuence.Should().BeFalse();
        }

        [Fact]
        public void IsValidSecuence_MutationSecuenceIsValid()
        {
            var isValidSecuence = _target.IsValidSecuence(WithMutationSecuence) ?? false;
            isValidSecuence.Should().BeTrue();
        }
    }
}