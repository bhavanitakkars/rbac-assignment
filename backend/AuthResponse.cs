namespace RbacBackend.Models
{
    public class AuthResponse
    {
        public string Username { get; set; } = "";
        public string Role { get; set; } = "";
        public string Token { get; set; } = "";
    }
}
