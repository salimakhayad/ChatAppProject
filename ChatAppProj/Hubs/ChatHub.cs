using ChatApp.Data;
using ChatApp.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService service)
        {
            _chatService = service;
        }

        public string GetConnectionId() => Context.ConnectionId;
        
        public override Task OnDisconnectedAsync(Exception ex)
        {
            var profileId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var profile = _chatService.GetAllProfiles().Where(x => x.Id == profileId).FirstOrDefault();
            
            var tr = _chatService.GetAllTimeRegistrations()
                .FirstOrDefault(tr => tr.ProfileId == profileId);
            
            tr.EndTime = DateTime.Now;
            _chatService.SaveChanges();

            var trs = _chatService.GetAllTimeRegistrations().Where(t => t.ChatId == tr.ChatId && tr.EndTime == null);
            var channelId = _chatService.GetAllChannels().FirstOrDefault(c => c.ChatId == tr.ChatId).Id.ToString();

            Clients.Group(channelId).
            SendAsync("UserLeftChannel", 
            new
            {
                ProfileName = profile.UserName,
                ProfileId = profile.Id  
            });

            return base.OnDisconnectedAsync(ex);
           
        }
    }
}
