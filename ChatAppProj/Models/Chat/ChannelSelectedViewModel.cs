using ChatApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models.Group
{
    public class ChannelSelectedViewModel
    {
        public ChannelSelectedViewModel()
        {
            Channels = new List<Channel>();
            ChatProfiles = new List<ChatProfile>();
        }
      
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string ProfileId { get; set; }
        public Domain.Profile Profile { get; set; }
        // public byte[] Photo { get; set; }
        public Channel SelectedChannel { get; set; }
        public virtual ICollection<Channel> Channels { get; set; }
        public virtual ICollection<ChatProfile> ChatProfiles { get; set; }
    }
}
