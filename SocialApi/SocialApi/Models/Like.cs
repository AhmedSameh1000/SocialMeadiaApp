using ZwajApp.api.Models;

namespace SocialApi.Models
{
    public class Like
    {
        public string LikerId { get; set; }
        public string LikeeId { get; set; }

        public User Liker { get; set; }

        public User Likee { get; set; }

    }
}
