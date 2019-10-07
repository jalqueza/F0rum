using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class BoardController : Controller
    {

        private readonly IBoardRepository _boardRepository;
        private readonly IPostRepository _postRepository;
        private readonly IThreadRepository _threadRepository;

        public BoardController(IBoardRepository boardRepository,
                              IPostRepository postRepository,
                              IThreadRepository threadRepository)
        {
            _boardRepository = boardRepository;
            _postRepository = postRepository;
            _threadRepository = threadRepository;
        }

        public IActionResult Index(int boardId)
        {
            BoardIndexViewModel model = new BoardIndexViewModel();
            model.Board = _boardRepository.GetById(boardId);

            model.ThreadViewModels = new List<BoardIndexThreadViewModel>();
            foreach (var thread in model.Board.Threads)
            {
                BoardIndexThreadViewModel boardIndexThreadViewModel = new BoardIndexThreadViewModel();
                boardIndexThreadViewModel.Thread = thread;
                boardIndexThreadViewModel.RepliesCount = thread.Posts.Count();
                boardIndexThreadViewModel.lastPost = thread.Posts.OrderBy(p => p.DateTime).LastOrDefault();
                model.ThreadViewModels.Add(boardIndexThreadViewModel);
            }

            return View(model);
        }

        [HttpGet]
        public ViewResult CreateThread(int boardId)
        {
            CreateThreadViewModel model = new CreateThreadViewModel();
            model.BoardTitle = _boardRepository.GetById(boardId).Title;
            model.BoardId = boardId;
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateThread(CreateThreadViewModel model)
        {
            if (ModelState.IsValid)
            {
                Thread newThread = new Thread
                {
                    Title = model.Title,
                    BoardId = model.BoardId
                };
                _threadRepository.Add(newThread);

                Post newPost = new Post
                {
                    Content = model.Content,
                    ThreadId = newThread.Id
                };
                _postRepository.Add(newPost);

                return RedirectToAction("index", new { boardId = model.BoardId });
            }
            return View(model);
        }
    }
}