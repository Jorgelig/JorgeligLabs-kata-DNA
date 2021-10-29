using JorgeligLabs.Kata.Core.Interfaces;
using JorgeligLabs.Kata.DNA.Core.Services;
using Xunit;
using FluentAssertions;

namespace JorgeligLabs.Kata.DNA.TDD.Test
{
    public class EvaluationServicesTest
    {
        static string InvalidLine = "XXXXXX";
        static string ValidLine = "ATGCGA";
        static string FiveMatchLine = "AAAAAA";
        static string SixMatchLine = "AXXXXXXX";

        string[] InvalidSecuence = new string[] {
            InvalidLine,
            InvalidLine,
            InvalidLine,
            InvalidLine,
            InvalidLine,
            InvalidLine
            };

        string[] SymetricMatrix = new string[] {
            "ATGCGA",
            "CAGTGC",
            "TTATGT",
            "AGAAGG",
            "CCCCTA",
            "TCACTG"
            };

        string[] AsymetricMatrix = new string[] {
            "ATGCGA",
            "CAGTGC",
            "TTATGT",
            "AGAAGG",
            "CCCCTA",
            "TCACTG",
            "TCACTG"
            };

        string[] WithoutMutationSecuence = new string[] {
            "ATGCGA",
            "CAGTGC",
            "TTATTT",
            "AGACGG",
            "GCGTCA",
            "TCACTG"
            };

        string[] WithMutationSecuence = new string[] {
            "ATGCGA",
            "CAGTGC",
            "TTATGT",
            "AGAAGG",
            "CCCCTA",
            "TCACTG"
            };
      

        string MutationFirstRowlLine = "ATGCGA";
        string MutationSecondRowlLine = "CAGTGC";
        string MutationFirstColumnlLine = "ACTACT";
        string MutationSecondColumnlLine = "TATGCC";
        string MutationInvertedDiagonalLine = "AAAATG";
        string MutationDiagonalLine = "AGTACT";


        EvaluationService _target = new EvaluationService();

        public EvaluationServicesTest()
        {
            
        }

        [Fact]
        public void IsValidMatrix_ValidMatrix()
        {
            var isValid = _target.IsValidMatrix(SymetricMatrix);
            isValid.Should().BeTrue();  
        }

        [Fact]
        public void IsValidMatrix_InvalidMatrix()
        {
            var isValid = _target.IsValidMatrix(AsymetricMatrix);
            isValid.Should().BeFalse();
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
            var isValidSecuence = _target.IsValidSecuence(WithoutMutationSecuence) ?? false;
            isValidSecuence.Should().BeTrue();
        }

        [Fact]
        public void GetFirstHorizontalLine_FirstLine()
        {
            var line = _target.GetFirstHorizontalLine(WithMutationSecuence);
            line?.Should().NotBeNull();
            line?.Should().Be(MutationFirstRowlLine);

        }

        [Fact]
        public void GetFirstVerticalLine_FirstVerticalLine()
        {
            var line = _target.GetFirstVerticalLine(WithMutationSecuence);
            line?.Should().NotBeNull(); 
            line?.Should().Be(MutationFirstColumnlLine);
        }

        [Fact]
        public void GetSecondHorizontalLine_SecondHorizontalLine()
        {
            var line = _target.GetHorizontalLine(WithMutationSecuence, 1);
            line?.Should().NotBeNull();
            line?.Should().Be(MutationSecondRowlLine);

        }

        [Fact]
        public void GetSecondVerticalLine_SecondVerticalLine()
        {
            var line = _target.GetVerticalLine(WithMutationSecuence, 1);
            line?.Should().NotBeNull();
            line?.Should().Be(MutationSecondColumnlLine);
        }

        [Fact]
        public void GetInvertedDiagonalLine_InvertedDiagonalLine()
        {
            var line = _target.GetInvertedDiagonalLine(WithMutationSecuence);
            line?.Should().NotBeNull();
            line?.Should().Be(MutationInvertedDiagonalLine);
        }


        [Fact]
        public void GetDiagonalLine_DiagonalLine()
        {
            var line = _target.GetDiagonalLine(WithMutationSecuence);
            line?.Should().NotBeNull();
            line?.Should().Be(MutationDiagonalLine);
        }

        [Fact]
        public void GetContinuosCharacterCount_FourMatch()
        {
            var line = FiveMatchLine;
            var count = _target.GetContinuosCharacterCount(line);
            count.Should().Be(5);  
        }

        [Fact]
        public void GetContinuosCharacterCount_SixMatch()
        {
            var line = SixMatchLine;
            var count = _target.GetContinuosCharacterCount(line);
            count.Should().Be(6);
        }

        [Fact]
        public void HasMutation_HasMutations()
        {
            var dna = WithMutationSecuence;
            var hasMutation = _target.HasMutation(dna);
            hasMutation.Should().BeTrue();
        }

        [Fact]
        public void HasMutation_WithoutMutations()
        {
            var dna = WithoutMutationSecuence;
            var hasMutation = _target.HasMutation(dna);
            hasMutation.Should().BeFalse();
        }

    }
}