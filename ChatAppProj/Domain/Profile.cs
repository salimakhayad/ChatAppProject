using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class Profile : IdentityUser
    {
        public Profile()
        {
         
        }
        public string Name { get; set; }

        [EmailAddress]
        // https://docs.microsoft.com/en-us/dotnet/api/system.net.mail.mailaddress?view=netcore-3.0
        public string EmailAddress { get; set; }
        public byte[] ProfilePicture { get; set; }
        public ICollection<Group> Groups { get; set; }


    }
}
