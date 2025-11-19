using Microsoft.AspNetCore.Identity;
using sarahah.Core.Domain.Entities;
using sarahah.Core.Domain.IdentityEntities;
using sarahah.Core.Domain.RepositoryContracts;
using sarahah.Core.DTO;
using sarahah.Core.ServiceContracts;
using Services.Helpers;

namespace sarahah.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessagesRepository _messagesRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public MessageService(IMessagesRepository messagesRepository, UserManager<ApplicationUser> userManager)
        {

            _messagesRepository = messagesRepository;
            _userManager = userManager;
        }
        public async Task<MessageResponse> AddMessageAsync(MessageAddRequest messageAddRequest, string username)
        {
            ValidationHelper.ModelValidation(messageAddRequest);

            Message addedMessage = messageAddRequest.ToMessage();

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                throw new Exception("User not found");

            addedMessage.Id = Guid.NewGuid();
            addedMessage.UserId = user.Id;
            addedMessage.createdAt = DateTime.UtcNow;
            addedMessage.IsRead = false;


            await _messagesRepository.AddMessageAsync(addedMessage);
            return addedMessage.ToMessageResponse();

        }

        public async Task<bool> DeleteMessage(Guid messageId)
        {
            return await _messagesRepository.DeleteMessage(messageId);
        }

        public async Task<IEnumerable<MessageResponse>> GetMessagesAsync(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user == null) throw new Exception("User not found");

            var messages = (await _messagesRepository.GetMessagesByReceiverIdAsync(user.Id)).ToList();

            var resultForView = messages
                .Select(m => m.ToMessageResponse())
                .ToList();

            bool updated = false;
            foreach (var msg in messages)
            {
                if (!msg.IsRead)
                {
                    msg.IsRead = true;
                    updated = true;
                }
            }

            if (updated)
                await _messagesRepository.SaveChangesAsync();

            return resultForView;
        }


        public async Task<bool> DeleteAllMessages(Guid userId)
        {
            return await _messagesRepository.DeleteAllMessage(userId);
        }

       
    }
}

