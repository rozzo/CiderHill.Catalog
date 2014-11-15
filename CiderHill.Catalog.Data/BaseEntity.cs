using System;

namespace CiderHill.Catalog.Data
{
    public abstract class BaseEntity<T> : AbstractEntity
    {
        public T Id { get; set; }

        public DateTime Created { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime Modified { get; set; }
        
        public string ModifiedBy { get; set; }
    }
}
