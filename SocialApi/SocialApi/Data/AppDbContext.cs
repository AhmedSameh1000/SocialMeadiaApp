using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialApi.Models;
using ZwajApp.api.Models;

namespace ZwajApp.api.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<UserConnection>   UserConnections  { get; set; }

        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Like>().HasKey(k => new { k.LikerId, k.LikeeId });

            builder.Entity<Like>()
                .HasOne(l => l.Likee)
                .WithMany(le => le.Likers)
                .HasForeignKey(k=>k.LikeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
                .HasOne(l => l.Liker)
                .WithMany(le => le.Likeees).
                HasForeignKey(k => k.LikerId)
                .OnDelete(DeleteBehavior.Restrict); ;

            builder.Entity<Message>()
           .HasOne(m => m.sender)
           .WithMany(u => u.messagesSent)
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(m => m.recipient)
                .WithMany(u => u.messagesRiceved)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<UserConnection>().HasKey(k=>new {k.userId,k.connectionId});

            base.OnModelCreating(builder);
        }
    }
}
