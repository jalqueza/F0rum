using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Models
{
    public class SQLPostRepository : IPostRepository
    {
        private AppDbContext context;

        public SQLPostRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Post Add(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
            return post;
        }

        public Post Delete(int Id)
        {
            Post post = this.GetById(Id);
            if (post.Replies.Count != 0)
            {
                post.Content = "[This post has been removed]";
                this.Update(post);
            }
            else if(post != null)
            {
                if(post.Reply != null)
                {
                    if (post.Reply.Content == "[This post has been removed]" && post.Reply.Replies.Count == 1)
                    {
                        context.Posts.Remove(post.Reply);
                        context.SaveChanges();
                    }
                }
                context.Posts.Remove(post);
                context.SaveChanges();
            }
            return post;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return context.Posts;
        }

        public Post GetById(int Id)
        {
            return context.Posts.Where(p => p.Id == Id)
                                  .Include(p => p.Reply)
                                    .ThenInclude(r => r.Replies)
                                  .Include(p => p.Replies)
                                  .FirstOrDefault();
        }

        public Post Update(Post postChanges)
        {
            var reply = context.Posts.Attach(postChanges); // attach changes
            reply.State = Microsoft.EntityFrameworkCore.EntityState.Modified; // state new changes made
            context.SaveChanges();
            return postChanges;
        }
    }
}
