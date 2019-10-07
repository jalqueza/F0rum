using System.Collections.Generic;

namespace Web.Models
{
    public interface IBoardRepository
    {
        Board GetById(int Id);
        IEnumerable<Board> GetAllBoards();
        Board Add(Board board);
        Board Update(Board boardChanges);
        Board Delete(int Id);
    }
}
