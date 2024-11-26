namespace JewerlyGala.Domain.Entities
{
    public class ItemMaterial
    {
        public int Id { get; set; }
        public string MaterialName { get; set; } = default!;
        public virtual ICollection<ItemSerie> ItemSeriesNav { get; set; } = [];
    }
}
