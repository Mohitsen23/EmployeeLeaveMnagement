
using Microsoft.AspNetCore.SignalR;
using Practice.Models;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
namespace Practice.Nofication
{
    public class NotificationHub:Hub
    {
        private static readonly Dictionary<int, string> _userConnectionMap = new Dictionary<int, string>();
        private  readonly LeaveApplicationContext _leaveApplicationContext;
        public NotificationHub(LeaveApplicationContext leaveApplicationContext)
        {
           
            _leaveApplicationContext= leaveApplicationContext;
        }
        public async Task NewMessage( string message)
        {
            await Clients.All.SendAsync("messageReceived",message);
        }

        public override async Task OnConnectedAsync()
        {
            var identity = GetIdentityFromContext();


            if (identity != null)
            {
                
                string userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var connectionId = Context.ConnectionId;
                var connect = new UserIdentity
                {
                    UserId = int.Parse(userId),
                    ConnectionId = connectionId
                };
                _leaveApplicationContext.Connections.Add(connect);
                _leaveApplicationContext.SaveChanges();
                 _userConnectionMap[int.Parse( userId)] = connectionId;
                await Clients.All.SendAsync("Connected",Context.ConnectionId, userId);
            }
            await base.OnConnectedAsync();

        }
        private List<UserIdentity> userIdentity;
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var identity = GetIdentityFromContext();
            if (identity != null)
            {
                string userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                userIdentity = _leaveApplicationContext.Connections
               .Where(user => user.UserId == int.Parse(userId))
               .ToList();
                if (userIdentity != null)
                {
                    foreach (var user in userIdentity)
                    {
                        _leaveApplicationContext.Connections.Remove(user);
                        _leaveApplicationContext.SaveChanges();
                    }
                    await Clients.All.SendAsync("UserOffline", Context.ConnectionId);
                }
                await base.OnDisconnectedAsync(exception);
            }
        }
        public async Task SendImages(int senderid, int receiverid, string imagebase64)
        { List<UserIdentity> receiverConnectionId = GetReceiverConnectionId(receiverid);
            if (receiverConnectionId != null)
            {foreach (var Users in receiverConnectionId)
              await Clients.Client(Users.ConnectionId).SendAsync("ReceiveImages", senderid, receiverid, imagebase64);
            }
 }
        
      public async Task SendMessagetoBot(string message)
        { string response = GetChatbotResponse(message);
           await Clients.Caller.SendAsync("ReceiveBotMessage", response);
          }

          private string GetChatbotResponse(string message)
           { if (message.ToLower().Contains("hello"))
            {
                return "Hi there!";
            }
            else if (message.ToLower().Contains("how are you"))
            {
                return "I'm just a chatbot, How can I assist you?  ";
            }
            else if (message.ToLower().Contains("my email"))
            {
                return "details";
            }
            else if (message.ToLower().Contains("my employees"))
            {
                return "employee";
            }
            else
            {
                return "I'm sorry, I don't understand. Can you rephrase your message?";
            }
            }

         public async Task SendMessage(int senderid,int receiverid, string message)
            {
            List<UserIdentity> receiverConnectionId = GetReceiverConnectionId(receiverid);
            if (receiverConnectionId != null)
            {foreach(var Users in receiverConnectionId)
             await Clients.Client(Users.ConnectionId).SendAsync("ReceiveMessage", senderid, receiverid, message);
            }
           }

        private List<UserIdentity> userIdentities;
           private List<UserIdentity> GetReceiverConnectionId(int receiverId)
             {
          userIdentities = _leaveApplicationContext.Connections
           .Where(user => user.UserId == receiverId)
             .ToList();

            if (userIdentities != null)
            {
                return userIdentities;
            }
            return null;
             }

        private ClaimsPrincipal GetIdentityFromContext()
            {
             var httpContext = Context.GetHttpContext();
             var token = httpContext.Request.Query["access_token"];

            if (!string.IsNullOrEmpty(token))
            {

                var tokenHandler = new JwtSecurityTokenHandler();

                try
                {
                    var jwtToken = tokenHandler.ReadJwtToken(token);


                    var claims = jwtToken.Claims;


                    return new ClaimsPrincipal(new ClaimsIdentity(claims, "Bearer"));
                }
                catch (Exception ex)
                {

                    return null;
                }

            }
            return null;
        }

        }
}
