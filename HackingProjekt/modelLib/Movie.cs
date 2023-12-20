using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HackingProjekt.modelLib
{
    public class movie
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Udkomst { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; } = string.Empty;

        [RegularExpression(@"^[a-zA-Z\s]*$")]
        [Required]
        public string Andmenelse { get; set; } = string.Empty;


        [Required]
        public string Forfatter { get; set; } = string.Empty;

        [Required]
        public string Aldersgrænse { get; set; } = string.Empty;
    } 
}
