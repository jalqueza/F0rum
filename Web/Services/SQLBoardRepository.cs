using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Web.Models
{
    public class SQLBoardRepository : IBoardRepository
    {
        private AppDbContext context;

        public SQLBoardRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Board Add(Board board)
        {
            context.Boards.Add(board);
            context.SaveChanges();
            return board;
        }

        public Board Delete(int Id)
        {
            Board board = context.Boards.Find(Id);
            if(board != null)
            {
                context.Boards.Remove(board);
                context.SaveChanges();
            }
            return board;
        }

        public IEnumerable<Board> GetAllBoards()
        {
            return context.Boards.Include(b => b.Threads)
                                    .ThenInclude(t => t.Posts);
        }

        public Board GetById(int Id)
        {
            return context.Boards.Where(b => b.Id == Id)
                                 .Include(b => b.Threads)
                                    .ThenInclude(t => t.Posts)
                                  .FirstOrDefault();
        }

        public Board Update(Board boardChanges)
        {
            var board = context.Boards.Attach(boardChanges);
            board.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return boardChanges;
        }
    }
}
