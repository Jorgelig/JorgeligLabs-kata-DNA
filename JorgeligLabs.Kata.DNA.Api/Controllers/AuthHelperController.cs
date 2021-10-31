using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace JorgeligLabs.Kata.DNA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthHelperController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        public AuthHelperController(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;

        }
        [HttpGet("get-access-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetToken()
        {
            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri("https://dev-jorgelig.us.auth0.com/");
            //client.DefaultRequestHeaders.Add("content-type", "application/json");
            string stringContent = JsonSerializer.Serialize(new AuthRequest
            {
                client_id = _configuration.GetSection("AuthenticationOptions:ClientId").Value,
                client_secret = _configuration.GetSection("AuthenticationOptions:ClientSecret").Value,
                audience = _configuration.GetSection("AuthenticationOptions:Audience").Value,
                grant_type = "client_credentials"
            });
            var dto = new StringContent(
                stringContent,
                System.Text.Encoding.UTF8, 
                "application/json"
            );
            using var response = await client.PostAsync("oauth/token", dto);

            var content = await response.Content.ReadFromJsonAsync<AuthResponse>();

            return Ok (content);
        }

        public class AuthRequest
        {
            public string client_id { get; set; }    
            public string client_secret { get; set; }
            public string audience { get; set; }
            public string grant_type { get; set; }

        }

        public class AuthResponse
        {
            public string access_token { get;set;}
            public string token_type { get;set;}
        }

    }
}
