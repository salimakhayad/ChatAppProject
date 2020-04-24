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
using Microsoft.AspNetCore.SignalR;
using ChatApp.Hubs;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IHubContext<ChatHub> _chat;

        private readonly IUserStore<Profile> _userStore;
        private readonly IUserClaimsPrincipalFactory<Profile> _claimsPrincipalFactory;
        private readonly SignInManager<Profile> _signInManager;
        private readonly UserManager<Profile> _userManager;
        //private Profile _currentProfile;

        public ChatController(IHubContext<ChatHub> chat, IChatService service, IUserStore<Profile> userStore, UserManager<Profile> userManager, IUserClaimsPrincipalFactory<Profile> claimsPrincipalFactory, SignInManager<Profile> signInManager)
        {
            this._chat = chat;
            this._chatService = service;
            this._userManager = userManager;
            this._claimsPrincipalFactory = claimsPrincipalFactory;
            this._userStore = userStore;
            this._signInManager = signInManager;
        }
        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            var chats = _chatService.GetAllChats();
            IQueryable<Chat> IQueryableChats = chats.AsQueryable();


            var chat = IQueryableChats
            .Include(x => x.Messages)
           .FirstOrDefault(x => x.Id == id);
            return View(chat);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId, string message)
        {
            var Message = new Message()
            {
                ChatId = chatId,
                Text = message,
                ProfileName = User.Identity.Name,
                Timestamp = DateTime.Now              
            };
            _chatService.InsertMessage(Message);
            await _chatService.SaveChangesAsync();
            var chat = _chatService.GetAllChats().FirstOrDefault(c => c.Id == chatId);
            var channel = _chatService.GetAllChannels().FirstOrDefault(channel => channel.Chat.Id == chatId);

            return RedirectToAction("JoinChannel","Channel", new { channelId = channel.Id });
        }
        //http://localhost:5001/Chat/JoinChannel?channelId=1
       


        [HttpPost("[action]/{connectionId}/{channelId}")]
        public async Task<IActionResult> JoinChannel(string connectionId, string channelId)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, channelId);


            var chat = _chatService.GetAllChats().FirstOrDefault(c => c.ChannelId.ToString() == channelId);

            var profileId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var profile = _chatService.GetAllProfiles().Where(x => x.Id == profileId).FirstOrDefault();


            await _chat.Clients.Group(chat.Id.ToString())
            .SendAsync("UserJoinedChannel", new
            {
                Text = " ... has joined the chat",
                Name = profile.UserName,
                Timestamp = DateTime.Now.ToShortTimeString()
            });


            // db
            var timeregistration = new TimeRegistration()
            {
                Chat = chat,
                ChatId = chat.Id,
                Profile = profile,
                ProfileId = profile.Id,
                TimeEntered = DateTime.Now
            };
            _chatService.InsertTimeRegistration(timeregistration);
            _chatService.SaveChanges();

            var trs = _chatService.GetAllTimeRegistrations().Where(tr => tr.ChatId == chat.Id&&tr.TimeLeft==null);
            var usersCurrentlyOnline = new List<string>();
            foreach (var tr in trs)
            {
                var user = _chatService.GetAllProfiles().FirstOrDefault(p => p.Id == tr.ProfileId);
                usersCurrentlyOnline.Add(user.UserName);
            }

             await _chat.Clients.Group(chat.Id.ToString()).
             SendAsync("UpdateUsersOnline", usersCurrentlyOnline)
             ;


            return Ok();
        }
        [HttpPost("[action]/{connectionId}/{channelId}")]
        public async Task<IActionResult> LeaveChannel(string connectionId, string channelId)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, channelId);
            return Ok();
        }
        // axios.post('/Chat/SendMessage', data)
        [HttpPost("[action]")]
        public async Task<ActionResult> SendMessage(
            int chatId,
            string message
            )
        {
            var chat = _chatService.GetAllChats().FirstOrDefault(c=>c.Id==chatId);
            var Message = new Message()
            {
                Chat = chat,
                ChatId = chatId,
                Text = message,
                ProfileName = User.Identity.Name,
                Timestamp = DateTime.Now
            };
            _chatService.InsertMessage(Message);
            await _chatService.SaveChangesAsync();

            await _chat.Clients.Group(chatId.ToString())
            .SendAsync("ReceiveMessage", new
            {
                Text = Message.Text,
                Name = Message.ProfileName,
                Timestamp = Message.Timestamp.ToShortTimeString()
            });
            return Ok();
        }
    }
}
