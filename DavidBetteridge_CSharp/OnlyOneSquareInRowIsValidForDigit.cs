using System;
using System.Linq;

namespace Sudoku
{
    public class OnlyOneSquareInRowIsValidForDigit : ISolver
    {
        public (bool success, Grid grid, Cell cell) Execute(Grid grid)
        {

            for (int rowNumber = 0; rowNumber < 9; rowNumber++)
            {
                var digitsToFillIn = grid.AllowedDigits
                                         .Where(digit => !grid.Row(rowNumber).Contains(digit));

                foreach (var digit in digitsToFillIn)
                {
                    var possibleColumns = grid.EmptyCellsInRow(rowNumber)
                                              .Where(cell => !grid.Column(cell.ColumnNumber).Contains(digit) &&
                                                             !grid.Box(cell.ColumnNumber, cell.RowNumber).Contains(digit))
                                              .Select(cell => cell.ColumnNumber);

                    switch (possibleColumns.Count())
                    {
                        case 0:
                            throw new Exception("Grid can't be solved");
                        case 1:
                            return (true, grid.FillInSquare(possibleColumns.First(), rowNumber, digit), new Cell(possibleColumns.First(), rowNumber));
                        default:
                            break;
                    }
                }
            }


            return (false, grid, null);
        }

    }
}
