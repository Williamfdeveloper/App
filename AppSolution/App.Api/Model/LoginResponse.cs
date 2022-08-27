using Microsoft.AspNetCore.Identity;

namespace App.Model
{
    public class LoginResponse
    {
        public IdentityUser user { get; set; }
        public string token { get; set; }
    }
}
