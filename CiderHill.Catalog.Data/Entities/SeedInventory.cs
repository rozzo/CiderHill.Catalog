using System;

namespace CiderHill.Catalog.Data.Entities
{
    public enum TransactionType
    {
        /// <summary>
        /// Addition to inventory.
        /// </summary>
        Addition,

        /// <summary>
        /// Reduction in inventory.
        /// </summary>
        Reduction
    }

    public class SeedInventory : BaseNote<int>
    {
        public int SeedId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int Amount { get; set; }
        public bool Depleted { get; set; }
    }
}
