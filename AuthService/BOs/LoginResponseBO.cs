namespace AuthService.BOs
{
    public class LoginResponseBO
    {
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime Expiry { get; set; }

        public string Username { get; set; }

        public int? UserId { get; set; }
    }
}
