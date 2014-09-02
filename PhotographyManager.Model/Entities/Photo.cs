namespace PhotographyManager.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Photo")]
    public partial class Photo
    {
        public Photo()
        {
            Album = new HashSet<Album>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public byte[] Image { get; set; }

        public byte[] MiniImage { get; set; }

        public byte[] MiddleImage { get; set; }

        public DateTime? ShootingTime { get; set; }

        [StringLength(100)]
        public string ShootingPlace { get; set; }

        public double? ShutterSpeed { get; set; }

        [StringLength(100)]
        public string ISO { get; set; }

        [MaxLength(100)]
        public string CameraModel { get; set; }

        [StringLength(100)]
        public string Diaphragm { get; set; }

        public double? FocalDistance { get; set; }

        public bool? Flash { get; set; }

        public virtual ICollection<Album> Album { get; set; }
    }
}
