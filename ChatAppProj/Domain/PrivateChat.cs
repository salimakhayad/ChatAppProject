using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    // for 1 on 1 chats
    public class PrivateChat:Group
    {
        #region properties
        public int PartnerName { get; set; }
        #endregion
        public PrivateChat()
        {

        }
        public PrivateChat(int creatorUserId, string creatorName, string name, Profiel partner,int partnerName) :base(creatorUserId,creatorName,name)
        {
            UserId = creatorUserId;
            ProfielName = creatorName;
            Name = name;
            Profielen = new List<Profiel>() {};
            Chat = new Chat() { Messages = new List<Message>() };
            PartnerName = partnerName;
        }

    }
}
