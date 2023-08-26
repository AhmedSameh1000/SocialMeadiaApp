using ZwajApp.api.Models;

namespace SocialApi.Models
{
    public class UserConnection
    {
        public string userId {  get; set; }

        public string connectionId { get; set; }


        public User User { get; set; }
    }
}
