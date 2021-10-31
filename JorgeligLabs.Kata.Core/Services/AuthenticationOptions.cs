using System;
using System.Collections.Generic;
using System.Text;

namespace JorgeligLabs.Kata.DNA.Core.Services
{
    public class AuthenticationOptions
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
