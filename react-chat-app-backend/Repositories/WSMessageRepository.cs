using Microsoft.EntityFrameworkCore;
using react_chat_app_backend.Context;
using react_chat_app_backend.Models;
using react_chat_app_backend.Repositories.Interfaces;

namespace react_chat_app_backend.Repositories;

public class WSMessageRepository : IWSMessageRepository
{
    private AppDbContext _appDbContext;

    public WSMessageRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddMessage(ChatMessage chatMessage)
    {
        _appDbContext.Messages.Add(chatMessage);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<List<ChatMessage>> GetMessages(string userId1, string userId2)
    {
         return await _appDbContext.Messages.Where(m =>
                m.senderId == userId1 && m.receiverId == userId2 ||
                m.senderId == userId2 && m.receiverId == userId1)
            .ToListAsync();
    }
    
}