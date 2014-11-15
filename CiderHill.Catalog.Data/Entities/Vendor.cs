namespace CiderHill.Catalog.Data.Entities
{
    public class Vendor : BaseEntity<int>
    {
        public string Name { get; set; }
        public string WebSite { get; set; }
        public string ProductBaseUrl { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
