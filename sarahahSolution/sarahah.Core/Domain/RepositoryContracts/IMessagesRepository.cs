using sarahah.Core.Domain.Entities;

namespace sarahah.Core.Domain.RepositoryContracts
{
    public interface IMessagesRepository
    {
        Task<Message> AddMessageAsync(Message message);

        Task<IEnumerable<Message>> GetMessagesByReceiverIdAsync(Guid receiverId);

        Task<bool> DeleteMessage(Guid messageId);

        Task<bool>DeleteAllMessage(Guid userId);

        Task SaveChangesAsync();
        Task<int> MarkAllAsReadDbAsync(Guid userId);


    }
}
