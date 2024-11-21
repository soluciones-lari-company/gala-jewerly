namespace JewerlyGala.Domain.Entities
{
    public class ItemSerieToFeatureAndValue
    {
        public Guid ItemSerieId { get; set; }
        public int ItemFeatureToValueId { get; set; }
        public virtual ItemFeatureToValue? ItemFeatureToValueNav { get; set; }
    }
}
