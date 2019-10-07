using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Models
{
    public class SQLThreadRepository : IThreadRepository
    {
        private AppDbContext context;

        public SQLThreadRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Thread Add(Thread thread)
        {
            context.Threads.Add(thread);
            context.SaveChanges();
            return thread;
        }

        public Thread Delete(int Id)
        {
            Thread thread = context.Threads.Find(Id);
            if(thread != null)
            {
                context.Threads.Remove(thread);
                context.SaveChanges();
            }
            return thread;
        }

        public IEnumerable<Thread> GetAllThreads()
        {
            return context.Threads;
        }

        public Thread GetById(int Id)
        {
            return context.Threads.Where(t => t.Id == Id)
                                  .Include(t => t.Posts)
                                  .FirstOrDefault();
        }

        public Thread Update(Thread threadChanges)
        { 
            var thread = context.Threads.Attach(threadChanges);
            thread.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return threadChanges;
        }
    }
}
