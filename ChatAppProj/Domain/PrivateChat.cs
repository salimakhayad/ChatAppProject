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
        public int Id { get; set; }
        public virtual Profiel Zender { get; set; }
        public virtual Chat Chat { get; set; }
        public virtual Profiel Partner { get; set; }
        #endregion
       

    }
}
