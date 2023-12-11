using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
namespace OttoFlights.Hubs
{
    public class VolHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VolHub(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("VolChange");
        }

        public async Task EnvoyerMessage(string user, string message)
        {
             var userRole = _httpContextAccessor.HttpContext.Session.GetString("UserRole");
            await Clients.All.SendAsync("ReceiveMessage", user, message, userRole, DateTime.Now);
        }
    }
}