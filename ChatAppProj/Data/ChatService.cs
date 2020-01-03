using ChatApp.Data;
using ChatApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAppProj.Data
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

        public void DeleteProfielById(int Id)
        {
            var profiel = _context.Profielen.Where(msg => msg.Id == Id.ToString()).FirstOrDefault();
            if (profiel != null)
            {
                _context.Profielen.Remove(profiel);
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

        public IEnumerable<PrivateChat> GetAllPrivateChats()
        {
            return _context.Chats.OfType<PrivateChat>();
        }

        public IEnumerable<Profiel> GetAllProfielen()
        {
            return _context.Profielen;

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

        public void InsertProfiel(Profiel profiel)
        {
            _context.Profielen.Add(profiel);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
