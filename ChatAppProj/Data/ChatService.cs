using ChatApp.Data;
using ChatApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Data
{
    public class ChatService:IChatService
    {
        private readonly ChatDbContext _context;
        public ChatService(ChatDbContext context)
        {
            _context = context;
        }

        public void DeleteChatById(int Id)
        {
            var chat = _context.Chats.Where(chat => chat.Id== Id).FirstOrDefault();
            if (chat != null)
            {
                _context.Chats.Remove(chat);
            }
        }

        public void DeleteGroupById(int Id)
        {
            var group = _context.Groups.Where(group => group.Id == Id).FirstOrDefault();
            if (group != null)
            {
                _context.Groups.Remove(group);
            }
        }

        public void DeleteMessageById(int Id)
        {
            var message = _context.Messages.Where(msg => msg.Id == Id).FirstOrDefault();
            if (message != null)
            {
                _context.Messages.Remove(message);
            }
        }

        public void DeleteProfileById(int Id)
        {
            var Profile = _context.Profiles.Where(msg => msg.Id == Id.ToString()).FirstOrDefault();
            if (Profile != null)
            {
                _context.Profiles.Remove(Profile);
            }
        }

        public IEnumerable<Chat> GetAllChats()
        {
            return _context.Chats;
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return _context.Groups;
        }

        public IEnumerable<Message> GetAllMessages()
        {
            return _context.Messages;
        }

       

        public IEnumerable<Profile> GetAllProfiles()
        {
            return _context.Profiles;

        }

        public IEnumerable<Channel> GetAllChannels()
        {
            return _context.Channels;
        }

        public void InsertChat(Chat chat)
        {
            _context.Chats.Add(chat);
        }

        public void InsertGroup(Group group)
        {
            _context.Groups.Add(group);
        }

        public void InsertMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public void InsertProfile(Profile Profile)
        {
            _context.Profiles.Add(Profile);
        }
        public void InsertChannel(Channel channel)
        {
            _context.Channels.Add(channel);
        }
        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<ChatProfile> GetAllChatProfiles()
        {
            return _context.ChatProfiles;
        }

        public void DeleteChatProfile(int Id)
        {
            var  chatprofile = _context.ChatProfiles.Where(x=>x.ChatId==Id).FirstOrDefault();
            _context.ChatProfiles.Remove(chatprofile);
            _context.SaveChanges();
        }

        public void InsertChatProfile(ChatProfile chatProfile)
        {
            _context.ChatProfiles.Add(chatProfile);
            _context.SaveChanges();
        }
    }
}
