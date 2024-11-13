namespace JewerlyGala.Domain.Entities
{
    public class ItemModel
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<ItemModelLinkFeature> Features { get; set; }
    }
}