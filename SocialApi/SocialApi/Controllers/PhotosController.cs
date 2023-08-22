using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZwajApp.api.IRepository;
using ZwajApp.api.Models;
using ZwajApp.api.ViewModels;

namespace ZwajApp.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoRepository IphotoRepo;
        private readonly UserManager<User> userManager;

        public PhotosController(IPhotoRepository IphotoRepo,UserManager<User> userManager)
        {
            this.IphotoRepo = IphotoRepo;
            this.userManager = userManager;
        }

        [HttpPost("UploadProfilePhoto")]
        public async Task<IActionResult> UploadProfilePhoto([FromForm] PhotoForDetailsDto model)
        {
           var Photo= IphotoRepo.UploadImageProfile(model);
            return Ok(Photo.Result);
        }

        [HttpGet("ImgProfile/{id}")]
        public async Task<IActionResult> ImgProfile(string id)
        {       
            var ImgProdile = await IphotoRepo.ImgProfile(id);
            return Ok(ImgProdile);
        }

        [HttpGet("AllPhotos/{UserId}")]
        public async Task<IActionResult> Allphotos(string UserId)
        {
            var photos=await IphotoRepo.AllPhotos(UserId);
            return Ok(photos);
        }
        [HttpDelete("deleteimage/{id}")]
        public async Task<IActionResult> deleteImage(int id)
        {
            await IphotoRepo.Deleteimage(id);
            return Ok();
        }
        [HttpGet("SetImageProfile/{id}")]
        public async Task<IActionResult> SetImageProfile(int id)
        {
            Claim userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string username = userIdClaim?.Value;
            var CurrentUser = await userManager.FindByNameAsync(username);
            await IphotoRepo.SetImageProfile(id,CurrentUser.Id);
            return Ok();
        }
        [HttpGet("GetUserdata/{id}")]
        public async Task<IActionResult> GetUserData(string id)
        {
            
            var CurrentUser = await userManager.FindByIdAsync(id);
            var image =  await IphotoRepo.ImgProfile(id);
            var Url = "";
            if(image is null)
            {
                Url = "https://localhost:7268/imagesprofile/Empty.jpg";
            }
            else
            {
                Url = image.Url;
            }

            var Allroles = await userManager.GetRolesAsync(CurrentUser);
            var UserData = new
            {
                name=CurrentUser.Name,
                url= Url,
                roles=Allroles,
            };
            return Ok(UserData);
           
        }
    }

}
