using System.ComponentModel.DataAnnotations;

namespace SkylerO_AmplifundProj.Models
{
    public class Book
    {
        [Required] public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public DateTime PublishDate { get; set; }
        public string? Description { get; set; }
        [Required] public string AuthorName { get; set; }

    }
}
