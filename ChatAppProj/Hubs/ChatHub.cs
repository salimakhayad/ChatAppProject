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
        private readonly IUserClaimsPrincipalFactory<Profile> _claimsPrincipalFactory;
        private readonly IChatService _chatService;

        public ChatHub(IChatService service,IUserClaimsPrincipalFactory<Profile> claimsPrincipalFactory)
        {
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _chatService = service;
          
        }

        public string GetConnectionId() => Context.ConnectionId;
        
        public override Task OnDisconnectedAsync(Exception ex)
        {
            var profileId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var profile = _chatService.GetAllProfiles().Where(x => x.Id == profileId).FirstOrDefault();
            
            var tr = _chatService.GetAllTimeRegistrations()
                .FirstOrDefault(tr => tr.ProfileId == profileId);
            
            tr.IsOnline = false;
            _chatService.SaveChanges();

           // Clients.All.SendAsync("setUserOffline", profile.UserName);

            var trs = _chatService.GetAllTimeRegistrations().Where(t => t.ChatId == tr.ChatId && tr.IsOnline);

            var usersCurrentlyOnline = trs.Select(tr => tr.ProfileName);

            Clients.Group(tr.ChatId.ToString()).
            SendAsync("UserLeftChannel", 
            new
            {
                ProfileName = profile.UserName,
                ProfileId = profile.Id  
            });

            // Clients.Group(tr.ChatId.ToString()).
            // SendAsync("UpdateUsersOnline", usersCurrentlyOnline);


            return base.OnDisconnectedAsync(ex);
           
        }
    }
}
