using System;
using System.Linq;

namespace Sudoku
{
    public class OnlyOneSquareInRowIsValidForDigit : ISolver
    {
        public (bool success, Grid grid, Cell cell) Execute(Grid grid)
        {
            foreach (var row in grid.Rows())
            {
                var digitsToFillIn = grid.AllowedDigits
                                         .Where(digit => !row.Contains(digit));

                foreach (var digit in digitsToFillIn)
                {
                    var possibleColumns = row.EmptyColumns()
                                              .Where(columnNumber => !grid.Column(columnNumber).Contains(digit) &&
                                                                     !grid.Box(columnNumber, row.RowNumber).Contains(digit));

                    switch (possibleColumns.Count())
                    {
                        case 0:
                            throw new Exception("Grid can't be solved");
                        case 1:
                            return (true, 
                                    grid.FillInSquare(possibleColumns.First(), row.RowNumber, digit),
                                    new Cell(possibleColumns.First(), row.RowNumber));
                        default:
                            break;
                    }
                }
            }


            return (false, grid, null);
        }

    }
}
