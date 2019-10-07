using Web.Models;

namespace Web.ViewModels
{
    public class BoardIndexThreadViewModel
    {
        public Thread Thread { get; set; }
        public int RepliesCount { get; set; }
        public Post lastPost { get; set; }
    }
}
