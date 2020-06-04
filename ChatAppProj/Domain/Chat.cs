
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class Chat
    {

        public Chat()
        {
        }
        public string Id { get; set; }

        [ForeignKey("Channel")]
        public string ChannelId { get; set; }
        public Channel Channel { get; set; }

        public ChatType ChatType { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Profile> Profiles { get; set; }
    }
}
