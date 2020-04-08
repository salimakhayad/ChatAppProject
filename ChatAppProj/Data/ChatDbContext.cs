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
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatProfile> ChatProfiles { get; set; }
        public DbSet<GroupProfile> GroupProfiles { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChatProfile>()
                .HasKey(x => new { x.ChatId, x.ProfileId });

            modelBuilder.Entity<GroupProfile>()
             .HasKey(x => new { x.GroupId, x.ProfileId });
        }
    }

}
    

