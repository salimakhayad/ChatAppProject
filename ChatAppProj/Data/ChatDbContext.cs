using ChatApp.Domain;
using Microsoft.EntityFrameworkCore;
using ChatApp.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ChatApp.Data
{
    public class ChatDbContext : IdentityDbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        {
        }
        public DbSet<Profiel> Profielen { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<PrivateChat> PrivateGroups { get; set; }
        public DbSet<Chat> Chats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Group>()
                .HasOne(p => p.Creator);
            modelBuilder.Entity<Group>()
                .HasOne(le => le.Leden);
            

            base.OnModelCreating(modelBuilder);
        }
    }

}
    

