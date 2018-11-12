using System;
using System.Linq;

namespace Sudoku
{
    public class OnlyOneSquareInColumnIsValidForDigit : ISolver
    {
        public SolverResult TryToSolveOneCell(Grid grid, PencilMarks pencilMarks)
        {
            foreach (var column in grid.Columns())
            {
                var digitsToFillIn = grid.AllowedDigits
                                         .Where(digit => column.DoesNotContain(digit));

                foreach (var digit in digitsToFillIn)
                {
                    var possibleRows = column.EmptyRows()
                                             .Where(rowNumber => pencilMarks.HasMark(column.ColumnNumber, rowNumber, digit));

                    switch (possibleRows.Count())
                    {
                        case 0:
                            throw new Exception("Grid can't be solved");

                        case 1:
                            var newCell = new Cell(column.ColumnNumber, possibleRows.First());
                            var updatedGrid = grid.FillInSquare(column.ColumnNumber, possibleRows.First(), digit);
                            return SolverResult.Success(updatedGrid, newCell, $"This is the only cell in this column where {digit} can go.");

                        default:
                            break;
                    }
                }
            }

            return SolverResult.Failed(grid);

        }

    }
}
