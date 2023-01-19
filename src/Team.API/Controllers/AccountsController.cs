using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Team.API.Requests;
using Team.Application.Contracts.Services;
using Team.Application.Dtos;
using Team.Domain.Entities.Identity;

namespace Team.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpGet("user-info")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (email == null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByEmailAsync(email);

            return Ok(new UserDto
            {
                Email = user.Email,
                Name = user.Name,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            });
        }

        [HttpGet]


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }


            return Ok(new UserDto
            {
                Email = user.Email,
                Name = user.Name,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUserRequest request)
        {
            var user = new AppUser
            {
                Email = request.Email,
                Name = request.Name,
                UserName = request.Username,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok(new UserDto
            {
                Email = user.Email,
                Name = user.Name,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            });
        }
    }
}
