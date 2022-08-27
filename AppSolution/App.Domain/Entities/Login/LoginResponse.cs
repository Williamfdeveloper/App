namespace App.Domain.Entities.Login
{
    public class LoginResponse
    {
        public Usuario user { get; set; }
        public string token { get; set; }
    }
}
