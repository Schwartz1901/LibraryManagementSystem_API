using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace LibraryManagementSystem_API.Hubs
{
    public class NotificationHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> UserConnections = new ConcurrentDictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            string userId = Context.UserIdentifier;
            UserConnections[userId] = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            string userId = Context.UserIdentifier;
            UserConnections.TryRemove(userId, out _);
            return base.OnDisconnectedAsync(exception);
        }

        public static string GetConnectionId(string userId)
        {
            return UserConnections.TryGetValue(userId, out var connectionId) ? connectionId : userId;
        }
    }
}
