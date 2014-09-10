using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyManager.Model
{
    public class AdvancedSearchModel
    {

        public string Name { get; set; }

        public DateTime? ShotAfter { get; set; }

        public DateTime? ShotBefore { get; set; }

        public string ShootingPlace { get; set; }

        public double? ShutterSpeed { get; set; }

        public string ISO { get; set; }

        public string CameraModel { get; set; }

        public string Diaphragm { get; set; }

        public double? FocalDistance { get; set; }

        public bool? Flash { get; set; }

    }
}
