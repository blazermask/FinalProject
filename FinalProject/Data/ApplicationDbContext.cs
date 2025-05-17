using Microsoft.EntityFrameworkCore;
using FinalProject.Data.Entities;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace FinalProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ForumPostEntity> ForumPosts { get; set; }
		public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<VoteEntity> Votes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=[REDACTED];database=[REDACTED];user=[REDACTED];password=[REDACTED];port=[REDACTED];";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36)); // или версията на твоя MySQL сървър

            optionsBuilder.UseMySql(connectionString, serverVersion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
        }

      
    }
}
