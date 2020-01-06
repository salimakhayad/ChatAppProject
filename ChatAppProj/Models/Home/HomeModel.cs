using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Domain;

namespace ChatApp.Models.Home
{
    public class HomeModel
    {
        public ICollection<Domain.Profiel> Profielen { get; set; }
    }
}
