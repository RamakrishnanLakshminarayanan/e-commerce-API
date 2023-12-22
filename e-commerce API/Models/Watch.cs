using System.ComponentModel.DataAnnotations;

namespace e_commerce_API.Models
{
    public class Watch
    {
        [Required]
        public int Watch_ID { get; set; }
        [Required]
        public string Watch_Name { get; set; }
        [Required]
        [Range(0.01, int.MaxValue)]
        public decimal? Unit_Price { get; set; }
        [Required]
        [Range(0.01, int.MaxValue)]
        public decimal? TotalPrice { get; set; } = null;
        public Discount? Discount { get; set; }
    }
}
