using System.Text.Json.Serialization;

namespace JorgeligLabs.Kata.DNA.Api
{
    public class MutantStatsResponse
    {
        [JsonPropertyName("count_mutations")]
        public int CountMutations { get;set; }
        [JsonPropertyName("count_no_mutation")]

        public int CountNoMutation { get;set; }

        internal int Total => CountMutations + CountNoMutation;

        public decimal Ratio => Total == 0 
            ? 0 
            : CountNoMutation == 0 
                ? 1
                : RatioValue(CountMutations, CountNoMutation);
        private decimal RatioValue(int mutantsCount, int humansCount)
        {
            var round = Convert.ToDecimal(mutantsCount) / Convert.ToDecimal(humansCount);            
            return round;
        }

        //private Func<int, int, decimal> GetRatio 
        //    => (mutantsCount, humansCount) 
        //    => Math.Round(Convert.ToDecimal(mutantsCount) / Convert.ToDecimal(humansCount), 2);
    }
}