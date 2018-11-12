namespace Sudoku
{
    public class AllSolvers : ISolver
    {
        private readonly ISolver[] solvers = new ISolver[]
        {
                new OnlyOneSquareInBoxIsValidForDigit(),
                new OnlyOneSquareInColumnIsValidForDigit(),
                new OnlyOneSquareInRowIsValidForDigit(),
                new OnlyOneDigitIsValidForSquare()
        };
        public SolverResult TryToSolveOneCell(Grid grid, PencilMarks pencilMarks)
        {
            foreach (var solver in solvers)
            {
                var result = solver.TryToSolveOneCell(grid, pencilMarks);
                if (result.ProgressMade) return result;
            }

            return SolverResult.Failed(grid);
        }
    }
}
