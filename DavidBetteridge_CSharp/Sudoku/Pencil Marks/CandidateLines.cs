namespace Sudoku
{
    public class CandidateLines
    {

        public bool Evaluate(Grid grid, PencilMarks pencilMarks)
        {
            var marksRemoved = 0;
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
                            marksRemoved += RemoveFromVerticallyAdjacentBoxes(box, 0, pencilMarks, digit);

                        if (columns[0] == 0 && columns[1] > 0 && columns[2] == 0)
                            marksRemoved += RemoveFromVerticallyAdjacentBoxes(box, 1, pencilMarks, digit);

                        if (columns[0] == 0 && columns[1] == 0 && columns[2] > 0)
                            marksRemoved += RemoveFromVerticallyAdjacentBoxes(box, 2, pencilMarks, digit);


                        if (rows[0] > 0 && rows[1] == 0 && rows[2] == 0)
                            marksRemoved += RemoveFromHorizontallyAdjacentBoxes(box, 0, pencilMarks, digit);

                        if (rows[0] == 0 && rows[1] > 0 && rows[2] == 0)
                            marksRemoved += RemoveFromHorizontallyAdjacentBoxes(box, 1, pencilMarks, digit);

                        if (rows[0] == 0 && rows[1] == 0 && rows[2] > 0)
                            marksRemoved += RemoveFromHorizontallyAdjacentBoxes(box, 2, pencilMarks, digit);

                    }
                }
            }

            return (marksRemoved > 0);
        }

        private int RemoveFromVerticallyAdjacentBoxes(Box box, int columnOffset, PencilMarks pencilMarks, char digit)
        {
            var marksRemoved = 0;
            var columnNumber = box.Left + columnOffset;

            for (int rowNumber = 0; rowNumber < box.Top; rowNumber++)
                marksRemoved += pencilMarks.TryRubOutMark(columnNumber, rowNumber, digit);

            for (int rowNumber = box.Top + 3; rowNumber < 9; rowNumber++)
                marksRemoved += pencilMarks.TryRubOutMark(columnNumber, rowNumber, digit);

            return marksRemoved;
        }

        private int RemoveFromHorizontallyAdjacentBoxes(Box box, int rowOffset, PencilMarks pencilMarks, char digit)
        {
            var marksRemoved = 0;
            var rowNumber = box.Top + rowOffset;

            for (int columnNumber = 0; columnNumber < box.Left; columnNumber++)
                marksRemoved += pencilMarks.TryRubOutMark(columnNumber, rowNumber, digit);

            for (int columnNumber = box.Left + 3; columnNumber < 9; columnNumber++)
                marksRemoved += pencilMarks.TryRubOutMark(columnNumber, rowNumber, digit);

            return marksRemoved;
        }
    }
}
