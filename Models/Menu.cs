using System.ComponentModel.DataAnnotations;

namespace OnlineFoodService.Models
{
    public class Menu
    {
        [Key]
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public float? ItemCost { get; set; }
        public string? Category { get; set; }

        public string? Image { get; set; }
    }
}
