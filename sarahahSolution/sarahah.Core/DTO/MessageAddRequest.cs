using sarahah.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace sarahah.Core.DTO
{
    public class MessageAddRequest
    {
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = null!;
        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        public Message ToMessage()
        {
            return new Message
            {
                Content = Content,
                createdAt = createdAt,    
            };
        }
    }
}
