namespace JewerlyGala.Domain.Entities
{
    public class ItemModelFeatureValue
    {
        public int Id { get; set; }
        public string ValueDetails { get; set; }
        public int IdFeature { get; set; }
        public virtual ItemModelFeature Feature { get; set; }
    }
}
