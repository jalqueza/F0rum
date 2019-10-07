using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IBoardRepository _boardRepository;
        private readonly IThreadRepository _threadRepository;
        private readonly IPostRepository _postRepository;

        public HomeController(IBoardRepository boardRepository,
                              IThreadRepository threadRepository,
                              IPostRepository postRepository)
        {
            _boardRepository = boardRepository;
            _threadRepository = threadRepository;
            _postRepository = postRepository;
        }
        public IActionResult Index()
        {
            var model = new HomeBoardsViewModel();
            model.Boards = new List<HomeBoardViewModel>();

            foreach (var board in _boardRepository.GetAllBoards())
            {
                var boardViewModel = new HomeBoardViewModel();
                boardViewModel.Board = board;
                boardViewModel.threadsCount = board.Threads.Count();
                boardViewModel.postsCount = 0;
                foreach (var thread in board.Threads)
                {
                    boardViewModel.postsCount = boardViewModel.postsCount + thread.Posts.Count();
                }
                boardViewModel.lastPost = (board.Threads.LastOrDefault() != null) ? board.Threads.OrderBy(t => t.DateTime).LastOrDefault().Posts.OrderBy(p => p.DateTime).LastOrDefault() : null;
                model.Boards.Add(boardViewModel);
            }
            return View(model);
        }
    }
}
