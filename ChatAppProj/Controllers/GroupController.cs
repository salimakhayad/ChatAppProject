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
using System.IO;

namespace ChatApp.Controllers
{
    [Authorize]
   
    public class GroupController : Controller
    {
        private readonly IChatService _chatService;
        private readonly SignInManager<Profile> _signInManager;
        private readonly UserManager<Profile> _userManager;
        private Profile _currentProfile;

        public GroupController(IChatService service, UserManager<Profile> userManager, SignInManager<Profile> signInManager)
        {
            this._chatService = service;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            GroupIndexModel model = new GroupIndexModel();
            var grps = _chatService.GetAllGroups().Where(g=>g.Privacy==PrivacyType.Public).ToList();
            model.Groups = grps.ToList();
            return View(model);
        }
        public IActionResult Join(int groupId)
        {

            var profileId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var profile = _chatService.GetAllProfiles().Where(x => x.Id == profileId).FirstOrDefault();

            var group = _chatService.GetAllGroups().FirstOrDefault(g => g.Id == groupId.ToString());
            var channels = _chatService.GetAllChannels()
                  .Where(c => c.GroupId == group.Id);

         var userIsAlrdyInGroup = _chatService.GetAllGroupProfiles().Where(g => g.GroupId == group.Id && g.ProfileId == profileId).Any();
         if (!userIsAlrdyInGroup)
         {
             var userGroup = new GroupProfile()
             {
                 GroupId = group.Id,
                 ProfileId = profileId
             };
             _chatService.InsertGroupProfile(userGroup);
             _chatService.SaveChanges();
         }
          // var usersInGroup = _chatService.GetAllGroupProfiles().Where(g => g.GroupId == group.Id).Select(x => x.Profile);
             foreach (var channel in channels)
             {
                 var selectedChannel = _chatService.GetAllChannels()
                  .Where(c => c.Id == channel.Id)
                  .FirstOrDefault();
             
                 var selectedChat = _chatService.GetAllChats()
                 .Where(chat => chat.ChannelId == selectedChannel.Id)
                 .FirstOrDefault();
             
                 var messages = _chatService.GetAllMessages()
                .Where(mes => mes.ChatId == selectedChat.Id);
             
             
                 selectedChat.Messages = messages.ToList();
                 selectedChannel.Chat = selectedChat;
             }
           
            return View(group);
        }

        public IActionResult Create()
        {
            CreateGroupModel model = new CreateGroupModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CreateGroupModel model)
        {
            if (!TryValidateModel(model))
            {
                return View(model);
            }

            var currentUserId = this._signInManager.UserManager.GetUserId(HttpContext.User);
            _currentProfile = _chatService.GetAllProfiles().Where(u => u.Id == currentUserId).First();

            // create group
            Group newGroup = new Group()
            {
                Name = model.Name,
                Profile = _currentProfile,
                ProfileId = _currentProfile.Id,
                Content = model.Content,         
                GroupProfiles = new List<GroupProfile>(),
                Channels = new List<Channel>()
            };

            _chatService.InsertGroup(newGroup);
            _chatService.SaveChanges();

            var groupFromDb = _chatService.GetAllGroups().FirstOrDefault(g => g.Id == newGroup.Id);

            using var memoryStream = new MemoryStream();
                        model.Photo.CopyTo(memoryStream);
                        groupFromDb.Photo = memoryStream.ToArray();
                        
             _chatService.SaveChanges();

            var newChannel = new Channel()
            {
                Name = "Main",
                GroupId = groupFromDb.Id,
                Group = groupFromDb
            };
            _chatService.InsertChannel(newChannel);
            _chatService.SaveChanges();
            groupFromDb = _chatService.GetAllGroups().FirstOrDefault(g => g.Id == newGroup.Id);
            var newChat = new Chat()
            {
                ChatType = ChatType.Group,
                Messages = new List<Message>(),
                Channel = groupFromDb.Channels.FirstOrDefault(),
                ChannelId = groupFromDb.Channels.FirstOrDefault().Id
            };

            _chatService.InsertChat(newChat);
            _chatService.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
