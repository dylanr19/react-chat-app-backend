using System.Net.WebSockets;
using react_chat_app_backend.Models;

namespace react_chat_app_backend.Services.Interfaces;

public interface IWSMessageService
{
    Task Handle(WebSocket webSocket, byte[] buffer);
    Task FetchHistory(WebSocket webSocket, byte[] buffer);
    Task StoreAndForward(byte[] buffer);
    Task Store(ChatMessage chatMessage);
    Task Forward(ChatMessage chatMessage);
    Task SendToUser(string userId, string json);
}