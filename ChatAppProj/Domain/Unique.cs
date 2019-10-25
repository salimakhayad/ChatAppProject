using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAppProj.Domain
{
    public abstract class UniqueId
    {
      public string Id { get; set; }
    }
}
