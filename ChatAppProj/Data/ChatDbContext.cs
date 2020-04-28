using ChatApp.Domain;
using Microsoft.EntityFrameworkCore;
using ChatApp.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;

namespace ChatApp.Data
{
    public class ChatDbContext : IdentityDbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        {
        }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChannelProfile> ChannelProfiles { get; set; }
        public DbSet<GroupProfile> GroupProfiles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<TimeRegistration> TimeRegistrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                                        .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
   
            modelBuilder.Entity<ChannelProfile>()
                .HasKey(x => new { x.ChannelId, x.ProfileId });

            modelBuilder.Entity<GroupProfile>()
             .HasKey(x => new { x.GroupId, x.ProfileId });

            



        }
    }

}
    

