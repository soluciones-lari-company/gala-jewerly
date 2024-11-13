namespace JewerlyGala.Domain.Entities
{
    public class ItemModelFeatureLinkValue
    {
        public int IdFeature { get; set; }
        public int IdValue { get; set; }
        public virtual ItemModelFeature Feature { get; set; }
        public virtual ItemModelFeatureValue Value { get; set; }
    }
}
