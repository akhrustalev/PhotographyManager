namespace PhotographyManager.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log")]
    public partial class Log
    {

        public int ID { get; set; }


        public DateTime Date { get; set; }


        public string Thread { get; set; }


        public string Level { get; set; }


        public string Logger { get; set; }


        public string Message { get; set; }

        public string Exception { get; set; }
    }
}
