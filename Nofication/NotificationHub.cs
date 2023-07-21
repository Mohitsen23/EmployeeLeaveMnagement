

using Microsoft.AspNetCore.SignalR;
using Practice.Models;

namespace Practice.Nofication
{
    public class NotificationHub:Hub
    {
        public async Task SendNotification(string user,string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
