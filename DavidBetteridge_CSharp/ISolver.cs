namespace Sudoku
{
    public interface ISolver
    {
        (bool success, Grid grid, Cell cell) Execute(Grid grid);
    }
}