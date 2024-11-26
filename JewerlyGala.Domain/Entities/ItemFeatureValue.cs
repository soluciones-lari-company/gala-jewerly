namespace JewerlyGala.Domain.Entities
{
    public class ItemFeatureValue
    {
        public int Id { get; set; }
        public string ValueName { get; set; } = default!;
        public virtual ICollection<ItemFeatureToValue> ItemFeatureToValueNav { get; set; } = [];
    }
}
