using System;
using System.Linq;

namespace Sudoku
{
    public class OnlyOneSquareInBoxIsValidForDigit : ISolver
    {
        public SolverResult Execute(Grid grid)
        {
            foreach (var box in grid.Boxes())
            {
                var digitsToFillIn = grid.AllowedDigits
                                         .Where(digit => box.DoesNotContain(digit));

                foreach (var digit in digitsToFillIn)
                {
                    var possibleCells = box.EmptyCells()
                                           .Where(location => grid.Column(location.columnNumber).DoesNotContain(digit) &&
                                                              grid.Row(location.rowNumber).DoesNotContain(digit));

                    switch (possibleCells.Count())
                    {
                        case 0:
                            throw new Exception("Grid can't be solved");

                        case 1:
                            var (columnNumber, rowNumber) = possibleCells.First();
                            var newCell = new Cell(columnNumber, rowNumber);
                            var updatedGrid = grid.FillInSquare(columnNumber, rowNumber, digit);
                            return SolverResult.Success(updatedGrid, newCell, $"This is the only cell in this box where {digit} can go.");

                        default:
                            break;
                    }
                }
            }

            return SolverResult.Failed(grid);
        }

    }
}
