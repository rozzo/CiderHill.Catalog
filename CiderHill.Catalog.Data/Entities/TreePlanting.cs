using System;

namespace CiderHill.Catalog.Data.Entities
{
    public class TreePlanting : BasePlantEntity<int>
    {
        public int TreeId { get; set; }
        public DateTime PlantDate { get; set; }
        public int LocationNumber { get; set; }
    }
}
