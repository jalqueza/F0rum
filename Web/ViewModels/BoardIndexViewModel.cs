using System.Collections.Generic;
using Web.Models;

namespace Web.ViewModels
{
    public class BoardIndexViewModel
    {
        public Board Board { get; set; }
        public IList<BoardIndexThreadViewModel> ThreadViewModels { get; set; }

    }
}
