using System.Collections.Generic;

namespace Sudoku
{
    public class CandidateLines : IPencilMarkReducer
    {
        public List<string> Evaluate(Grid grid, PencilMarks pencilMarks)
        {
            var result = new List<string>();
            foreach (var box in grid.Boxes())
            {
                foreach (var digit in grid.AllowedDigits)
                {
                    if (box.DoesNotContain(digit))
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

                        if (columns[0] > 0 && columns[1] == 0 && columns[2] == 0)
                            RemoveFromVerticallyAdjacentBoxes(box, 0, pencilMarks, digit, result);

                        if (columns[0] == 0 && columns[1] > 0 && columns[2] == 0)
                            RemoveFromVerticallyAdjacentBoxes(box, 1, pencilMarks, digit, result);

                        if (columns[0] == 0 && columns[1] == 0 && columns[2] > 0)
                            RemoveFromVerticallyAdjacentBoxes(box, 2, pencilMarks, digit, result);


                        if (rows[0] > 0 && rows[1] == 0 && rows[2] == 0)
                            RemoveFromHorizontallyAdjacentBoxes(box, 0, pencilMarks, digit, result);

                        if (rows[0] == 0 && rows[1] > 0 && rows[2] == 0)
                            RemoveFromHorizontallyAdjacentBoxes(box, 1, pencilMarks, digit, result);

                        if (rows[0] == 0 && rows[1] == 0 && rows[2] > 0)
                            RemoveFromHorizontallyAdjacentBoxes(box, 2, pencilMarks, digit, result);

                    }
                }
            }

            return result;
        }

        private void RemoveFromVerticallyAdjacentBoxes(Box box, int columnOffset, PencilMarks pencilMarks, char digit, List<string> results)
        {
            var columnNumber = box.Left + columnOffset;

            for (int rowNumber = 0; rowNumber < box.Top; rowNumber++)
                RemoveMark(pencilMarks, digit, results, columnNumber, rowNumber);

            for (int rowNumber = box.Top + 3; rowNumber < 9; rowNumber++)
                RemoveMark(pencilMarks, digit, results, columnNumber, rowNumber);
        }

        private static void RemoveMark(PencilMarks pencilMarks, char digit, List<string> results, int columnNumber, int rowNumber)
        {
            if (pencilMarks.TryRubOutMark(columnNumber, rowNumber, digit) > 0)
            {
                results.Add($"Using CandidateLines removed pencilmark {digit} from {columnNumber},{rowNumber}");
            };
        }

        private void RemoveFromHorizontallyAdjacentBoxes(Box box, int rowOffset, PencilMarks pencilMarks, char digit, List<string> results)
        {
            var rowNumber = box.Top + rowOffset;

            for (int columnNumber = 0; columnNumber < box.Left; columnNumber++)
                RemoveMark(pencilMarks, digit, results, columnNumber, rowNumber);

            for (int columnNumber = box.Left + 3; columnNumber < 9; columnNumber++)
                RemoveMark(pencilMarks, digit, results, columnNumber, rowNumber);

        }
    }
}
