using System.Collections.Generic;

namespace Web.Models
{
    public interface IThreadRepository
    {
        Thread GetById(int Id);
        IEnumerable<Thread> GetAllThreads();
        Thread Add(Thread board);
        Thread Update(Thread postChanges);
        Thread Delete(int Id);
    }
}
