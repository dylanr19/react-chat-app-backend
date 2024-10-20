using System.Net.WebSockets;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using react_chat_app_backend.Migrations;
using react_chat_app_backend.Services;
using react_chat_app_backend.Services.Interfaces;

namespace react_chat_app_backend.Controllers.WSControllers;

public class WSController : ControllerBase
{
    private IWSManager _wsManager;
    private IWSMessageService _wsMessageService;
    private ITokenService _tokenService;
    
    public WSController(IWSMessageService wsMessageService, IWSManager wsManager, ITokenService tokenService)
    {
        _wsManager = wsManager;
        _wsMessageService = wsMessageService;
        _tokenService = tokenService;
    }

    [Route("/ws")]
    public async Task Get()
    {
        string userId = HttpContext.Request.Query["userID"];
        string token = HttpContext.Request.Query["token"];
        
        if (HttpContext.WebSockets.IsWebSocketRequest 
            && _tokenService.IsTokenValid(userId, token)) {
            
            if (!string.IsNullOrEmpty(userId)) {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                _wsManager.Add(userId, webSocket);
                await _wsMessageService.BroadcastOnlineStatus(userId, "online");
                await Listener(webSocket);
            }
        } else {
            
            HttpContext.Response.StatusCode = 400;
        }
    }

    private async Task Listener(WebSocket webSocket)
    {
        while (true)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket
                .ReceiveAsync(new ArraySegment<byte>(buffer),CancellationToken.None);
            
            if (receiveResult.CloseStatus.HasValue)
            {
                var client = _wsManager.Get(webSocket);
                await _wsMessageService.BroadcastOnlineStatus(client.userId, "offline");
                _wsManager.Remove(webSocket);
                
                await webSocket.CloseAsync(
                    receiveResult.CloseStatus.Value,
                    receiveResult.CloseStatusDescription,
                    CancellationToken.None);
                break;   
            }
            await _wsMessageService.Handle(webSocket, buffer);
        }
    }
    
}