using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using ZwajApp.api.Models;

namespace SocialApi.Hubs
{
    public class Chathub:Hub
    {
        private readonly UserManager<User> userManager;

        public Chathub(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public void Refresh()
        {
            Clients.All.SendAsync("Data");
        }
    }
}
