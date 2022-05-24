using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EFrameworkDbFirst.Lesson.Models
{
    public partial class corsodotnetapplicantsContext : DbContext
    {
        public corsodotnetapplicantsContext()
        {
        }

        public corsodotnetapplicantsContext(DbContextOptions<corsodotnetapplicantsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<ApplicantSubmission> ApplicantSubmissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=corsodotnet.applicants; User id = sa ; password = Pass@word01;Trusted_Connection=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
