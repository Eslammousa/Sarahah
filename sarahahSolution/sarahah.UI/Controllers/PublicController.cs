using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sarahah.Core.Domain.IdentityEntities;
using sarahah.Core.DTO;
using sarahah.Core.ServiceContracts;

namespace sarahah.UI.Controllers
{
    [AllowAnonymous]
    [Route("")]
    public class PublicController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMessageService _messageService;

        public PublicController(UserManager<ApplicationUser> userManager, IMessageService messageService)
        {
            _userManager = userManager;
            _messageService = messageService;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> SendMessage(string username)
        {
            var result = await _userManager.FindByNameAsync(username);

            if (result == null) return NotFound();

            return View("SendMessage", username);
        }

        [HttpPost("{username}")]
        public async Task<IActionResult> SendMessage(MessageAddRequest messageAddRequest, string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var message = await _messageService.AddMessageAsync(messageAddRequest, username);

            if (message == null) return NotFound();

            TempData["Success"] = "Message sent successfully";

            return RedirectToAction(nameof(SendMessage), new { username = username });

        }


        

    }
}
