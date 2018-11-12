using System.Collections.Generic;

namespace Sudoku
{
    public interface IPencilMarkReducer
    {
        List<string> Evaluate(Grid grid, PencilMarks pencilMarks);
    }
}