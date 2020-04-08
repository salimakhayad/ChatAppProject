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


namespace ChatApp.Controllers
{
    [Authorize]
   
    public class GroupController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IUserStore<Profile> _userStore;
        private readonly IUserClaimsPrincipalFactory<Profile> _claimsPrincipalFactory;
        private readonly SignInManager<Profile> _signInManager;
        private readonly UserManager<Profile> _userManager;
        private Profile _currentProfile;

        public GroupController(IChatService service, IUserStore<Profile> userStore, UserManager<Profile> userManager, IUserClaimsPrincipalFactory<Profile> claimsPrincipalFactory, SignInManager<Profile> signInManager)
        {
            this._chatService = service;
            this._userManager = userManager;
            this._claimsPrincipalFactory = claimsPrincipalFactory;
            this._userStore = userStore;
            this._signInManager = signInManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            GroupIndexModel model = new GroupIndexModel();
            model.Groups = _chatService.GetAllGroups().ToList();
            return View(model);
        }
        public IActionResult Join(int groupId)
        {
           
            var profileId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var profile = _chatService.GetAllProfiles().Where(x => x.Id == profileId).FirstOrDefault();

            var group = _chatService.GetAllGroups().FirstOrDefault(g => g.Id == groupId);
            var channels = _chatService.GetAllChannels()
                  .Where(c => c.GroupId == group.Id);

            foreach (var channel in channels)
            {
                var selectedChannel = _chatService.GetAllChannels()
                 .Where(c => c.Id == channel.Id)
                 .FirstOrDefault();

                var selectedChat = _chatService.GetAllChats()
                .Where(chat => chat.ChannelId == selectedChannel.Id)
                .FirstOrDefault();

                var messages = _chatService.GetAllMessages()
               .Where(mes => mes.ChannelId == selectedChat.ChannelId);


                selectedChat.Messages = messages.ToList();
                selectedChannel.Chat = selectedChat;
            }
            var allmessages = _chatService.GetAllMessages();
            return View(group);
        }
        /*
         * 
           // _chatService.SaveChangesAsync();

             var group = _chatService.GetAllGroups()
                .Where(g => g.Id == groupId)
                .FirstOrDefault();

            var channels = _chatService.GetAllChannels()                 
                 .Where(c => c.GroupId == groupId);  

            group.Channels = channels.ToList();
           // 
           // var currentUserId = this._signInManager.UserManager.GetUserId(HttpContext.User);
            
         */
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

            Group newGroup = new Group()
            {
                Name = model.Name,
                Profile = _currentProfile,
                ProfileId = _currentProfile.Id,
                ChatProfiles = new List<ChatProfile>(),
                Channels = new List<Channel>()
            };

            _chatService.InsertGroup(newGroup);
            _chatService.SaveChanges();

            var groupFromDb = _chatService.GetAllGroups().FirstOrDefault(g => g.Id == newGroup.Id);
            _chatService.InsertChannel( new Channel() {
                                        Name = "Main",
                                        GroupId = groupFromDb.Id,
                                        Group = groupFromDb
                                        });
            _chatService.SaveChanges();
            var groupFromDbWithChannel = _chatService.GetAllGroups().FirstOrDefault(g => g.Id == newGroup.Id);
            var newChat = new Chat()
            {
                ChatType = ChatType.Group,
                Messages = new List<Message>(),
                Channel = groupFromDbWithChannel.Channels.FirstOrDefault(),
                ChannelId = groupFromDbWithChannel.Channels.FirstOrDefault().Id
            };

            _chatService.InsertChat(newChat);
            _chatService.SaveChanges();

            var chatFromDb = _chatService.GetAllChats().FirstOrDefault(g => g.Id == newChat.Id);
            var msg = new Message()
            {
                ProfileName = "Server",
                Text = "Welcome to our Server!",
                Channel = groupFromDbWithChannel.Channels.FirstOrDefault(),
                ChannelId = groupFromDbWithChannel.Channels.FirstOrDefault().Id,
             };

            _chatService.InsertMessage(msg);

            var chatFromDbn = _chatService.GetAllChats().FirstOrDefault(g => g.Id == newChat.Id);

            _chatService.SaveChanges();


            var group = _chatService.GetAllGroups().FirstOrDefault(g => g.Id == newGroup.Id);
            var getAllMessages = _chatService.GetAllMessages();
            /*
             *  Channels = new List<Channel>(){
                                    new Channel() {
                                        Name = "Main",
                                        Chat = new Chat() {
                                            ChatType = ChatType.Group,
                                            Messages = new List<Message>() {
                                                            new Message() {
                                                                ProfileName = "Server",
                                                                Text = "Welcome to our Server!"} } },}}};
             */


            return RedirectToAction("Index");
        }
    }
}
