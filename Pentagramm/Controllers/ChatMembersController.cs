using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pentagramm.Data;
using Pentagramm.DTOs.Member;
using Pentagramm.Models.Entities;

namespace Pentagramm.Controllers
{
    [ApiController]
    [Route("api/chat")]
    [Authorize]
    public class ChatMembersController(AppDbContext appDbContext) : ControllerBase
    {
        private AppDbContext AppDbContext { get; set; } = appDbContext;

        [HttpPost("{chatId}/members")]
        public async Task<IActionResult> AddMember([FromRoute] string chatId, [FromBody] CreateMemberDto dto)
        {
            var check = await AppDbContext.CheckForNull(chatId: chatId);

            if (check != null)
            {
                return NotFound(check);
            }

            var member = new ChatMember
            {
                ChatId = chatId,
                Role = dto.Role,
                UserId = AppDbContext.GetUserId(User)
            };

            await AppDbContext.ChatMembers.AddAsync(member);
            await AppDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(AddMember), new GetMemberDto
            {
                ChatId = member.ChatId,
                Role = member.Role,
                UserId = member.UserId,
                JoinedAt = member.JoinedAt,
                UpdatedAt = member.UpdatedAt
            });
        }

        [HttpGet("{chatId}/members")]
        public async Task<IActionResult> GetMembers([FromRoute] string chatId)
        {
            var check = await AppDbContext.CheckForNull(chatId: chatId);

            if (check != null)
            {
                return NotFound(check);
            }

            return Ok(new GetMembersDto
            {
                ChatId = chatId,
                Members = await AppDbContext.ChatMembers.Where(mem => mem.ChatId == chatId)
                                                        .Select(mem => new GetMemberDto
                                                        {
                                                            ChatId = chatId,
                                                            UserId = AppDbContext.GetUserId(User),
                                                            JoinedAt= mem.JoinedAt,
                                                            UpdatedAt= mem.UpdatedAt,
                                                            Role = mem.Role
                                                        }).ToListAsync()
            });
        }

        [HttpPatch("{chatId}/members/{memberId}")]
        public async Task<IActionResult> UpdateMember([FromRoute] string chatId, [FromRoute] string memberId, [FromBody] UpdateMemberDto dto)
        {
            var check = await AppDbContext.CheckForNull(chatId: chatId, memberIds: [memberId]);

            if(check != null)
            {
                return NotFound(check);
            }

            var member = await AppDbContext.ChatMembers.FirstOrDefaultAsync(mem => mem.UserId == memberId && mem.ChatId == chatId);

            member.Role = dto.Role;

            await AppDbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{chatId}/members/{memberId}")]
        public async Task<IActionResult> DeleteMember([FromRoute] string chatId, [FromRoute] string memberId)
        {
            var check = await AppDbContext.CheckForNull(chatId: chatId, memberIds: [memberId]);

            if (check != null)
            {
                return NotFound(check);
            }

            var member = await AppDbContext.ChatMembers.FirstOrDefaultAsync(mem => mem.UserId == memberId && mem.ChatId == chatId);

            AppDbContext.ChatMembers.Remove(member);
            await AppDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
