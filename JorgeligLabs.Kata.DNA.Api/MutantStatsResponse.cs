using System.Text.Json.Serialization;

namespace JorgeligLabs.Kata.DNA.Api
{
    public class MutantStatsResponse
    {
        [JsonPropertyName("count_mutations")]
        public int CountMutations { get;set; }
        [JsonPropertyName("count_no_mutation")]

        public int CountNoMutation { get;set; }

        public decimal Ratio =>
             CountNoMutation == 0 && CountMutations > 0
             ? CountMutations
             : RatioValue(CountMutations, CountNoMutation);
        private decimal RatioValue(int mutantsCount, int humansCount)
        {
            var round = Math.Round(Convert.ToDecimal(mutantsCount) / Convert.ToDecimal(humansCount), 2);
            return round;
        }

        //private Func<int, int, decimal> GetRatio 
        //    => (mutantsCount, humansCount) 
        //    => Math.Round(Convert.ToDecimal(mutantsCount) / Convert.ToDecimal(humansCount), 2);
    }
}