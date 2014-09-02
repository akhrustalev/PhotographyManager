namespace PhotographyManager.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Album")]
    public partial class Album
    {
        public Album()
        {
            Photo = new HashSet<Photo>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Discription { get; set; }

        public int UserID { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Photo> Photo { get; set; }
    }
}
