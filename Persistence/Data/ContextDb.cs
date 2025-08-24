using Models.Models;
using Persistence.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Repositories;

namespace Persistence.Data
{
    public class ContextDb(DbContextOptions<ContextDb> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("id");
                entity.Property(e => e.Username).HasColumnName("username");
                entity.Property(e => e.Password).HasColumnName("password");

                entity.HasIndex(e => e.Username).IsUnique();

                entity.ToTable("user");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("id");
                entity.Property(e => e.TeamA).HasColumnName("team_a");
                entity.Property(e => e.TeamB).HasColumnName("team_b");
                entity.Property(e => e.PriceTicket).HasColumnName("price_ticket");
                entity.Property(e => e.NumberOfSeatsTotal).HasColumnName("number_of_seats_total");
                entity.Property(e => e.MatchType).HasConversion<string>().HasColumnName("match_type");

                entity.HasMany(e => e.Tickets)
                    .WithOne(e => e.Match)
                    .HasForeignKey(e => e.MatchId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.ToTable("match");
            });


            modelBuilder.Entity<Ticket>(
                entity =>
                {
                    entity.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("id");
                    entity.Property(e => e.FirstName).HasColumnName("first_name");
                    entity.Property(e => e.LastName).HasColumnName("last_name");
                    entity.Property(e => e.Address).HasColumnName("address");
                    entity.Property(e => e.NumberOfSeats).HasColumnName("number_of_seats_ticket");

                    entity.Property(e => e.MatchId)
                        .HasColumnName("match_id")
                        .IsRequired();

                    entity.ToTable("ticket");
                }
            );
            
        }
    }
}
