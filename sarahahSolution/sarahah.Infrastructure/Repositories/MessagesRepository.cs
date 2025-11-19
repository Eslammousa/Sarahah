using Microsoft.EntityFrameworkCore;
using sarahah.Core.Domain.Entities;
using sarahah.Core.Domain.RepositoryContracts;
using sarahah.Infrastructure.Data.DBContext;

namespace sarahah.Infrastructure.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly AppDbContext _db;
        public MessagesRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<Message> AddMessageAsync(Message message)
        {
            await _db.messages.AddAsync(message);
            await _db.SaveChangesAsync();
            return message;
        }

    

        public async Task<bool> DeleteAllMessage(Guid userId)
        {
            var messages = _db.messages.Where(m => m.UserId == userId);

            if (!messages.Any()) return false;
            _db.messages.RemoveRange(messages);
            return await _db.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteMessage(Guid messageId)
        {
            var msg = await _db.messages.FirstOrDefaultAsync(m => m.Id == messageId);

            if (msg == null) return false;

            _db.messages.Remove(msg);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Message>> GetMessagesByReceiverIdAsync(Guid receiverId)
        {
            return await _db.messages
                .Where(m => m.UserId == receiverId)
                .Include(x => x.User)
                .AsTracking()
                .ToListAsync();

        }

        public async Task<int> MarkAllAsReadDbAsync(Guid userId)
        {
            var messages = _db.messages.Where(m => m.UserId == userId && !m.IsRead);
            await messages.ForEachAsync(m => m.IsRead = true);
            return await _db.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

    }
}
