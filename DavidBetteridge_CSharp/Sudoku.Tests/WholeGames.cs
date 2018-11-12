using Xunit;

namespace Sudoku.Tests
{
    public class WholeGames
    {
        [Fact]
        public void AnEasyBoardCanBeSolved()
        {
            var grid = GridBuilder.Easy();

            var allSolvers = new AllSolvers();
            var pencilMarkReducer = new PencilMarkReducer();

            var success = true;
            while (success)
            {
                var pencilMarks = new PencilMarks(grid);
                var reductions = pencilMarkReducer.Reduce(grid, pencilMarks);

                var result = allSolvers.TryToSolveOneCell(grid, pencilMarks);
                if (result.ProgressMade)
                {
                    grid = result.Grid;
                }
                success = result.ProgressMade;
            }

            var notValidBecause = grid.Validate();
            Assert.True(string.IsNullOrWhiteSpace(notValidBecause));

        }
    }
}
