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
        private readonly SignInManager<Profile> _signInManager;
        private readonly UserManager<Profile> _userManager;


        public ChannelController(IChatService service, UserManager<Profile> userManager, SignInManager<Profile> signInManager)
        {
            this._chatService = service;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
       
        public IActionResult Join(string channelId)
        {
            // set viewbag => Channel - nameOfChannel
            var profileId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var profile = _chatService.GetAllProfiles().Where(x => x.Id == profileId).FirstOrDefault();

            var selectedChannel = _chatService.GetAllChannels()
                  .FirstOrDefault(c => c.Id.ToString() == channelId);
            var selectedChat = _chatService.GetChatById(selectedChannel.ChatId);
            selectedChannel.Chat = selectedChat;

            var currentGroup = _chatService.GetAllGroups().FirstOrDefault(g => g.Channels.Contains(selectedChannel));
            var channels = _chatService.GetAllChannels()
                  .Where(c => c.GroupId == currentGroup.Id);

            var usersIdsInGroup = _chatService.GetAllGroupProfiles().Where(g => g.GroupId == currentGroup.Id).Select(g => g.ProfileId).ToList();
            var users = _chatService.GetAllProfiles();

            List<Profile> UsersInGroup = SelectUsersInGroup(usersIdsInGroup, users);
            List<Profile> UsersOutsideGroup = SelectUsersOutsideGroup(usersIdsInGroup, users);

            var model = MapChannelSelectedModel(profileId, profile, selectedChannel, currentGroup, channels, UsersInGroup, UsersOutsideGroup);

            return View("Views/Group/ChannelSelected.cshtml", model);
        }

        private ChannelSelectedViewModel MapChannelSelectedModel(string profileId, Profile profile, Channel selectedChannel, Group currentGroup, IEnumerable<Channel> channels, List<Profile> filteredUsersInGroup, List<Profile> UsersOutsideGroup)
        {
            ChannelSelectedViewModel model = new ChannelSelectedViewModel();
            model.Photo = currentGroup.Photo;
            // if user is owner
            model.IsOwner = currentGroup.ProfileId == profileId;
            model.OwnerId = currentGroup.ProfileId;

            model.Group = currentGroup;
            model.Channels = channels.ToList();
            model.Name = currentGroup.Name;
            model.Profile = profile;
            model.ProfileId = profile.Id;
            model.SelectedChannel = selectedChannel;
            model.Id = currentGroup.Id;
            model.ProfilesInside = filteredUsersInGroup;
            model.ProfilesOutside = UsersOutsideGroup;
            return model;
        }

        private static List<Profile> SelectUsersOutsideGroup(List<string> usersIdsInGroup, IEnumerable<Profile> users)
        {
            var UsersOutsideGroup = new List<Profile>();

            foreach (var user in users)
            {
                if (!usersIdsInGroup.Contains(user.Id))
                {
                    UsersOutsideGroup.Add(user);
                }
            }

            return UsersOutsideGroup;
        }

        private static List<Profile> SelectUsersInGroup(List<string> usersIdsInGroup, IEnumerable<Profile> users)
        {
            var filteredUsersInGroup = new List<Profile>();
            foreach (var user in users)
            {
                if (usersIdsInGroup.Contains(user.Id))
                {
                    filteredUsersInGroup.Add(user);
                }
            }

            return filteredUsersInGroup;
        }

        public IActionResult Create(Guid groupId)
        {
           
            var groupFromDb = _chatService.GetAllGroups().FirstOrDefault(g => g.Id.GetHashCode().ToString() == groupId.ToString());

            CreateChannelModel model = new CreateChannelModel()
            {
                groupId = groupId
            };

          
          return View("Views/Channel/Create.cshtml", model);

        }
        [HttpPost]
        public IActionResult CreatePost(CreateChannelModel model)
        {
            if (ModelState.IsValid)
            {
                var group = _chatService.GetAllGroups().FirstOrDefault(g => g.Id == model.groupId);
                var channel = _chatService.GetAllChannels().FirstOrDefault(c => c.GroupId == group.Id);

                var newChat = new Chat()
                {
                    Id = Guid.NewGuid(),
                    ChatType = ChatType.Group,
                    Messages = new List<Message>(),
                    Group = group,
                    GroupId = group.Id
                };

                _chatService.InsertChat(newChat);
                _chatService.SaveChanges();

                var chatFromDb = _chatService.GetAllChats().FirstOrDefault(c => c.Id == newChat.Id);


                var newChannel = new Channel()
                {
                    //Id = Guid.NewGuid(),
                    Name = model.Name,
                    GroupId = group.Id,
                    Group = group,
                    Chat = chatFromDb,
                    ChatId = chatFromDb.Id
                };

                _chatService.InsertChannel(newChannel);
       
                _chatService.SaveChanges();

                group.Channels.Add(newChannel);
                _chatService.SaveChanges();

                var groupFromDbWithChannel = _chatService.GetAllGroups().FirstOrDefault(g => g.Id == model.groupId);

                var channelFromDb = _chatService.GetAllChannels().FirstOrDefault(c => c.Id == newChannel.Id && c.GroupId == groupFromDbWithChannel.Id);
       
                return RedirectToAction("Join", "Channel", new { channelId = channelFromDb.Id });
       
            }
            else
            {
                return View();
            }
           
            
        
        }

    }
}
