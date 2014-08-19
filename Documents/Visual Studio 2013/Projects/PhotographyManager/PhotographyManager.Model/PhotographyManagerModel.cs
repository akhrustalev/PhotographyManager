namespace PhotographyManager.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Infrastructure;

    public partial class PhotographyManagerModel : DbContext
    {
        public PhotographyManagerModel()
            : base("name=PhotographyManagerModel")
        {
        }

        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<FreeUser> FreeUser { get; set; }

        public virtual DbSet<PaidUser> PaidUser { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Album>()
                .HasMany(e => e.Photo)
                .WithMany(e => e.Album)
                .Map(m => m.ToTable("Album2Photo").MapLeftKey("AlbumID").MapRightKey("PhotoID"));

            modelBuilder.Entity<Photo>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Photo>()
                .Property(e => e.ShootingPlace)
                .IsUnicode(false);

            modelBuilder.Entity<Photo>()
                .Property(e => e.ISO)
                .IsUnicode(false);

            modelBuilder.Entity<Photo>()
                .Property(e => e.Diaphragm)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Album)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
        }
    }
}
