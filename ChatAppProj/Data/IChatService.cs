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
        IEnumerable<ChannelProfile> GetAllChannelProfiles();
        IEnumerable<TimeRegistration> GetAllTimeRegistrations();
        void DeleteMessageById(int Id);
        void DeleteGroupById(int Id);
        void DeleteChatById(int Id);
        void DeleteProfileById(int Id);
        void DeleteChannelProfile(int Id);
        void DeleteChatProfileById(int Id);
        void DeleteTimeRegistration(int Id);
        void InsertChat(Chat chat);
        void InsertGroup(Group group);
        void InsertProfile(Profile Profile);
        void InsertChannelProfile(ChannelProfile chatProfile);
        void InsertMessage(Message message);
        void InsertChannel(Channel channel);
        void InsertTimeRegistration(TimeRegistration tr);
        void SaveChanges();
        Task SaveChangesAsync();


    }
}
