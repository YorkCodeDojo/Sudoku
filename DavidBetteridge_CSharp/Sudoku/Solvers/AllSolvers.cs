namespace Sudoku
{
    public class AllSolvers : ISolver
    {
        private readonly ISolver[] solvers = new ISolver[]
        {
                new OnlyOnePencilMarkInCell(),
                new OnlyOneSquareInRowIsValidForDigit(),
                new OnlyOneSquareInColumnIsValidForDigit(),
                new OnlyOneSquareInBoxIsValidForDigit(),
                new OnlyOneDigitIsValidForSquare()
        };
        public SolverResult TryToSolveOneCell(Grid grid)
        {
            foreach (var solver in solvers)
            {
                var result = solver.TryToSolveOneCell(grid);
                if (result.ProgressMade) return result;
            }

            return SolverResult.Failed(grid);
        }
    }
}
