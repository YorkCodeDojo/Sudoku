namespace Sudoku
{
    public class OnlyOnePencilMarkInCell : ISolver
    {
        public SolverResult TryToSolveOneCell(Grid grid)
        {
            var pencilMarks = new PencilMarks(grid);

            var pencilMarkReducer = new DoublePairs();//  CandidateLines();
            if (pencilMarkReducer.Evaluate(grid, pencilMarks))
            {
                var (columnNumber, rowNumber, digit) = pencilMarks.TryFindSinglePencilMark();
                if (columnNumber > -1)
                {
                    var location = new Cell(columnNumber, rowNumber);
                    var newGrid = grid.FillInSquare(columnNumber, rowNumber, digit);
                    return SolverResult.Success(newGrid, location, "Other option removed by candidate lines rule");
                }
            }

            return SolverResult.Failed(grid);
        }
    }
}
