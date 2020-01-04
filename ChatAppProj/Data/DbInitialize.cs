using ChatApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Data
{
    public class DbInitialize
    {
        public static void Initialize(ChatDbContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
