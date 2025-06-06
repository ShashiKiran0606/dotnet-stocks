using dotnet_stocks_api.Interfaces;
using dotnet_stocks_api.Models;
using dotnet_stocks_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_stocks_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        public IProfileService _profileService;

        public IJwtService _jwtService;

        public ProfileController(IProfileService profileService, IJwtService jwtService )
        {
            _profileService = profileService;

            _jwtService = jwtService;
        }
        
        [HttpGet("")]
        public List<ProfileModel> getAllProfiles()
        {
            return _profileService.GetProfiles();
        }
        [Authorize]
        [HttpPost("")]
        public IActionResult InsertProfile([FromBody] ProfileModel profile)
        {
            if (String.IsNullOrEmpty(profile.FirstName) || String.IsNullOrEmpty(profile.LastName))
            {
                throw new ArgumentNullException("FirstName or LastName Cannot be null.");
            }

            try
            {
                var response = _profileService.CreateProfile(profile);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok("Successfully Created Profile.");
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginObject)
        {
            if (String.IsNullOrEmpty(loginObject.Username) || String.IsNullOrEmpty(loginObject.Password))
            {
                return BadRequest();
            }

            if(loginObject.Username != "admin" ||  loginObject.Password != "password")
            {
                return Unauthorized();
            }

            var token = _jwtService.GenerateToken(loginObject.Username);
            return Ok(token);
        }
    }
}
