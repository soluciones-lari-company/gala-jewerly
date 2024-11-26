namespace JewerlyGala.Domain.Queries
{
    public class QItemSerieFeatureValues
    {
        public Guid SerieId { get; set; }
        public string Feature { get; set; } = default!;
        public string Value { get; set; } = default!;
    }
}
