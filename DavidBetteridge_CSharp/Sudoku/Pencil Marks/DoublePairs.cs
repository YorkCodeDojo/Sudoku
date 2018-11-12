using System.Collections.Generic;

namespace Sudoku
{
    class DoublePairs : IPencilMarkReducer
    {
        public List<string> Evaluate(Grid grid, PencilMarks pencilMarks)
        {
            var results = new List<string>();

            CheckBlockColumn(results, grid, grid.Box(0, 0), grid.Box(0, 3), grid.Box(0, 6), pencilMarks);
            CheckBlockColumn(results, grid, grid.Box(3, 0), grid.Box(3, 3), grid.Box(3, 6), pencilMarks);
            CheckBlockColumn(results, grid, grid.Box(6, 0), grid.Box(6, 3), grid.Box(6, 6), pencilMarks);

            CheckBlockRow(results, grid, grid.Box(0, 0), grid.Box(3, 0), grid.Box(6, 0), pencilMarks);
            CheckBlockRow(results, grid, grid.Box(0, 3), grid.Box(3, 3), grid.Box(6, 3), pencilMarks);
            CheckBlockRow(results, grid, grid.Box(0, 6), grid.Box(3, 6), grid.Box(6, 6), pencilMarks);

            return results;
        }

        private void CheckBlockColumn(List<string> results, Grid grid, Box box1, Box box2, Box box3, PencilMarks pencilMarks)
        {
            CheckByColumn(results, grid, box1, box2, box3, pencilMarks);
            CheckByColumn(results, grid, box1, box3, box2, pencilMarks);
            CheckByColumn(results, grid, box2, box3, box1, pencilMarks);
        }

        private void CheckBlockRow(List<string> results, Grid grid, Box box1, Box box2, Box box3, PencilMarks pencilMarks)
        {
            CheckByRow(results, grid, box1, box2, box3, pencilMarks);
            CheckByRow(results, grid, box1, box3, box2, pencilMarks);
            CheckByRow(results, grid, box2, box3, box1, pencilMarks);
        }
        private void CheckByRow(List<string> results, Grid grid, Box box1, Box box2, Box target, PencilMarks pencilMarks)
        {
            foreach (var digit in grid.AllowedDigits)
            {
                if (box1.DoesNotContain(digit) && box2.DoesNotContain(digit) && target.DoesNotContain(digit))
                {
                    var (_, rows1) = PossiblePlacesForDigit(box1, pencilMarks, digit);
                    var (_, rows2) = PossiblePlacesForDigit(box2, pencilMarks, digit);

                    if (rows1[0] > 0 && rows1[1] > 0 && rows1[2] == 0 &&
                        rows2[0] > 0 && rows2[1] > 0 && rows2[2] == 0)
                    {
                        RemoveFromRow(results, target, pencilMarks, 0, digit);
                        RemoveFromRow(results, target, pencilMarks, 1, digit);
                    }

                    if (rows1[0] > 0 && rows1[1] == 0 && rows1[2] > 0 &&
                        rows2[0] > 0 && rows2[1] == 0 && rows2[2] > 0)
                    {
                        RemoveFromRow(results, target, pencilMarks, 0, digit);
                        RemoveFromRow(results, target, pencilMarks, 2, digit);
                    }

                    if (rows1[0] == 0 && rows1[1] > 0 && rows1[2] > 0 &&
                        rows2[0] == 0 && rows2[1] > 0 && rows2[2] > 0)
                    {
                        RemoveFromRow(results, target, pencilMarks, 1, digit);
                        RemoveFromRow(results, target, pencilMarks, 2, digit);
                    }
                }

            }
        }
        private void CheckByColumn(List<string> results, Grid grid, Box box1, Box box2, Box target, PencilMarks pencilMarks)
        {
            foreach (var digit in grid.AllowedDigits)
            {
                if (box1.DoesNotContain(digit) && box2.DoesNotContain(digit) && target.DoesNotContain(digit))
                {
                    var (columns1, _) = PossiblePlacesForDigit(box1, pencilMarks, digit);
                    var (columns2, _) = PossiblePlacesForDigit(box2, pencilMarks, digit);

                    if (columns1[0] > 0 && columns1[1] > 0 && columns1[2] == 0 &&
                        columns2[0] > 0 && columns2[1] > 0 && columns2[2] == 0)
                    {
                        RemoveFromColumn(results, target, pencilMarks, 0, digit);
                        RemoveFromColumn(results, target, pencilMarks, 1, digit);
                    }

                    if (columns1[0] > 0 && columns1[1] == 0 && columns1[2] > 0 &&
                        columns2[0] > 0 && columns2[1] == 0 && columns2[2] > 0)
                    {
                        RemoveFromColumn(results, target, pencilMarks, 0, digit);
                        RemoveFromColumn(results, target, pencilMarks, 2, digit);
                    }

                    if (columns1[0] == 0 && columns1[1] > 0 && columns1[2] > 0 &&
                        columns2[0] == 0 && columns2[1] > 0 && columns2[2] > 0)
                    {
                        RemoveFromColumn(results, target, pencilMarks, 1, digit);
                        RemoveFromColumn(results, target, pencilMarks, 2, digit);
                    }
                }

            }
        }

        private void RemoveFromRow(List<string> results, Box target, PencilMarks pencilMarks, int rowOffset, char digit)
        {
            for (int columnOffset = 0; columnOffset < 3; columnOffset++)
            {
                var columnNumber = columnOffset + target.Left;
                var rowNumber = rowOffset + target.Top;
                if (pencilMarks.TryRubOutMark(columnNumber, rowNumber, digit) > 0)
                    results.Add($"Using DoublePairs removed pencilmark{digit} from {columnNumber},{rowNumber}");
            }
        }

        private void RemoveFromColumn(List<string> results, Box target, PencilMarks pencilMarks, int columnOffset, char digit)
        {
            for (int rowOffset = 0; rowOffset < 3; rowOffset++)
            {
                var columnNumber = columnOffset + target.Left;
                var rowNumber = rowOffset + target.Top;
                if (pencilMarks.TryRubOutMark(columnNumber, rowNumber, digit) > 0)
                    results.Add($"Using DoublePairs removed pencilmark{digit} from {columnNumber},{rowNumber}");
            }
        }

        private static (int[] columns, int[] rows) PossiblePlacesForDigit(Box box, PencilMarks pencilMarks, char digit)
        {
            var columns = new int[3];
            var rows = new int[3];

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (pencilMarks.HasMark(box.Left + x, box.Top + y, digit))
                    {
                        columns[x]++;
                        rows[y]++;
                    }
                }
            }

            return (columns, rows);
        }
    }
}
