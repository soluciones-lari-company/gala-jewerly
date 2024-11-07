namespace JewerlyGala.Domain.Entities
{
    public class ItemModelFeature
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public virtual ItemModel Model { get; set; }
        public virtual ICollection<ItemModelFeatureValue> Values { get; set; } 
    }
}
