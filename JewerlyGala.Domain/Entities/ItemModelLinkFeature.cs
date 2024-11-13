namespace JewerlyGala.Domain.Entities
{
    public class ItemModelLinkFeature
    {
        public int IdModel { get; set; }
        public int IdFeature { get; set; }
        public virtual ItemModel Model { get; set; }
        public virtual ItemModelFeature Feature { get; set; }
    }
}
