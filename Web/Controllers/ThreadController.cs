using Web.Models;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    public class ThreadController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IThreadRepository _threadRepository;

        public ThreadController(IPostRepository postRepository,
                                IThreadRepository threadRepository)
        {
            _postRepository = postRepository;
            _threadRepository = threadRepository;
        }

        public IActionResult Index(int threadId)
        {

            ThreadIndexViewModel model = new ThreadIndexViewModel();
            model.Thread = _threadRepository.GetById(threadId);
            model.ThreadId = model.Thread.Id;
            model.Username = User.Identity.Name;
            return View(model);
        }

        [Authorize(Roles= "Admin, User")]
        [HttpPost]
        public IActionResult WritePost(ThreadIndexViewModel model)
        {
            

            if (model.CreatePostViewModel.Content != null)
            {
                Post newPost = new Post
                {
                    Content = model.CreatePostViewModel.Content,
                    ThreadId = model.ThreadId,
                    User = User.Identity.Name
            };
                _postRepository.Add(newPost);

                return RedirectToAction("index", new { threadId = model.ThreadId });
            }

            model.Thread = _threadRepository.GetById(model.ThreadId);
            model.Username = User.Identity.Name;

            return View("Index", model);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public IActionResult WriteReply(ThreadIndexViewModel model)
        {
            if (model.CreateReplyViewModel.Content != null)
            {
                if (model.SubmitType == "Reply")
                {
                    Post newPost = new Post
                    {
                        Content = model.CreateReplyViewModel.Content,
                        ThreadId = model.ThreadId,
                        ReplyId = model.ReplyId,
                        User = User.Identity.Name
                    };
                    _postRepository.Add(newPost);
                }
                else if (model.SubmitType == "Update")
                {
                    Post updatePost = _postRepository.GetById(model.UpdateId);
                    updatePost.Id = model.UpdateId;
                    updatePost.Content = model.CreateReplyViewModel.Content;
                    _postRepository.Update(updatePost);
                }
                else if(model.SubmitType == "Delete")
                {
                    _postRepository.Delete(model.DeleteId);
                }

                return RedirectToAction("index", new { threadId = model.ThreadId });
            }

            model.Thread = _threadRepository.GetById(model.ThreadId);
            model.Username = User.Identity.Name;

            return View("Index", model);
        }

        [Authorize(Roles= "Admin, User")]
        [HttpPost]
        public IActionResult DeletePost(ThreadIndexViewModel model)
        {
            _postRepository.Delete(model.DeleteId);
            return RedirectToAction("index", new { threadId = model.ThreadId });
        }
    }
}
