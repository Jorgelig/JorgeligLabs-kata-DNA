using System.Text.Json.Serialization;

namespace JorgeligLabs.Kata.DNA.Api
{
    public class MutantStatsResponse
    {
        [JsonPropertyName("count_mutations")]
        public int CountMutations { get;set; }
        [JsonPropertyName("count_no_mutation")]

        public int CountNoMutation { get;set; } 

        public decimal Ratio => CountMutations / CountNoMutation;
    }
}