namespace ZwajApp.api.ViewModels
{
    public class UserForListDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }

        public string? Gender { get; set; }

        public int Age { get; set; }

        public string? KnownAS { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string? City { get; set; }
        public string? Country { get; set; }

        public string? PhotoUrl { get; set; }
        public string? Interestes { get; set; }
        public bool isLikedByCurrentUser { get; set; }
    }
}