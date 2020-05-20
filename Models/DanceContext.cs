using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Laba1_ISTTP
{
    public partial class DanceContext : DbContext
    {
        public DanceContext()
        {
        }

        public DanceContext(DbContextOptions<DanceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Choreography> Choreography { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Competition> Competition { get; set; }
        public virtual DbSet<DanceStudio> DanceStudio { get; set; }
        public virtual DbSet<Dancer> Dancer { get; set; }
        public virtual DbSet<Dstyle> Dstyle { get; set; }
        public virtual DbSet<Nomination> Nomination { get; set; }
        public virtual DbSet<NominationList> NominationList { get; set; }
        public virtual DbSet<Organizer> Organizer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server= DESKTOP-S0325RD\SQLEXPRESS; 

Database= Dance; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Choreography>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DancerId).HasColumnName("DancerID");

                entity.Property(e => e.Duration)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Music)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.StyleId).HasColumnName("StyleID");

                entity.HasOne(d => d.Dancer)
                    .WithMany(p => p.Choreography)
                    .HasForeignKey(d => d.DancerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Choreography_Dancer");

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.Choreography)
                    .HasForeignKey(d => d.StyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Choreography_DStyle");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Information)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Competition>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.Information)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.OrganizerId).HasColumnName("OrganizerID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Competition)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Competition_City");

                entity.HasOne(d => d.Organizer)
                    .WithMany(p => p.Competition)
                    .HasForeignKey(d => d.OrganizerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Competition_Organizer");
            });

            modelBuilder.Entity<DanceStudio>(entity =>
            {
                entity.ToTable("Dance Studio");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adress)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.DanceStudio)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dance Studio_City");
            });

            modelBuilder.Entity<Dancer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.DanceStudioId).HasColumnName("DanceStudioID");

                entity.Property(e => e.Information)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.DanceStudio)
                    .WithMany(p => p.Dancer)
                    .HasForeignKey(d => d.DanceStudioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dancer_Dance Studio");
            });

            modelBuilder.Entity<Dstyle>(entity =>
            {
                entity.ToTable("DStyle");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Information)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Nomination>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChoreographyId).HasColumnName("ChoreographyID");

                entity.Property(e => e.CompetitionId).HasColumnName("CompetitionID");

                entity.Property(e => e.DancerId).HasColumnName("DancerID");

                entity.Property(e => e.NomListId).HasColumnName("NomListID");

                entity.Property(e => e.Place)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Choreography)
                    .WithMany(p => p.Nomination)
                    .HasForeignKey(d => d.ChoreographyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Nomination_Choreography");

                entity.HasOne(d => d.Competition)
                    .WithMany(p => p.Nomination)
                    .HasForeignKey(d => d.CompetitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Nomination_Competition");

                entity.HasOne(d => d.Dancer)
                    .WithMany(p => p.Nomination)
                    .HasForeignKey(d => d.DancerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Nomination_Dancer");

                entity.HasOne(d => d.NomList)
                    .WithMany(p => p.Nomination)
                    .HasForeignKey(d => d.NomListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Nomination_Nomination List");
            });

            modelBuilder.Entity<NominationList>(entity =>
            {
                entity.ToTable("Nomination List");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Organizer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
