namespace App.Domain.Entities.Register
{
    public class RegisterResponse
    {
        public Usuario user { get; set; }
        public string urlConfirmarEmail { get; set; }
    }
}
