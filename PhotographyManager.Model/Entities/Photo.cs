namespace PhotographyManager.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Photo")]
    public partial class Photo:IEntity
    {
        public Photo()
        {
            Album = new HashSet<Album>();
            Comment = new HashSet<Comment>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public DateTime? ShootingTime { get; set; }

        [StringLength(100)]
        public string ShootingPlace { get; set; }

        public double? ShutterSpeed { get; set; }

        [StringLength(100)]
        public string ISO { get; set; }

        [StringLength(100)]
        public string CameraModel { get; set; }

        [StringLength(100)]
        public string Diaphragm { get; set; }

        public double? FocalDistance { get; set; }

        public bool Flash { get; set; }

        public int UserID { get; set; }

        public virtual User User { get; set; }

        public virtual PhotoImage PhotoImage { get; set; }

        public virtual ICollection<Album> Album { get; set; }

        public virtual ICollection<Comment> Comment { get; set; }
    }
}
