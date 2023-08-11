using ChatApplication.DAL.Db.Context;
using ChatApplication.DAL.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.DAL.Db.Repositories;

public class ConversationRepository
{
    private readonly ApplicationContext _context;

    public ConversationRepository(ApplicationContext context)
    {
        _context = context;
    }

    public ConversationEntity? GetOrCreateUserConversation(Guid userId)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);

        var conversation = _context.Conversations
            .FirstOrDefault(c => c.UserId == userId);

        if (conversation == null)
        {
            conversation = new ConversationEntity
            {
                User = user
            };

            _context.Conversations.Add(conversation);
            _context.SaveChanges();
        }

        return conversation;
    }

    public List<ConversationMessageEntity> GetMessages(Guid conversationId)
    {
        return _context.ConversationMessages
            .Where(m => m.ConversationId == conversationId)
            .ToList();
    }

    public bool AddMessageToConversation(Guid conversationId, ConversationMessageEntity message)
    {
        var conversation = _context.Conversations
            .Include(c => c.ConversationMessages)
            .FirstOrDefault(c => c.Id == conversationId);

        conversation?.ConversationMessages.Add(message);

        _context.SaveChanges();

        return true;
    }
}