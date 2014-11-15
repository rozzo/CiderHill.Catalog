using CiderHill.Catalog.Data.Entities;

namespace CiderHill.Catalog.Data
{
    public abstract class BasePlantEntity<T> : BaseEntity<T>
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int VendoryId { get; set; }
        public string ProductNumber { get; set; }

        public virtual Category Category { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
