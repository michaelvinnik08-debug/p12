using DBL;
using Microsoft.AspNetCore.SignalR;

namespace Tune.site.Hubs
{
    public class ChatHub : Hub
    {
        private readonly MessagesDB _messagesDB;

        public ChatHub(MessagesDB messagesDB)
        {
            _messagesDB = messagesDB;
        }

        public async Task JoinChatGroup(int chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Chat_{chatId}");
        }

        public async Task SendMessage(int chatId, int userId, string text, DateTime time)
        {
            await Clients.Group($"Chat_{chatId}")
                .SendAsync("ReceiveMessage", chatId, userId, text, time);
        }

        public async Task DeleteMessage(int messageId, int chatId)
        {
            // Delete the message from the database
            await _messagesDB.Delete(messageId);

            // Notify clients in the chat group
            await Clients.Group($"Chat_{chatId}")
                .SendAsync("MessageDeleted", messageId);
        }
    }
}
