using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class CreateReplyViewModel
    {
        [Required(ErrorMessage = "Reply Content Required")]
        public string Content { get; set; }
    }
}