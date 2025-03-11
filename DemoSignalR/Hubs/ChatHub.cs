using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace DemoSignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Guid roomId, string user, string message)
        {
            await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinRoom(Guid roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        }

        public async Task LeaveRoom(Guid roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
        }
    }
}
