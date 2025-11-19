using sarahah.Core.Domain.Entities;

namespace sarahah.Core.DTO
{
    public class MessageResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime createdAt { get; set; }
        public bool IsRead { get; set; }

        public Guid? ParentMessageId { get; set; }
        public ICollection<MessageResponse> Replies { get; set; } = new List<MessageResponse>();

    }


    public static class MessageExtensions
    {
        public static MessageResponse ToMessageResponse(this Message message)
        {
            return new MessageResponse
            {
                Id = message.Id,
                Content = message.Content,
                createdAt = message.createdAt,
                IsRead = message.IsRead,
            };
        }
    }
    
        
    
}
