using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ChatApp.Models.Channel
{
    public class CreateChannelModel
    {
        public string Name { get; set; }
        public Guid groupId { get; set; }
    }
}
