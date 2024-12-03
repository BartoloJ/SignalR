
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Concurrent;

internal class ChatHub : Hub
{
    private static readonly ConcurrentDictionary<string, string> ActiveUsers = new();
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task RegisterUser(string user)
    {
        if (ActiveUsers.Values.Contains(user))
        {
            throw new HubException("Username already in use.");
        }

        ActiveUsers[Context.ConnectionId] = user;
        await Clients.Caller.SendAsync("UserRegistered", true);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        ActiveUsers.TryRemove(Context.ConnectionId, out _);
        await base.OnDisconnectedAsync(exception);
    }
}