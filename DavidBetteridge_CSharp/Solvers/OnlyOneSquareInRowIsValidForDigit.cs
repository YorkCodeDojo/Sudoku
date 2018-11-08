using System;
using System.Linq;

namespace Sudoku
{
    public class OnlyOneSquareInRowIsValidForDigit : ISolver
    {
        public SolverResult TryToSolveOneCell(Grid grid)
        {
            foreach (var row in grid.Rows())
            {
                var digitsToFillIn = grid.AllowedDigits
                                         .Where(digit => row.DoesNotContain(digit));

                foreach (var digit in digitsToFillIn)
                {
                    var possibleColumns = row.EmptyColumns()
                                              .Where(columnNumber => grid.Column(columnNumber).DoesNotContain(digit) &&
                                                                     grid.Box(columnNumber, row.RowNumber).DoesNotContain(digit));

                    switch (possibleColumns.Count())
                    {
                        case 0:
                            throw new Exception("Grid can't be solved");

                        case 1:
                            var newCell = new Cell(possibleColumns.First(), row.RowNumber);
                            var updatedGrid = grid.FillInSquare(possibleColumns.First(), row.RowNumber, digit);
                            return SolverResult.Success(updatedGrid, newCell, $"This is the only cell in this row where {digit} can go.");
                                    
                        default:
                            break;
                    }
                }
            }

            return SolverResult.Failed(grid);
        }
    }
}
