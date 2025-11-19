using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sarahah.Core.Domain.IdentityEntities;
using sarahah.Core.Domain.RepositoryContracts;
using sarahah.Core.DTO;
using sarahah.Core.ServiceContracts;
using sarahah.Core.Services;

namespace sarahah.UI.Controllers
{
    [Route("[Controller]")]
    public class MessageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMessageService _messageService;
     

        public MessageController(UserManager<ApplicationUser> userManager, IMessageService messageService)
        {
            _userManager = userManager;
            _messageService = messageService;
        }

        [Route("[action]/{Id:guid}")]
        public async Task<IActionResult> GetAllMessages(Guid Id)
        {
            if (Id == Guid.Empty) return BadRequest("Invalid user Id");

            var message = await _messageService.GetMessagesAsync(Id);

            return View(message);
        }

        [HttpPost("[action]/{messageId:guid}")]
        public async Task<IActionResult> DeleteMessage(Guid messageId)
        {
           if (messageId == Guid.Empty) return BadRequest("Invalid message Id");

           await _messageService.DeleteMessage(messageId);

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            return RedirectToAction(nameof(GetAllMessages), new { id = userId });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteAllMessage()
        {

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId is null) return BadRequest();
            await _messageService.DeleteAllMessages(Guid.Parse(userId));


            return RedirectToAction(nameof(GetAllMessages), new { id = userId });
        }


    }
    }

