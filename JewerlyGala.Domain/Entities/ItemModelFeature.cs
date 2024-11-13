namespace JewerlyGala.Domain.Entities
{
    public class ItemModelFeature
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        //public int IdModel { get; set; }
        //public virtual ItemModel Model { get; set; }
        public virtual ICollection<ItemModelFeatureLinkValue> Values { get; set; }
        public virtual ICollection<ItemModelLinkFeature> Models { get; set; }
    }
}
