using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Sudoku
{
    public class PencilMarks
    {
        private readonly List<char>[,] marks = new List<char>[9, 9];

        public PencilMarks(Grid grid)
        {
            foreach (var item in grid.EmptyCells())
            {
                marks[item.ColumnNumber, item.RowNumber] = new List<char>();
                foreach (var digit in grid.AllowedDigits)
                {
                    if (grid.Column(item.ColumnNumber).DoesNotContain(digit) &&
                        grid.Row(item.RowNumber).DoesNotContain(digit) &&
                        grid.Box(item.ColumnNumber, item.RowNumber).DoesNotContain(digit))
                    {
                        marks[item.ColumnNumber, item.RowNumber].Add(digit);
                    }
                }
            }
        }

        internal bool TryGetSinglePencilMark(int columnNumber, int rowNumber, out char digit)
        {
            var marksForCell = marks[columnNumber, rowNumber];
            if (marksForCell == null || marksForCell.Count() != 1)
                digit = ' ';
            else
                digit = marksForCell[0];

            return (digit != ' ');
        }

        internal ImmutableArray<char> Marks(int columnNumber, int rowNumber)
        {
            var marksForCell = marks[columnNumber, rowNumber];
            if (marksForCell == null)
                return ImmutableArray<char>.Empty;
            else
                return marksForCell.ToImmutableArray();
        }

        internal bool HasMark(int columnNumber, int rowNumber, char digit)
        {
            var marksForCell = marks[columnNumber, rowNumber];
            return (marksForCell != null && marksForCell.Contains(digit));
        }

        public (int columnNumber, int rowNumber, char digit) TryFindSinglePencilMark()
        {
            for (int columnNumber = 0; columnNumber < 9; columnNumber++)
            {
                for (int rowNumber = 0; rowNumber < 9; rowNumber++)
                {
                    var marksForCell = marks[columnNumber, rowNumber];
                    if (marksForCell != null && marksForCell.Count == 1)
                        return (columnNumber, rowNumber, marksForCell.First());
                }
            }

            return (-1, -1, ' ');
        }

        internal int TryRubOutMark(int columnNumber, int rowNumber, char digit)
        {
            var marksForCell = marks[columnNumber, rowNumber];
            if (marksForCell == null || !marksForCell.Contains(digit))
            {
                return 0;
            }
            else
            {
                marksForCell.Remove(digit);
                return 1;
            }
        }

        public void RubOutMark(int columnNumber, int rowNumber, char digit)
        {
            var marksForCell = marks[columnNumber, rowNumber];
            if (marksForCell == null)
                throw new System.Exception($"There are no pencil marks for cell {columnNumber},{rowNumber} so {digit} cannot be removed.");

            if (!marksForCell.Contains(digit))
                throw new System.Exception($"The cell {columnNumber},{rowNumber} does not contain the pencil mark {digit} so it cannot be removed.");

            marksForCell.Remove(digit);
        }
    }
}
