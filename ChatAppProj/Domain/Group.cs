using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{// for group chats
    public class Group
    {
        #region properties
        [Key]
        public int Id { get; set; }
        public int UserId;
        public string ProfielName;
        public Chat Chat;
        public string Name { get; set; }
        public virtual List<Profiel> Profielen { get; set; }
        #endregion
        public Group()
        {

        }
        public Group(int creatorUserId,string creatorName,string name)
        {
            UserId = creatorUserId;
            ProfielName = creatorName;
            Name = name;
            Profielen = new List<Profiel>() { };
            Chat = new Chat() { Messages = new List<Message>() };
        }

    }
}
