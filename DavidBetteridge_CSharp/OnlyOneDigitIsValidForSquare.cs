using System;
using System.Linq;

namespace Sudoku
{
    public class OnlyOneDigitIsValidForSquare : ISolver
    {
        public (bool success, Grid grid, Cell cell) Execute(Grid grid)
        {
            foreach (var cell in grid.EmptyCells())
            {
                var possibleDigits = grid.AllowedDigits.Where(digit => !grid.Row(cell.RowNumber).Contains(digit) &&
                                                                       !grid.Column(cell.ColumnNumber).Contains(digit) &&
                                                                       !grid.Box(cell.ColumnNumber, cell.RowNumber).Contains(digit));

                switch (possibleDigits.Count())
                {
                    case 0:
                        throw new Exception("Grid can't be solved");
                    case 1:
                        return (true, 
                                grid.FillInSquare(cell.ColumnNumber, cell.RowNumber, possibleDigits.First()),
                                new Cell(cell.ColumnNumber, cell.RowNumber));
                    default:
                        break;
                }

            }

            return (false, grid, null);
        }
    }
}
