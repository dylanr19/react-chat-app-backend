using System.Text.Json.Serialization;

namespace react_chat_app_backend.Models;

public class MessageHistory
{
    public List<MessageData> messages { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MessageType type { get; set; }
}