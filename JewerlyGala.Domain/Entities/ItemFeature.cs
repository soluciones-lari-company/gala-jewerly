namespace JewerlyGala.Domain.Entities
{
    public class ItemFeature
    {
        public int Id { get; set; }
        public string FeatureName { get; set; } = default!;
        public virtual ICollection<ItemFeatureToValue> ItemFeatureToValueNav { get; set; } = [];
    }
}
