namespace Sudoku
{
    public interface ISolver
    {
        SolverResult Execute(Grid grid);
    }
}