using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class DisjointSubset : IPencilMarkReducer
    {
        public List<string> Evaluate(Grid grid, PencilMarks pencilMarks)
        {
            var result = new List<string>();
            foreach (var row in grid.Rows())
            {
                var marks = row
                            .EmptyColumns()
                            .Select(c => pencilMarks.Marks(c, row.RowNumber))
                            .GroupBy(c => c)
                            .Where(c => c.Key.Count() > 1 && c.Count() == c.Key.Count());

                foreach (var group in marks)
                {
                    var subset = group.Key;
                    foreach (var columnNumber in row.EmptyColumns())
                    {
                        var marksInCell = pencilMarks.Marks(columnNumber, row.RowNumber);
                        if (!marksInCell.SequenceEqual(subset))
                        {
                            foreach (var digit in subset)
                            {
                                if (pencilMarks.TryRubOutMark(columnNumber, row.RowNumber, digit) > 0)
                                {
                                    result.Add($"DisjointSubset (row) rubbed out {digit} from {columnNumber},{row.RowNumber}.  Subset was {{{string.Join(',', subset)}}}");
                                }
                            }
                        }
                    }
                }

            }

            return result;
        }
    }
}
