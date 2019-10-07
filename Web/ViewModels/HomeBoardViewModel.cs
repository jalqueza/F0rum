using Web.Models;

namespace Web.ViewModels
{
    public class HomeBoardViewModel
    {
        public Board Board { get; set; }
        public Post lastPost { get; set; }
        public int threadsCount { get; set; }
        public int postsCount { get; set; }
    }
}
