using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pentagramm.Data;
using Pentagramm.DTOs.Auth;
using Pentagramm.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pentagramm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AppDbContext appDbContext, UserManager<User> userManager, IConfiguration configuration) : ControllerBase
    {
        private AppDbContext AppDbContext { get; set; } = appDbContext;
        private UserManager<User> UserManager { get; set; } = userManager;
        private IConfiguration Configuration { get; set; } = configuration;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            await using var transaction = await AppDbContext.Database.BeginTransactionAsync();
            if (dto.PhoneNumber == null || dto.NickName == null || dto.Password == null || !ModelState.IsValid)
            {
                transaction.Rollback();
                return BadRequest();
            }

            var user = new User
            {
                PhoneNumber = dto.PhoneNumber,
                Role = dto.Role,
                CreatedAt = DateTime.UtcNow,
                UserName = dto.NickName
            };

            var registerResult = await UserManager.CreateAsync(user, dto.Password);

            if (!registerResult.Succeeded)
            {
                transaction.Rollback();
                return BadRequest(registerResult.Errors);
            }

            await UserManager.AddClaimsAsync(user, [new(ClaimTypes.Name, user.UserName),
                                                    new(ClaimTypes.Sid, user.Id)]);

            var roleResult = await UserManager.AddToRoleAsync(user, user.Role);

            if(!roleResult.Succeeded)
            {
                transaction.Rollback();
                return BadRequest(roleResult.Errors);
            }

            transaction.Commit();

            return CreatedAtAction(nameof(Register), new UserCreatedDto
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                UserName = user.UserName
            });
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers() => Ok( await AppDbContext.Users.Select(user => new
        {
            Username = user.UserName,
            user.Id,
            user.PhoneNumber,
            user.Role
        }).ToListAsync());

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await UserManager.Users.SingleOrDefaultAsync(user => user.PhoneNumber == dto.PhoneNumber);
            
            if(user == null)
            {
                return NotFound();
            }

            var result = await UserManager.CheckPasswordAsync(user, dto.Password);
            
            if(!result)
            {
                return Unauthorized();
            }
            
            var claims = await UserManager.GetClaimsAsync(user);

            var jwtSettings = Configuration.GetSection("Jwt");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = token.ValidTo
            });
        }
    }
}
