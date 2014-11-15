namespace CiderHill.Catalog.Data
{
    public abstract class BaseNote<T> : BaseEntity<T>
    {
        public string Note { get; set; }
    }
}
