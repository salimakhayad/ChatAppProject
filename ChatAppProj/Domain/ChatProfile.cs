using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class ChannelProfile
    {
        [ForeignKey("Profile")]
        public string ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        [ForeignKey("Channel")]
        public int ChannelId { get; set; }
        public virtual Channel Channel { get; set; }
    }
}
