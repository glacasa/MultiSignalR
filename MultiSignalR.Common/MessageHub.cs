using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace MultiSignalR.Common
{
    public class MessageHub : Hub
    {
    }

    public class MessageHubContextWrapper
    {
        private readonly IHubContext<MessageHub> hubContext;

        public MessageHubContextWrapper(IHubContext<MessageHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public Task SendMessage(string message)
        {
            return hubContext.Clients.All.SendAsync("Message", message);
        }
    }
}
