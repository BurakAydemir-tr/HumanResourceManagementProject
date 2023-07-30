using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class HrmsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=HrmsNew;Trusted_Connection=True;TrustServerCertificate=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<JobAdvertisement> JobAdvertisements { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<JobExperience> JobExperiences { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<CV> CVs { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }
        public DbSet<EndpointOperationClaim> EndpointOperationClaim { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Candidate>().ToTable("Candidates");
            modelBuilder.Entity<Employer>().ToTable("Employers");
            modelBuilder.Entity<JobPosition>().ToTable("JobPositions");

            modelBuilder.Entity<Employer>().HasMany(x => x.JobAdvertisements)
                .WithOne(x => x.Employer);

            modelBuilder.Entity<JobPosition>().HasMany(x => x.JobAdvertisements)
                .WithOne(x => x.JobPosition);

            modelBuilder.Entity<Candidate>().HasOne(x => x.CV)
                .WithOne(x=>x.Candidate);

            modelBuilder.Entity<CV>().HasMany(x => x.Universities)
                .WithOne(x => x.CV);

            modelBuilder.Entity<CV>().HasMany(x => x.JobExperiences)
                .WithOne(x => x.CV);

            modelBuilder.Entity<CV>().HasMany(x => x.Technologies)
                .WithOne(x => x.CV);

            modelBuilder.Entity<UserOperationClaim>().HasKey(ky => new {ky.UserId, ky.OperationClaimId});

            modelBuilder.Entity<UserOperationClaim>().HasOne(k=>k.User)
                .WithMany(x=>x.UserOperationClaims)
                .HasForeignKey(x=>x.UserId);

            modelBuilder.Entity<UserOperationClaim>().HasOne(k => k.OperationClaim)
                .WithMany(x => x.UserOperationClaims)
                .HasForeignKey(x => x.OperationClaimId);

            modelBuilder.Entity<EndpointOperationClaim>().HasKey(k => new {k.EndpointsId,k.OperationClaimsId});

            modelBuilder.Entity<EndpointOperationClaim>().HasOne(k => k.Endpoint)
                .WithMany(x=>x.EndpointOperationClaims)
                .HasForeignKey(x=>x.EndpointsId);

            modelBuilder.Entity<EndpointOperationClaim>().HasOne(k => k.OperationClaim)
                .WithMany(x => x.EndpointOperationClaims)
                .HasForeignKey(x => x.OperationClaimsId);

        }

    }
}
