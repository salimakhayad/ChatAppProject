using ChatApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models.Group
{
    public class ChannelSelectedViewModel
    {
      
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfileId { get; set; }
        public bool IsOwner { get; set; }
        public string OwnerId { get; set; }
        public Domain.Profile Profile { get; set; }
        public byte[] Photo { get; set; }
        public Domain.Channel SelectedChannel { get; set; }
        public virtual ICollection<Domain.Channel> Channels { get; set; }
        public Domain.Group Group { get; set; }

        public ICollection<Domain.Profile> ProfilesInside { get; set; }
        public ICollection<Domain.Profile> ProfilesOutside { get; set; }

    }
}
