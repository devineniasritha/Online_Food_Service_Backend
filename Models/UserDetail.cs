using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodService.Models
{
    public class UserDetail
    {

        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public string? Mobileno { get; set; }
        public string? Address { get; set; }
        public string? UserType { get; set; }
    }
}
