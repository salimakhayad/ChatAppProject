using ChatApp.Domain;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Models.Group;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ChatApp.Models.Chat;
using ChatApp.Models.Channel;

namespace ChatApp.Controllers
{
    [Authorize]
    public class ChannelController:Controller
    {
        private readonly IChatService _chatService;
        private readonly IUserStore<Profile> _userStore;
        private readonly IUserClaimsPrincipalFactory<Profile> _claimsPrincipalFactory;
        private readonly SignInManager<Profile> _signInManager;
        private readonly UserManager<Profile> _userManager;


        public ChannelController(IChatService service, IUserStore<Profile> userStore, UserManager<Profile> userManager, IUserClaimsPrincipalFactory<Profile> claimsPrincipalFactory, SignInManager<Profile> signInManager)
        {
            this._chatService = service;
            this._userManager = userManager;
            this._claimsPrincipalFactory = claimsPrincipalFactory;
            this._userStore = userStore;
            this._signInManager = signInManager;
        }
       
        public IActionResult Join(int channelId)
        {
            // set viewbag => Channel - nameOfChannel
            ChannelSelectedViewModel model = new ChannelSelectedViewModel();
 
            var profileId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var profile = _chatService.GetAllProfiles().Where(x => x.Id == profileId).FirstOrDefault();

            var selectedChannel = _chatService.GetAllChannels()
                  .FirstOrDefault(c => c.Id == channelId); 

            var currentGroup = _chatService.GetAllGroups().FirstOrDefault(g => g.Channels.Contains(selectedChannel));
           
            var channels = _chatService.GetAllChannels()
                  .Where(c => c.GroupId == currentGroup.Id);

            model.Photo = currentGroup.Photo;
            // if user is owner

            model.IsOwner = currentGroup.ProfileId == profileId;

            model.OwnerId = currentGroup.ProfileId;



            foreach (var channel in channels)
            {
                var sChannel = _chatService.GetAllChannels()
                 .Where(c => c.Id == channel.Id)
                 .FirstOrDefault();

                var sChat = _chatService.GetAllChats()
                .Where(chat => chat.ChannelId == sChannel.Id)
                .FirstOrDefault();

                var messages = _chatService.GetAllMessages()
               .Where(mes => mes.ChatId == sChat.Id );

                var channelProfiles = _chatService.GetAllChannelProfiles()
                    .Where(cp=>cp.ChannelId==channel.Id);

                sChat.Messages = messages.ToList();
                var profiles = new List<Profile>();
                foreach (var prof in channelProfiles)
                {
                    var profFromDb = _chatService.GetAllProfiles().Where(p => p.Id == prof.ProfileId).FirstOrDefault();
                    profiles.Add(profFromDb);
               
                }
                sChat.Profiles = profiles.ToList();
                if (channel.Id == selectedChannel.Id)
                {
                    selectedChannel.Chat = sChat;
                }
                
                
            }


            model.Group = currentGroup;
            model.Channels = channels.ToList();
            model.Name = currentGroup.Name;
            model.Profile = profile;
            model.ProfileId = profile.Id;
            model.SelectedChannel = selectedChannel;
            model.Id = currentGroup.Id;
        
            return View("Views/Group/ChannelSelected.cshtml", model);
        }
      
        public IActionResult Create(int groupId)
        {
           
            var groupFromDb = _chatService.GetAllGroups().FirstOrDefault(g => g.Id == groupId);

            CreateChannelModel model = new CreateChannelModel()
            {
                groupId = groupId
            };

          
          return View("Views/Channel/Create.cshtml", model);

        }
        [HttpPost]
        public IActionResult Create(CreateChannelModel model)
        {
            if (ModelState.IsValid)
            {
                var group = _chatService.GetAllGroups().FirstOrDefault(g => g.Id == model.groupId);
       
                _chatService.InsertChannel(
                    new Channel()
                    {
                        Name = model.Name,
                        GroupId = group.Id,
                        Group = group
                    });
       
                _chatService.SaveChanges();
       
                var groupFromDbWithChannel = _chatService.GetAllGroups().FirstOrDefault(g => g.Id == group.Id);
       
                var channelFromDb = _chatService.GetAllChannels().FirstOrDefault(c => c.Name == model.Name && c.GroupId == groupFromDbWithChannel.Id);
       
                var newChat = new Chat()
                {
                    ChatType = ChatType.Group,
                    Messages = new List<Message>(),
                    Channel = channelFromDb,
                    ChannelId = channelFromDb.Id
                };
       
                _chatService.InsertChat(newChat);
                _chatService.SaveChanges();
       
                _chatService.SaveChanges();
       
                return RedirectToAction("Join", "Channel", new { channelId = channelFromDb.Id });
       
            }
            else
            {
                return View();
            }
           
            
        
        }

    }
}
