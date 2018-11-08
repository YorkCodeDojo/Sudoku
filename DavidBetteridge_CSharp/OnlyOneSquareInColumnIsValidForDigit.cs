using System;
using System.Linq;

namespace Sudoku
{
    public class OnlyOneSquareInColumnIsValidForDigit : ISolver
    {
        public (bool success, Grid grid, Cell cell) Execute(Grid grid)
        {
            foreach (var column in grid.Columns())
            {
                var digitsToFillIn = grid.AllowedDigits
                                         .Where(digit => !column.Contains(digit));

                foreach (var digit in digitsToFillIn)
                {
                    var possibleRows = column.EmptyRows()
                                              .Where(rowNumber => !grid.Row(rowNumber).Contains(digit) &&
                                                                  !grid.Box(column.ColumnNumber, rowNumber).Contains(digit));

                    switch (possibleRows.Count())
                    {
                        case 0:
                            throw new Exception("Grid can't be solved");
                        case 1:
                            return (true, 
                                    grid.FillInSquare(column.ColumnNumber, possibleRows.First(), digit),
                                    new Cell(column.ColumnNumber, possibleRows.First()));
                        default:
                            break;
                    }
                }
            }

            return (false, grid, null);
        }

    }
}
