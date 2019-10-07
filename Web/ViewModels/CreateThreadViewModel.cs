using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class CreateThreadViewModel
    {
        [Required]
        public string Title { get; set; }
        public int BoardId { get; set; }
        public string BoardTitle { get; set; }
        public DateTime DateTime { get; set; }
        [Required]
        public string Content { get; set; }
        public CreateThreadViewModel()
        {
            DateTime = DateTime.Now;
        }
    }
}
