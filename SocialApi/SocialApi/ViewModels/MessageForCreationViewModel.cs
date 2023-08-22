using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace SocialApi.ViewModels
{
    public class MessageForCreationViewModel
    {
        public string senderId { get; set; }
        public string recipientId { get; set; }
        
        public DateTime MessageSend { get; set; }

        public string content { get; set; }
        public MessageForCreationViewModel()
        {
            this.MessageSend = DateTime.Now;
        }
    }
}

