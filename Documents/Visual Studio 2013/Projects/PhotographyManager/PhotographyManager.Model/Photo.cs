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

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? ShootingDateTime { get; set; }

        [StringLength(100)]
        public string ShootingPlace { get; set; }

        [StringLength(100)]
        public string CameraModel { get; set; }

        public double? FocalLength { get; set; }

        [StringLength(100)]
        public string Diaphragm { get; set; }

        public double? ShutterSpeed { get; set; }

        [StringLength(100)]
        public string ISO { get; set; }

        public bool? Flash { get; set; }

        public virtual ICollection<Album> Album { get; set; }
    }
}
