namespace AuthService.BOs
{
    public class SaveCredentialBO
    {
        public long UserId { get; set; }
        public string Password { get; set; } = string.Empty;

        public string Email { get; set; }
    }
}
