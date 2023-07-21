using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Practice.Models;
using Practice.Nofication;
using System.Threading.Tasks;

[Route("api/notifications")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationController(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("/notification")]
    public async Task<IActionResult> SendNotification([FromBody] NotificationModel notification)
    { await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification.message);
          return Ok();
    }


}
