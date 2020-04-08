using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Domain;

namespace ChatApp.Models.Home
{
    public class HomeModel
    {
        public ICollection<Domain.Profile> Profiles { get; set; }
        public ICollection<Domain.Group> ChatGroups { get; set; }
    }
}
