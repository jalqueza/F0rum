using System.ComponentModel.DataAnnotations;
using Web.Models;

namespace Web.ViewModels
{
    public class ThreadIndexViewModel
    {
        public Thread Thread { get; set; }
        public int ThreadId { get; set; }
        public CreatePostViewModel CreatePostViewModel { get; set; }
        public CreateReplyViewModel CreateReplyViewModel { get; set; }
        public int ReplyId { get; set; }
        public string SubmitType { get; set; }
        public int UpdateId { get; set; }
        public int DeleteId { get; set; }
        public string Username { get; set; }
    }
}