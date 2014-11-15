using System.Collections.Generic;

namespace CiderHill.Catalog.Data.Entities
{
    public class Tree : BasePlantEntity<int>
    {
        public string Harvest { get; set; }
        public string Zone { get; set; }

        public virtual List<TreePlanting> TreePlantings { get; set; }
        public virtual List<TreeNote> TreeNotes { get; set; }
    }
}
