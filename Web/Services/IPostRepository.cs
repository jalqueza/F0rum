using System.Collections.Generic;

namespace Web.Models
{
    public interface IPostRepository
    {
        Post GetById(int Id);
        IEnumerable<Post> GetAllPosts();
        Post Add(Post board);
        Post Update(Post replyChanges);
        Post Delete(int Id);
    }
}
