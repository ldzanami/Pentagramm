using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pentagramm.Data;
using Pentagramm.DTOs.Chat;
using Pentagramm.Models.Entities;

namespace Pentagramm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatsController(AppDbContext appDbContext) : ControllerBase
    {
        private AppDbContext AppDbContext { get; set; } = appDbContext;

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatDto dto)
        {
            if(dto.Name == null)
            {
                return BadRequest();
            }

            var chat = new Chat
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                Name = dto.Name
            };

            await AppDbContext.Chats.AddAsync(chat);
            await AppDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateChat), new GetChatDto
            {
                Id = chat.Id,
                CreatedAt = chat.CreatedAt,
                UpdatedAt = chat.UpdatedAt,
                Name = chat.Name
            });
        }

        [HttpGet("chats")]
        public async Task<IActionResult> GetChats() => Ok(new GetChatsDto
        {
            Chats = await AppDbContext.Chats.Select(chat => new GetChatDto
            {
                Id = chat.Id,
                Name = chat.Name,
                CreatedAt = chat.CreatedAt,
                UpdatedAt = chat.UpdatedAt
            }).ToListAsync()
        });

        [HttpDelete("{chatId}")]
        public async Task<IActionResult> DeteteChat([FromRoute] string chatId)
        {
            if(chatId == null)
            {
                return BadRequest();
            }

            var check = await AppDbContext.CheckForNull(chatId: chatId);

            if (check != null)
            {
                return NotFound(check);
            }

            var chat = await AppDbContext.Chats.FindAsync(chatId);

            AppDbContext.Chats.Remove(chat);
            await AppDbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{chatId}")]
        public async Task<IActionResult> UpdateChat([FromRoute] string chatId, [FromBody] UpdateChatDto dto)
        {
            if(dto.Name == null)
            {
                return BadRequest();
            }

            var check = await AppDbContext.CheckForNull(chatId: chatId);

            if (check != null)
            {
                return NotFound(check);
            }

            var chat = await AppDbContext.Chats.FindAsync(chatId);

            chat.Name = dto.Name;

            await AppDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
