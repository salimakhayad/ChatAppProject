using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class ChatProfile
    {
        public string ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
