using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EframeworkInMemory.Lesson.Persisntence
{
    public partial class ApplicantsContext : DbContext
    {
        public ApplicantsContext()
        {
        }

        public ApplicantsContext(DbContextOptions<ApplicantsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<ApplicantSubmission> ApplicantSubmissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                                optionsBuilder.UseSqlServer("Server=(local);Database=corsodotnet.applicants;Trusted_Connection=false; User Id=sa;Password=Pass@word01; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ApplicantSubmission>(entity =>
            {
                entity.Property(e => e.SubmissionDate).HasColumnType("datetime");

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.Applicant)
                    .WithMany(p => p.ApplicantSubmissions)
                    .HasForeignKey(d => d.ApplicantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicantSubmissions_Applicants");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
