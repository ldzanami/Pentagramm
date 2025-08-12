using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pentagramm.Data;
using Pentagramm.DTOs.Message;

namespace Pentagramm.Controllers
{
    [ApiController]
    [Route("api/chat")]
    [Authorize]
    public class MessagesController(AppDbContext appDbContext) : ControllerBase
    {
        private AppDbContext AppDbContext { get; set; } = appDbContext;

        [HttpGet("{chatId}/messages")]
        public async Task<IActionResult> GetMessages([FromRoute] string chatId, int skip, int take)
        {
            var check = await AppDbContext.CheckForNull(chatId: chatId);

            if(check != null)
            {
                return NotFound(check);
            }

            return Ok(new GetMessagesDto
            {
                ChatId = chatId,
                Messages = await AppDbContext.Messages.Where(mes => mes.ChatId == chatId)
                                                      .Select(mes => new GetMessageDto
                                                      {
                                                          AuthorId = mes.AuthorId,
                                                          AuthorName = mes.AuthorName,
                                                          CreatedAt = mes.CreatedAt,
                                                          UpdatedAt = mes.UpdatedAt,
                                                          Id = mes.Id,
                                                          IsReaded = mes.IsReaded,
                                                          Text = mes.Text
                                                      })
                                                      .OrderBy(mes => mes.CreatedAt)
                                                      .Skip(skip)
                                                      .Take(take)
                                                      .ToListAsync()
            });
        }
    }
}
