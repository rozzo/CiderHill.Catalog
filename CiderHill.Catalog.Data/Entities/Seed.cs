using System.Collections.Generic;

namespace CiderHill.Catalog.Data.Entities
{
    public class Seed : BaseEntity<int>
    {
        public int Days { get; set; }
        public bool CoolWeatherCrop { get; set; }

        public virtual List<SeedPlanting> SeedPlantings { get; set; }
        public virtual List<SeedNote> SeedNotes { get; set; }
        public virtual List<SeedInventory> SeedInventory { get; set; }
    }
}
