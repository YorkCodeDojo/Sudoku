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

            var success = true;
            while (success)
            {
                var result = allSolvers.TryToSolveOneCell(grid);
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
