using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialApi.Helper;
using SocialApi.Models;
using SocialApi.ViewModels;
using System.Security.Claims;
using ZwajApp.api.Extensions;
using ZwajApp.api.IRepository;
using ZwajApp.api.Models;

namespace SocialApi.Controllers
{
    [Route("api/Users/{userId}/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IUserRepository UserRepository;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public MessagesController(IUserRepository UserRepository, IMapper mapper,UserManager<User> userManager)
        {
            this.UserRepository = UserRepository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet("{id}")] 
        public async Task<IActionResult> GetMessage(string userId,int id)
        {
            if(userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }
            var Message = await UserRepository.GetMessage(id);
            if(Message is null)
                return NotFound();
            return Ok(Message);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessage(string userId, MessageForCreationViewModel messageForCreationViewModel)
        {
             Claim userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string username = userIdClaim?.Value;
            var CurrentUser = await userManager.FindByNameAsync(username);
            if (userId != CurrentUser.Id)
            {
                return Unauthorized();
            }
            messageForCreationViewModel.senderId= userId;
            var Recipient = await userManager.FindByIdAsync(messageForCreationViewModel.recipientId);
            if(Recipient is null)
            {
                return BadRequest("Usr Not Found");
            }
            var message = mapper.Map<Message>(messageForCreationViewModel);
            UserRepository.Add(message);
            var MessageToReturn = mapper.Map<MessageForCreationViewModel>(message);
            await UserRepository.SaveAll();
            Response.Headers.Append("id", message.id.ToString());
            return Ok(MessageToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesForUser(string userId,[FromQuery]MessageParams messageParams)
        {
            Claim userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string username = userIdClaim?.Value;
            var CurrentUser = await userManager.FindByNameAsync(username);
            if (userId != CurrentUser.Id)
            {
                return Unauthorized();
            }
            messageParams.UserId = userId;
            var MessagesFormRepo = await UserRepository.GetMessagesForUser(messageParams);
            var Messages=mapper.Map<IEnumerable<MessageToReturnDto>>(MessagesFormRepo);
            Response.AddPagination
                (
                MessagesFormRepo.CurrentPage,
                MessagesFormRepo.PageSize,
                MessagesFormRepo.TotlaCount,
                MessagesFormRepo.TotalPages
                );
            return Ok(Messages);
        }

        [HttpGet("Chat/{recipientid}")]
        public async Task<IActionResult> GetConversation(string userId,string recipientid)
        {
            Claim userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string username = userIdClaim?.Value;
            var CurrentUser = await userManager.FindByNameAsync(username);
            if (userId != CurrentUser.Id)
            {
                return Unauthorized();
            }
            var Messages=await UserRepository.GetConversation(userId, recipientid);
          var MessageToreturn = mapper.Map<IEnumerable<MessageToReturnDto>>(Messages);

            return Ok(MessageToreturn);
        }


        [HttpGet("count")]
        public async Task<IActionResult> GetUnreadMessagesForUser(string userId)
        {
            Claim userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string username = userIdClaim?.Value;
            var CurrentUser = await userManager.FindByNameAsync(username);

            var count = await UserRepository.GetUnreadMessagesForUser(CurrentUser.Id);
            return Ok(count);
        }
    }
}
