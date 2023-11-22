using System.ComponentModel.DataAnnotations;

namespace OnlineFoodService.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int? ItemId { get; set; }
        public int? Quantity { get; set; }

        public string? Status { get; set; }
        public virtual UserDetail? User { get; set; }
        public virtual Menu? Item { get; set; }
    }
}
