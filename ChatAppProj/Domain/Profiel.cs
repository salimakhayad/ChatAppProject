using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class Profiel
    {
        [Key]
        public string UserId { get; set; }
        public string Naam { get; set; }
        [NotMapped]
        public virtual List<Group> Groups { get; set; }
    }
}
