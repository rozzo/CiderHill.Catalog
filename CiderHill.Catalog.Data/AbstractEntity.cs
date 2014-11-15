using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiderHill.Catalog.Data
{
    public abstract class AbstractEntity : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
