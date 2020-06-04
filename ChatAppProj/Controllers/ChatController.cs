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
using ChatApp.Models.Chat;
using System.IO;

namespace ChatApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IHubContext<ChatHub> _chat;
        private readonly SignInManager<Profile> _signInManager;
        private readonly UserManager<Profile> _userManager;

        public ChatController(IHubContext<ChatHub> chat, IChatService service, UserManager<Profile> userManager, SignInManager<Profile> signInManager)
        {
            this._chat = chat;
            this._chatService = service;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        [HttpPost("[action]/{connectionId}/{channelId}")]
        public async Task<IActionResult> JoinChannel(string connectionId, string channelId)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, channelId);


            var chat = _chatService.GetAllChats().FirstOrDefault(c => c.ChannelId.ToString() == channelId);

            var profileId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var profile = _chatService.GetAllProfiles().Where(x => x.Id == profileId).FirstOrDefault();

            // register time
            var timeregistration = new TimeRegistration()
            {
                Chat = chat,
                ChatId = chat.Id,
                Profile = profile,
                ProfileId = profile.Id,
                StartTime = DateTime.Now
            };
            _chatService.InsertTimeRegistration(timeregistration);
            _chatService.SaveChanges();

            // notify Clients in group
            await _chat.Clients.Group(channelId)
            .SendAsync("UserJoinedChannel", new
            {
                Text = " has joined.",
                Name = profile.UserName,
                ProfileId = profile.Id,
                Timestamp = DateTime.Now.ToShortTimeString()
            });


            // ChatService.SetUsersOfflineAfter5Min();
            var trs = _chatService.GetAllTimeRegistrations().Where(tr => tr.ChatId == chat.Id && tr.EndTime == null);
            var usersCurrentlyOnline = trs.Select(tr => new ProfileChatModel
            {
                ProfileName = tr.Profile.Name,
                ProfileId = tr.ProfileId
            });


            await _chat.Clients.Group(channelId).
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
       
        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId, string message)
        {
            var profileId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var profile = _chatService.GetAllProfiles().Where(x => x.Id == profileId).FirstOrDefault();

            var Message = new Message()
            {
                ChatId = chatId.ToString(),
                Text = message?? " ",
                Timestamp = DateTime.Now,
                ProfileId = profileId,
                Profile = profile,
                Type = MessageType.Text
            };
            _chatService.InsertMessage(Message);
            await _chatService.SaveChangesAsync();
            var chat = _chatService.GetAllChats().FirstOrDefault(c => c.Id == chatId.ToString());
            var channel = _chatService.GetAllChannels().FirstOrDefault(channel => channel.Chat.Id == chatId.ToString());
        
            return RedirectToAction("Join","Channel", new { channelId = channel.Id });
        }
       

       
        // axios.post('/Chat/SendMessage', data)
        [HttpPost("[action]")]
        public async Task<ActionResult> SendMessage(
            int chatId,
            string message
            )
        {
            var chat = _chatService.GetAllChats().FirstOrDefault(c=>c.Id==chatId.ToString());
            var channelId = _chatService.GetAllChannels().FirstOrDefault(c => c.Id == chat.ChannelId).Id.ToString();

            var profileId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var profile = _chatService.GetAllProfiles().Where(x => x.Id == profileId).FirstOrDefault();

            var Message = new Message()
            {
                Chat = chat,
                ChatId = chatId.ToString(),
                Text = message??" ",
                Profile = profile,
                ProfileId = profileId,
                Timestamp = DateTime.Now,
                Type = MessageType.Text
            };
            _chatService.InsertMessage(Message);
            await _chatService.SaveChangesAsync();

            await _chat.Clients.Group(channelId)
            .SendAsync("ReceiveMessage", new
            {
                Message.Text,
                Message.Profile.Name,
                Timestamp = Message.Timestamp.ToShortTimeString(),
                Type = MessageType.Text,
                Message.Id
            });
            return Ok();
        }
        // axios.post('/Chat/SendGif', data)
        [HttpPost("[action]")]
        public async Task<ActionResult> SendGif(
           int chatId,
           string gifUrl
           )
        {
            var chat = _chatService.GetAllChats().FirstOrDefault(c => c.Id == chatId.ToString());
            var channelId = _chatService.GetAllChannels().FirstOrDefault(c => c.Id == chat.ChannelId).Id.ToString();
            var profileId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var profile = _chatService.GetAllProfiles().Where(x => x.Id == profileId).FirstOrDefault();

            var Message = new Message()
            {
                Chat = chat,
                ChatId = chat.Id,
                Text = gifUrl,
                Profile = profile,
                ProfileId = profileId,
                Timestamp = DateTime.Now,
                Type = MessageType.Gif
            };
            _chatService.InsertMessage(Message);
            await _chatService.SaveChangesAsync();

            await _chat.Clients.Group(channelId)
              .SendAsync("ReceiveMessage", new
              {
                  Message.Text,
                  Message.Profile.Name,
                  Timestamp = DateTime.Now.ToShortTimeString(),
                  Type = MessageType.Gif
              });


            return Ok();
          
        }
        [HttpPost("[action]")]
        public async Task<ActionResult> SendImage(
           int chatId,
           IFormFile file
           )
        {
            var chat = _chatService.GetAllChats().FirstOrDefault(c => c.Id == chatId.ToString());
            var channelId = _chatService.GetAllChannels().FirstOrDefault(c => c.Id == chat.ChannelId).Id.ToString();
            var profileId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var profile = _chatService.GetAllProfiles().Where(x => x.Id == profileId).FirstOrDefault();

            var Message = new Message()
            {
                Chat = chat,
                ChatId = chat.Id,
                Text = "",
                Profile = profile,
                ProfileId= profile.Id,
                Timestamp = DateTime.Now,
                Type = MessageType.Image
            };
            _chatService.InsertMessage(Message);
            await _chatService.SaveChangesAsync();

            var messageFromDb = _chatService.GetAllMessages().FirstOrDefault(m => m.Id == Message.Id);

            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            messageFromDb.File = memoryStream.ToArray();

            await _chatService.SaveChangesAsync();
            var imgsrc = String.Format($"data:image/gif;base64,{Convert.ToBase64String(Message.File)}");
            await _chat.Clients.Group(channelId)
              .SendAsync("ReceiveMessage", new
              {
                  Src = imgsrc,
                  Message.Profile.Name,
                  Timestamp = DateTime.Now.ToShortTimeString(),
                  Type = MessageType.Image
              });

            return Ok();

        }
    }
}

