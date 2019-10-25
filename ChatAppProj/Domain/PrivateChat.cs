using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    // for 1 on 1 chats
    public class PrivateChat:Group
    {
        /*
         * public virtual Profiel Creator { get; set; }
        public virtual ICollection<Profiel> Leden { get; set; }
        public Chat GroupChat { get; set; }
        public string GroupName { get; set; }
         */
        public PrivateChat()
        {

        }
        public PrivateChat(Profiel creator, string groupName, Profiel partner) : base(creator, groupName)
        {
            Creator = creator;
            GroupChat = new Chat();
            Leden = new List<Profiel>() { partner };
            Partner = partner;
        }
        #region properties
        public virtual Profiel Partner { get; set; }
        #endregion
       

    }
}
