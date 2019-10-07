using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class CreatePostViewModel
    {
        [Required(ErrorMessage = "Post Content Required")]
        public string Content { get; set; }
    }
}