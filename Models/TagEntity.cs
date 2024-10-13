using System.ComponentModel.DataAnnotations;

namespace CatApiApp.Models
{
    /// <summary>
    /// Represents the TagEntity related to a cat's temperament or breed.
    /// </summary>
    public class TagEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tag name is required")]
        [MaxLength(50, ErrorMessage = "Tag name cannot be longer than 50 characters")]
        public string Name { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public List<CatEntity> Cats { get; set; } = [];
    }
}
