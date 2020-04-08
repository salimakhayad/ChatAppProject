using ChatApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Data
{
    public interface IChatService
    {
        IEnumerable<Chat> GetAllChats();
        IEnumerable<Group> GetAllGroups();
        IEnumerable<Channel> GetAllChannels();
        IEnumerable<Message> GetAllMessages();
        IEnumerable<Profile> GetAllProfiles();
        IEnumerable<ChatProfile> GetAllChatProfiles();
        void DeleteMessageById(int Id);
        void DeleteGroupById(int Id);
        void DeleteChatById(int Id);
        void DeleteProfileById(int Id);
        void DeleteChatProfile(int Id);
        void InsertChat(Chat chat);
        void InsertGroup(Group group);
        void InsertProfile(Profile Profile);
        void InsertChatProfile(ChatProfile chatProfile);
        void InsertMessage(Message message);
        void InsertChannel(Channel channel);
        void SaveChanges();
        Task SaveChangesAsync();


    }
}
