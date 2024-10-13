using System.ComponentModel.DataAnnotations;

namespace CatApiApp.Models
{
    /// <summary>
    /// Represents the CatEntity stored in the database.
    /// </summary>
    public class CatEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "CatId is required")]
        public string CatId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Width must be greater than 0")]
        public int Width { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Height must be greater than 0")]
        public int Height { get; set; }

        [Required(ErrorMessage = "Image is required")]
        [Url(ErrorMessage = "The Image field must be a valid URL.")] 
        public string Image { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public List<TagEntity> Tags { get; set; } = [];
    }
}
