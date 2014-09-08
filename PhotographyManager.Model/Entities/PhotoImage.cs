namespace PhotographyManager.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhotoImage")]
    public partial class PhotoImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("Photo")]
        public int ID { get; set; }

        [Column(TypeName = "image")]
        public byte[] BigImage { get; set; }

        [Column(TypeName = "image")]
        public byte[] MiddleImage { get; set; }

        [Column(TypeName = "image")]
        public byte[] MiniImage { get; set; }

        public virtual Photo Photo { get; set; }
    }
}
