namespace CatApiApp.Models
{
    /// <summary>
    /// Represents the CatEntity stored in the database.
    /// </summary>
    public class CatEntity
    {
        public int Id { get; set; }
        public string CatId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Image { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public List<TagEntity> Tags { get; set; } = [];
    }
}
