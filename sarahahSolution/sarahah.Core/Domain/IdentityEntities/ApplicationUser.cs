using Microsoft.AspNetCore.Identity;
using sarahah.Core.Domain.Entities;
using sarahah.Core.Enums;

namespace sarahah.Core.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
     

        public ICollection<Message> messages { get; set; } = new List<Message>();



    }
}
