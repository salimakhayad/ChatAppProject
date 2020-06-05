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
        IEnumerable<GroupProfile> GetAllGroupProfiles();
        IEnumerable<TimeRegistration> GetAllTimeRegistrations();
        Chat GetChatById(Guid id);
        void DeleteMessageById(int Id);
        void DeleteGroupById(int Id);
        void DeleteChatById(int Id);
        void DeleteProfileById(int Id);
        void DeleteTimeRegistrationById(int Id);
        // void DeleteGroupProfile(int id);
        void InsertChat(Chat chat);
        void InsertGroup(Group group);
        void InsertProfile(Profile Profile);
        void InsertGroupProfile(GroupProfile groupProfile);
        void InsertMessage(Message message);
        void InsertChannel(Channel channel);
        void InsertTimeRegistration(TimeRegistration tr);
        void SaveChanges();
        Task SaveChangesAsync();


    }
}
