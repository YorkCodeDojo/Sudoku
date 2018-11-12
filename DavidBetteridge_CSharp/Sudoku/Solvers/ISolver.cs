namespace Sudoku
{
    public interface ISolver
    {
        SolverResult TryToSolveOneCell(Grid grid, PencilMarks pencilMarks);
    }
}