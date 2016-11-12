using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueFit.Core { 
    [Table("Restaurant")]
    public partial class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(400)]
        public string Name { get; set; }

        [Required]
        [MaxLength]
        public string City { get; set; }

        [MaxLength(500)]
        public string State { get; set; }

        [Required]
        [MaxLength(500)]
        public string Country { get; set; }

        [Required]
        [MaxLength(20)]
        public string ZipCode { get; set; }

        public int? Rating { get; set; }
    }
}
