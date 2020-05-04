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
        public string ProfileId { get; set; }
        public string ProfileName { get; set; }
        // insertiondate
        public bool IsOnline { get; set; }

    }
}
