namespace PhotographyManager.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserRoles:IEntity
    {
        public UserRoles()
        {
            User = new HashSet<User>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(256)]
        public string RoleName { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
