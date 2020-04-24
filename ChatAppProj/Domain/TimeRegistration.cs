using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class TimeRegistration
    {
       
        public int Id { get; set; }

        [ForeignKey("Chat")]
        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }

        [ForeignKey("Profile")]
        public string ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public DateTime TimeEntered { get; set; }
        public DateTime? TimeLeft { get; set; }

    }
}
