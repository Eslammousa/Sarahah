using sarahah.Core.Domain.IdentityEntities;

namespace sarahah.Core.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;


        // 
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;


    }
}
