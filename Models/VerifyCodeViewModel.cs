using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class VerifyCodeViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { set; get; }
        public bool Status { set; get; }
        public string Message { set; get; }
    }
}