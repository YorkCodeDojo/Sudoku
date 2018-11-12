using System;
using System.Linq;

namespace Sudoku
{
    public class OnlyOneDigitIsValidForSquare : ISolver
    {
        public SolverResult TryToSolveOneCell(Grid grid, PencilMarks pencilMarks)
        {
            foreach (var cell in grid.EmptyCells())
            {
                if (pencilMarks.TryGetSinglePencilMark(cell.ColumnNumber, cell.RowNumber, out var digit))
                {
                    var newCell = new Cell(cell.ColumnNumber, cell.RowNumber);
                    var updatedGrid = grid.FillInSquare(cell.ColumnNumber, cell.RowNumber, digit);
                    return SolverResult.Success(updatedGrid, newCell, $"{digit} is the only possible digit which can go in this location.");
                }
            }

                //var possibleDigits = grid.AllowedDigits.Where(digit => grid.Row(cell.RowNumber).DoesNotContain(digit) &&
                //                                                       grid.Column(cell.ColumnNumber).DoesNotContain(digit) &&
                //                                                       grid.Box(cell.ColumnNumber, cell.RowNumber).DoesNotContain(digit));

                //switch (possibleDigits.Count())
                //{
                //    case 0:
                //        throw new Exception("Grid can't be solved");

                //    case 1:
                //        var newCell = new Cell(cell.ColumnNumber, cell.RowNumber);
                //        var updatedGrid = grid.FillInSquare(cell.ColumnNumber, cell.RowNumber, possibleDigits.First());
                //        return SolverResult.Success(updatedGrid, newCell, $"{possibleDigits.First()} is the only possible digit which can go in this location.");

                //    default:
                //        break;
                //}
            //}

            return SolverResult.Failed(grid);
        }
    }
}
