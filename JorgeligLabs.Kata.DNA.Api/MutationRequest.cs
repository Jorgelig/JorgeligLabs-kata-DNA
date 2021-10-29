using System.Text.Json.Serialization;

namespace JorgeligLabs.Kata.DNA.Api
{
    public class MutationRequest
    {
        //[JsonPropertyName("dna")]

        public string[] DNA { get; set; }   
    }
}
