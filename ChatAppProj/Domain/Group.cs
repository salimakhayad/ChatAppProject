using ChatAppProj.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{// for group chats
    public class Group : UniqueId
    {
        #region properties
        public virtual Profiel Creator { get; set; }
        public virtual ICollection<Profiel> Leden { get; set; }
        public Chat GroupChat { get; set; }
        public string GroupName { get; set; }
        public Group()
        {

        }
        public Group(Profiel creator, string groupName)
        {
            Creator = creator;
            GroupName = groupName;
            Leden = new List<Profiel>() { };
            GroupChat = new Chat();
            // Chat = new Chat() { Messages = new List<Message>() };
        }
        public void AddMember(Profiel member)
        {
            this.Leden.Add(member);
        }
        public void RemoveMember(Profiel member)
        {
            this.Leden.Remove(member);
        }
        public void SetGroupName(string gname)
        {
            this.GroupName = gname;
        }
        #endregion



    }
}
