using ZwajApp.api.Models;

namespace SocialApi.Models
{
    public class Message
    {
        public int id { get; set; }
    
        public string senderId { get; set; }
        public User sender { get; set; }
    
        public string recipientId { get; set; }

        public User recipient { get; set; }
        public string content { get; set; }
        public bool isRead { get; set; }

        public DateTime? dateRead { get; set; } 

        public DateTime MessageSend { get; set; }



        public bool senderDeleted { get; set; }
        public bool recipientDeleted { get; set; }

    }

}
