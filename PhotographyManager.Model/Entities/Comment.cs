using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyManager.Model
{
    public class Comment: IEntity
    {
        public int ID { get; set; }

        public string Text { get; set; }

        public virtual User User { get; set; }

        public virtual Photo Photo { get; set; }
    }
}
