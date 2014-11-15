using System;

namespace CiderHill.Catalog.Data.Entities
{
    public class SeedPlanting : BasePlantEntity<int>
    {
        public int SeedId { get; set; }
        public DateTime PlantDate { get; set; }
        public int RowNumber { get; set; }
        public string RowLocation { get; set; }
        public string Yield { get; set; }
    }
}
