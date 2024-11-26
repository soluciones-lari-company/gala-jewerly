namespace JewerlyGala.Domain.Entities
{
    public class ItemFeatureToValue
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public int ValueId { get; set; }
        public virtual ItemFeature? FeatureNav { get; set; }
        public virtual ItemFeatureValue? FeatureValueNav { get; set; }
        public virtual ICollection<ItemSerieToFeatureAndValue> ItemSerieToFeatureAndValueNav { get; set; } = [];
    }
}
