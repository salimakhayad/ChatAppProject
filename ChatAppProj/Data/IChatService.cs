using ChatApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Data
{
    public interface IChatService
    {
        IEnumerable<PrivateChat> GetAllPrivateChats();
        IEnumerable<Chat> GetAllChats();
        IEnumerable<Group> GetAllGroups();
        IEnumerable<Message> GetAllMessages();
        IEnumerable<Profiel> GetAllProfielen();
        void DeleteMessageById(int Id);
        void DeleteGroupById(int Id);
        void DeleteChatById(int Id);
        void DeleteProfielById(int Id);

        void InsertChat(Chat chat);
        void InsertGroup(Group group);
        void InsertProfiel(Profiel profiel);
        void InsertMessage(Message message);
        void SaveChanges();


    }
}
