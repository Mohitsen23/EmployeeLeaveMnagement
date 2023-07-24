

using Microsoft.AspNetCore.SignalR;
using Practice.Models;

namespace Practice.Nofication
{
    public class NotificationHub:Hub
    {
        public async Task NewMessage( string message)
        {
            await Clients.All.SendAsync("messageReceived",message);
        }
    }
}
