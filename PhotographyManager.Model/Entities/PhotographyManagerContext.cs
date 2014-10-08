namespace PhotographyManager.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PhotographyManagerContext : DbContext
    {
        public PhotographyManagerContext()
            : base("name=PhotographyManagerContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<UserMembership> Membership { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<PhotoImage> PhotoImage { get; set; }
        public virtual DbSet<UserRoles> Roles { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Log> Log { get; set; }

        public virtual DbSet<Comment> Comment { get; set; }

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

            modelBuilder.Entity<Photo>()
                .HasOptional(e => e.PhotoImage)
                .WithRequired(e => e.Photo);

            modelBuilder.Entity<UserRoles>()
                .HasMany(e => e.User)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("UsersInRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Album)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Membership)
                .WithRequired(e => e.User);

            modelBuilder.Entity<Log>()
                .Property(e => e.Thread)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Level)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Logger)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Exception)
                .IsUnicode(false);
        }
    }
}
