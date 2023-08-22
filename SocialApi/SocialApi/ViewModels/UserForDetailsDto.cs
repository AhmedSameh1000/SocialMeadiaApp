namespace ZwajApp.api.ViewModels
{
    public class UserForDetailsDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string DataOfBirth { get; set; }
        public string? Gender { get; set; }

        public int Age { get; set; }

        public string? KnownAS { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }
        public string? Introduction { get; set; }
        public string? LookingFor { get; set; }
        public string? Interestes { get; set; }

        public string? City { get; set; }
        public string? Country { get; set; }

        public string? PhotoUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<PhotoForReturnDto>? Photos { get; set; }

        public bool? isLikedByCurrentUser { get; set; }



    }
}