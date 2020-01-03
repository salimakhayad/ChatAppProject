using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{// for group chats
    public class Group 
    {
        #region properties
        public int Id { get; set; }
        public virtual Profiel Creator { get; set; }
        public virtual ICollection<Profiel> Profielen { get; set; }
        public virtual Chat GroupChat { get; set; }
        public string GroupName { get; set; }

        #endregion



    }
}
