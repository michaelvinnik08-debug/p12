using DBL;
using Microsoft.AspNetCore.SignalR;
using Moduls;

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

   
        public async Task SendMessage(int chatId, int userId, string text, DateTime time, int messageId)
        {
            await Clients.Group($"Chat_{chatId}")
                .SendAsync("ReceiveMessage", chatId, userId, text, time, messageId);
        }

        public async Task DeleteMessage(int messageId, int chatId)
        {
            // Delete the message from the database
            await _messagesDB.Delete(messageId);

            // Get the new last message for this chat
            var remainingMessages = await _messagesDB.SelectMessagesByChatId(chatId);
            var newLastMessage = remainingMessages.OrderByDescending(m => m.Time).FirstOrDefault();

            // Notify clients in the chat group
            await Clients.Group($"Chat_{chatId}")
                .SendAsync("MessageDeleted", messageId,
                    newLastMessage?.text ?? "",
                    newLastMessage?.Time ?? DateTime.MinValue);
        }
        public async Task UpdateMessage(int messageId, int chatId, string newText)
        {
            // Update the message in the database
            await _messagesDB.UpdateMessageText(messageId, newText);

            // Get all messages to find the last one
            var allMessages = await _messagesDB.SelectMessagesByChatId(chatId);
            var lastMessage = allMessages.OrderByDescending(m => m.Time).FirstOrDefault();

            // Notify all clients in the chat group
            await Clients.Group($"Chat_{chatId}")
                .SendAsync("MessageUpdated",
                    messageId,
                    newText,
                    lastMessage?.id == messageId ? newText : lastMessage?.text ?? "");
        }
    }
}