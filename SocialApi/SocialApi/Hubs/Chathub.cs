using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SocialApi.Models;
using SocialApi.ViewModels;
using System.Security.Claims;
using ZwajApp.api.Data;
using ZwajApp.api.Models;

namespace SocialApi.Hubs
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    [Authorize]
    public class Chathub:Hub
    {
        private readonly UserManager<User> userManager;
        private readonly AppDbContext appDbContext;

        public Chathub(UserManager<User> userManager,AppDbContext appDbContext)
        {
            this.userManager = userManager;
            this.appDbContext = appDbContext;
        }
        public void Refresh(string recipientId, MessageForCreationViewModel _MessageToSend)
        {
            var ConnectionsIdForSameUser = appDbContext.UserConnections.Where(u => u.userId == recipientId).Select(c=>c.connectionId).AsEnumerable();
          
            Clients.Clients(ConnectionsIdForSameUser).SendAsync("Data", _MessageToSend);
        }

        public override async Task OnConnectedAsync()
        {
            
            var username=Context.UserIdentifier;
            var user = await userManager.FindByNameAsync(username);
            var userconnection = new UserConnection()
            {
                connectionId = Context.ConnectionId,
                userId = user.Id
            };
            appDbContext.UserConnections.Add(userconnection);
            await appDbContext.SaveChangesAsync();
             await base.OnConnectedAsync();
        }
        public async Task StoreConnectionidInLoggedin(string email)
        {
            var userid=await userManager.FindByEmailAsync(email);

            var userconnection = new UserConnection()
            {
                connectionId = Context.ConnectionId,
                userId = userid.Id
            };
            appDbContext.Add(userconnection);
            await appDbContext.SaveChangesAsync();     
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionid = Context.ConnectionId;
           var OnlineUser= appDbContext.UserConnections.FirstOrDefault(c=>c.connectionId == connectionid);
            if(OnlineUser is not null)
            {
                appDbContext.Remove(OnlineUser);
                await appDbContext.SaveChangesAsync();
            }



            await base.OnDisconnectedAsync(exception);
        }
    }
}
