using Xunit;

namespace Sudoku.Tests
{
    public class ValidationTests
    {
        [Fact]
        public void ValidBoard()
        {
            var grid = GridBuilder.Easy();
            var actualResult = grid.ValidatePartialBoard();

            Assert.Equal("", actualResult);
        }

        [Fact]
        public void InvalidBoard_Too_Many_Twos_In_Row_0()
        {
            var grid = GridBuilder.Easy();
            grid = grid.FillInSquare(0, 0, '2');
            grid = grid.FillInSquare(1, 0, '2');

            var actualResult = grid.ValidatePartialBoard();

            Assert.Equal("The digit 2 occurs 3 times in row 0.", actualResult);
        }


        [Fact]
        public void InvalidBoard_Too_Many_Threes_In_Column_2()
        {
            var grid = GridBuilder.Easy();
            grid = grid.FillInSquare(2, 0, '3');
            grid = grid.FillInSquare(2, 1, '3');

            var actualResult = grid.ValidatePartialBoard();

            Assert.Equal("The digit 3 occurs 2 times in column 2.", actualResult);
        }
    }
}
