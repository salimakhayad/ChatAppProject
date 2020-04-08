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
        private Profile _currentProfile;

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
                ChannelId = chatId,
                Text = message,
                ProfileName = User.Identity.Name
            };
            _chatService.InsertMessage(Message);
            await _chatService.SaveChangesAsync();

            return RedirectToAction("Chat", new { id = chatId });
        }
        //http://localhost:5001/Chat/JoinChannel?channelId=1
        [HttpPost("{channelId}")]
        public IActionResult JoinChannel(string channelId)
        {
            ChannelSelectedViewModel model = new ChannelSelectedViewModel();

            var profileId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var profile = _chatService.GetAllProfiles().Where(x => x.Id == profileId).FirstOrDefault();

            var selectedChannel = _chatService.GetAllChannels()
                  .FirstOrDefault(c => c.Id.ToString() == channelId);

            var currentGroup = _chatService.GetAllGroups().FirstOrDefault(g => g.Channels.Contains(selectedChannel));
            var channels = _chatService.GetAllChannels()
                  .Where(c => c.GroupId == currentGroup.Id);

            

            foreach (var channel in channels)
            {
                var sChannel = _chatService.GetAllChannels()
                 .Where(c => c.Id == channel.Id)
                 .FirstOrDefault();

                var sChat = _chatService.GetAllChats()
                .Where(chat => chat.ChannelId == sChannel.Id)
                .FirstOrDefault();

                var messages = _chatService.GetAllMessages()
               .Where(mes => mes.ChannelId == sChat.ChannelId);


                sChat.Messages = messages.ToList();
                selectedChannel.Chat = sChat;
            }
            var allmessages = _chatService.GetAllMessages();
            model.Channels = channels.ToList();
            model.Name = currentGroup.Name;
            model.Profile = profile;
            model.ProfileId = profile.Id;
            model.SelectedChannel = selectedChannel;
            model.Id = currentGroup.Id;

            return View(model);
           
        }
        [HttpPost("[action]/{connectionId}/{channelId}")]
        public async Task<IActionResult> JoinChannel(string connectionId, string channelId)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, channelId);
            return Ok();
        }
        [HttpPost("[action]/{connectionId}/{channelId}")]
        public async Task<IActionResult> LeaveChannel(string connectionId, string channelId)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, channelId);
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<ActionResult> SendMessage(
            int channelID,
            string message,
            string roomName,
            [FromServices]ChatDbContext ctx)
        {
            
            var Message = new Message()
            {
                ChannelId = channelID,
                Text = message,
                ProfileName = User.Identity.Name,

            };
            ctx.Messages.Add(Message);
            await ctx.SaveChangesAsync();

            await _chat.Clients.Group(channelID.ToString())
            .SendAsync("ReceiveMessage", new
            {
                Text = Message.Text,
                Name = Message.ProfileName,
                Timestamp = Message.Timestamp.ToString("dd/MM/yyyy hh:mm:ss")
            });
            return Ok();
        }
    }
}
