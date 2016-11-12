using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueFit.Core
{
    [Table("Review")]
    public class Review
    {
        [MinLength(10)]
        [MaxLength(400)]
        public string Title { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Description { get; set; }
        [Column(Order =0)]
        [Key]
        public string UserId { get; set; }
        [Key]
        [Column(Order = 1)]
        [Required]
        public int RestaurantId { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }
 
    }
}
