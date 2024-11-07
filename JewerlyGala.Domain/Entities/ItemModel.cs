namespace JewerlyGala.Domain.Entities
{
    public class ItemModel
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<ItemModelFeature> Features { get; set; }
    }
}