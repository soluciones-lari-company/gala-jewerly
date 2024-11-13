namespace JewerlyGala.Domain.Entities
{
    public class ItemModelFeatureValue
    {
        public int Id { get; set; }
        public string ValueDetails { get; set; }
        public virtual ICollection<ItemModelFeatureLinkValue> Features { get; set; }
    }
}
