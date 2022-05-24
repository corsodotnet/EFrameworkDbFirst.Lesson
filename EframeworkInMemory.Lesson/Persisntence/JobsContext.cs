using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EframeworkInMemory.Lesson.Persisntence
{
    public partial class JobsContext : DbContext
    {
        public JobsContext()
        {
        }

        public JobsContext(DbContextOptions<JobsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobApplicant> JobApplicants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=(local);Database=corsodotnet.jobs;Trusted_Connection=false; User Id=sa;Password=Pass@word01; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.PostedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<JobApplicant>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobApplicants)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobApplicants_Jobs");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
