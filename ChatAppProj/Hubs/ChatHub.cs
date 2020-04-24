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
    
        public override Task OnConnectedAsync()
        {
           // var profileId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
           // var profile = _chatService.GetAllProfiles().Where(x => x.Id == profileId).FirstOrDefault();
           //
           // var profielen = new List<string>();
           //
           // var profiles = _chatService.GetAllProfilesInGroup(groupId);
           // Clients.Group(profiel.Group).SendAsync("UpdateUsersOnline")
           // // probeer nog eens
           //
           // Clients.All.SendAsync("UpdateUsersOnline", UserNames);
           //
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var profileId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var profile = _chatService.GetAllProfiles().Where(x => x.Id == profileId).FirstOrDefault();

            var tr = _chatService.GetAllTimeRegistrations()
                .FirstOrDefault(tr => tr.ProfileId == profileId);
            var chat = _chatService.GetAllChats().FirstOrDefault(c => c.Id == tr.ChatId);
            tr.Chat = chat;
            tr.TimeLeft = DateTime.Now;
            _chatService.SaveChanges();

           // Clients.All.SendAsync("setUserOffline", profile.UserName);

            var trs = _chatService.GetAllTimeRegistrations().Where(tr => tr.ChatId == tr.Chat.Id && tr.TimeLeft == null);
            var usersCurrentlyOnline = new List<string>();
            foreach (var tir in trs)
            {
                var user = _chatService.GetAllProfiles().FirstOrDefault(p => p.Id == tir.ProfileId);
                usersCurrentlyOnline.Add(user.UserName);
            }

            Clients.Group(tr.Chat.Id.ToString()).
            SendAsync("UserLeftChannel", profile.UserName);

            Clients.Group(tr.Chat.Id.ToString()).
            SendAsync("UpdateUsersOnline", usersCurrentlyOnline);


            return base.OnDisconnectedAsync(exception);
           
        }
    }
}
