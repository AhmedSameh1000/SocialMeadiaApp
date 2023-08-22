namespace ZwajApp.api.Models
{
    public class AuthModel
    {
        public string? Message { get; set; }
        public bool isAuthenticated { set; get; }
        public string? UsreName { get; set; }
        public string? Email { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }
        public string? Name { get; set; }
    }
}