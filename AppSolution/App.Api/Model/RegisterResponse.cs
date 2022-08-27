using Microsoft.AspNetCore.Identity;

namespace App.Api.Model
{
    public class RegisterResponse
    {
        public IdentityUser user { get; set; }
        public string urlConfirmarEmail { get; set; }
    }
}
