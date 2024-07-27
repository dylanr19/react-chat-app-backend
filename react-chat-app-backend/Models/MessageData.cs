using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace react_chat_app_backend.Models;

public class MessageData
{
    [Key] 
    public Guid id { get; set; }
    public DateTime date { get; set; }
    
    [DisallowNull]
    public string senderId { get; set; }
    [DisallowNull]
    public string receiverId { get; set; }
    [DisallowNull]
    public string text { get; set; }

    public MessageData()
    {
        id = Guid.NewGuid();
        date = DateTime.UtcNow;
    }
    
}