namespace PhotographyManager.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public abstract partial class User:IEntity
    {
        public User()
        {
            Album = new HashSet<Album>();
            Photo = new HashSet<Photo>();
            Roles = new HashSet<UserRoles>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Album> Album { get; set; }

        public virtual UserMembership Membership { get; set; }

        public virtual ICollection<Photo> Photo { get; set; }

        public virtual ICollection<UserRoles> Roles { get; set; }
    }
}
