using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineFoodService.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderId { get; set; }

        //Foreign Key
        //[Display(Name = "Cart")]

        public int CartId { get; set; }

        //[ForeignKey("CartId")]

        public virtual Cart? Cart { get; set; }

        public DateTime OrderTime { get; set; }

        public string? Payment { get; set; }

        public string? Status { get; set; }
    }
}
