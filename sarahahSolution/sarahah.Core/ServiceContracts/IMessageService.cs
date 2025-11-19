using sarahah.Core.DTO;

namespace sarahah.Core.ServiceContracts
{
    public interface IMessageService
    {
        Task<MessageResponse> AddMessageAsync(MessageAddRequest messageAddRequest , string username);

        Task<IEnumerable<MessageResponse>> GetMessagesAsync(Guid Id);

        Task<bool> DeleteMessage(Guid messageId);

        Task<bool> DeleteAllMessages(Guid userId);







    }
}
