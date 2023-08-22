using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialApi.Helper;
using SocialApi.Models;
using System.Security.Claims;
using ZwajApp.api.Extensions;
using ZwajApp.api.IRepository;
using ZwajApp.api.Models;
using ZwajApp.api.ViewModels;

namespace ZwajApp.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _repo;
        private readonly IMapper _mapper;
        private readonly IUserRepository UserRepository;

        public UsersController(UserManager<User> repo,
         IMapper mapper, IUserRepository UserRepository)
        {
            _mapper = mapper;
            this.UserRepository = UserRepository;
            _repo = repo;
        }

        [HttpPost("{id}/like/{recipientId}")]
        [Authorize]
        public async Task<IActionResult> LikeUser(string id, string recipientId)
        {
            var like = await UserRepository.GetLike(id, recipientId);
            if (like is not null)
            {
                return BadRequest("already liked this User later");
            }
            like = new Like
            {
                LikerId = id,
                LikeeId = recipientId
            };
            var likeeName = _repo.FindByIdAsync(recipientId).Result.Name;
            UserRepository.Add(like);
            if (await UserRepository.SaveAll())
            {
                return Ok();
            }
            else
            {
                return BadRequest("Failed To Like This User");
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserParams userParams)
        {
            Claim userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string username = userIdClaim?.Value;
            var CurrentUser = await _repo.FindByNameAsync(username);
            userParams.UserId = CurrentUser.Id;
            var Users = await UserRepository.GetUsers(userParams);
            ;

            var UsersForReturn = _mapper.Map<IEnumerable<UserForListDto>>(Users);
            Response.AddPagination(Users.CurrentPage, Users.PageSize, Users.TotlaCount, Users.TotalPages);

            foreach (var User in UsersForReturn)
            {
                User.isLikedByCurrentUser = await UserRepository.IsLikeThisUser(CurrentUser.Id, User.Id);
                if (User.PhotoUrl is null)
                {
                    User.PhotoUrl = @"https://localhost:7268/imagesprofile/Empty.jpg";
                }
            }

            return Ok(UsersForReturn);
        }

        [HttpGet("{id}/dislike/{recipientid}")]
        public async Task<IActionResult> dislikeUser(string id, string recipientid)
        {
            var Done = await UserRepository.dislikeThisUser(id, recipientid);
            return Ok(Done);
        }






        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            User? Userr = await _repo.Users.Include(f => f.Photos).FirstOrDefaultAsync(c => c.Id == id);
            Claim userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string username = userIdClaim?.Value;
            var CurrentUser = await _repo.FindByNameAsync(username);

            UserForDetailsDto UserForReturn = _mapper.Map<UserForDetailsDto>(Userr);
            if (UserForReturn.PhotoUrl is null)
            {
                UserForReturn.PhotoUrl = @"https://localhost:7268/imagesprofile/Empty.jpg";
            }
            UserForReturn.isLikedByCurrentUser = await UserRepository.IsLikeThisUser(CurrentUser.Id, Userr.Id);
            return Ok(UserForReturn);

        }

        [HttpPost("UserData/{id}")]
        public async Task<IActionResult> UpdateUserData(string id, UserModel2 Model)
        {
            User? User = await _repo.FindByIdAsync(id);
            if (User is null)
            {
                return NotFound(User);
            }
            User.Name = Model.name;
            User.PhoneNumber = Model.Phone;
            _ = await _repo.UpdateAsync(User);
            return Ok(User);
        }

        [HttpPost("Userplace/{id}")]
        public async Task<IActionResult> UpdateUserplace(string id, UserModel3 Model)
        {
            User? User = await _repo.FindByIdAsync(id);
            if (User is null)
            {
                return NotFound(User);
            }
            User.City = Model.city;
            User.Country = Model.country;
            _ = await _repo.UpdateAsync(User);
            return Ok(User);
        }

        [HttpPost("Userinformations/{id}")]
        public async Task<IActionResult> UpdateUserinformations(string id, UserModel4 Model)
        {
            User? User = await _repo.FindByIdAsync(id);
            if (User is null)
            {
                return NotFound(User);
            }
            User.Interestes = Model.interstes;
            User.LookingFor = Model.LookingFor;
            User.Introduction = Model.introduction;
            User.KnownAS = Model.KnownAs;
            User.DataOfBirth = Model.DateOfBirth.Value;
            _ = await _repo.UpdateAsync(User);
            return Ok(User);
        }

        [HttpPost("ChangePassword/{id}")]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] PasswordModel model)
        {
            User? user = await _repo.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result = await _repo.ChangePasswordAsync(user, model.oldpassword, model.newpassword);

            return !result.Succeeded ? BadRequest(result.Errors) : Ok();
        }

    }
}