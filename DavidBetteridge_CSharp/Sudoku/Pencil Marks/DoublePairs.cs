namespace Sudoku
{
    class DoublePairs
    {
        public bool Evaluate(Grid grid, PencilMarks pencilMarks)
        {
            var numberRemoved = 0;

            numberRemoved += CheckBlockColumn(grid, grid.Box(0, 0), grid.Box(0, 3), grid.Box(0, 6), pencilMarks);
            numberRemoved += CheckBlockColumn(grid, grid.Box(3, 0), grid.Box(3, 3), grid.Box(3, 6), pencilMarks);
            numberRemoved += CheckBlockColumn(grid, grid.Box(6, 0), grid.Box(6, 3), grid.Box(6, 6), pencilMarks);

            numberRemoved += CheckBlockRow(grid, grid.Box(0, 0), grid.Box(3, 0), grid.Box(6, 0), pencilMarks);
            numberRemoved += CheckBlockRow(grid, grid.Box(0, 3), grid.Box(3, 3), grid.Box(6, 3), pencilMarks);
            numberRemoved += CheckBlockRow(grid, grid.Box(0, 6), grid.Box(3, 6), grid.Box(6, 6), pencilMarks);

            return (numberRemoved > 0);
        }

        private int CheckBlockColumn(Grid grid, Box box1, Box box2, Box box3, PencilMarks pencilMarks)
        {
            var numberRemoved = 0;

            numberRemoved += CheckByColumn(grid, box1, box2, box3, pencilMarks);
            numberRemoved += CheckByColumn(grid, box1, box3, box2, pencilMarks);
            numberRemoved += CheckByColumn(grid, box2, box3, box1, pencilMarks);

            return numberRemoved;
        }

        private int CheckBlockRow(Grid grid, Box box1, Box box2, Box box3, PencilMarks pencilMarks)
        {
            var numberRemoved = 0;

            numberRemoved += CheckByRow(grid, box1, box2, box3, pencilMarks);
            numberRemoved += CheckByRow(grid, box1, box3, box2, pencilMarks);
            numberRemoved += CheckByRow(grid, box2, box3, box1, pencilMarks);

            return numberRemoved;
        }
        private int CheckByRow(Grid grid, Box box1, Box box2, Box target, PencilMarks pencilMarks)
        {
            var numberRemoved = 0;
            foreach (var digit in grid.AllowedDigits)
            {
                if (box1.DoesNotContain(digit) && box2.DoesNotContain(digit) && target.DoesNotContain(digit))
                {
                    var (_, rows1) = PossiblePlacesForDigit(box1, pencilMarks, digit);
                    var (_, rows2) = PossiblePlacesForDigit(box2, pencilMarks, digit);

                    if (rows1[0] > 0 && rows1[1] > 0 && rows1[2] == 0 &&
                        rows2[0] > 0 && rows2[1] > 0 && rows2[2] == 0)
                    {
                        numberRemoved += RemoveFromRow(target, pencilMarks, 0, digit);
                        numberRemoved += RemoveFromRow(target, pencilMarks, 1, digit);
                    }

                    if (rows1[0] > 0 && rows1[1] == 0 && rows1[2] > 0 &&
                        rows2[0] > 0 && rows2[1] == 0 && rows2[2] > 0)
                    {
                        numberRemoved += RemoveFromRow(target, pencilMarks, 0, digit);
                        numberRemoved += RemoveFromRow(target, pencilMarks, 2, digit);
                    }

                    if (rows1[0] == 0 && rows1[1] > 0 && rows1[2] > 0 &&
                        rows2[0] == 0 && rows2[1] > 0 && rows2[2] > 0)
                    {
                        numberRemoved += RemoveFromRow(target, pencilMarks, 1, digit);
                        numberRemoved += RemoveFromRow(target, pencilMarks, 2, digit);
                    }
                }

            }
            return numberRemoved;
        }
        private int CheckByColumn(Grid grid, Box box1, Box box2, Box target, PencilMarks pencilMarks)
        {
            var numberRemoved = 0;
            foreach (var digit in grid.AllowedDigits)
            {
                if (box1.DoesNotContain(digit) && box2.DoesNotContain(digit) && target.DoesNotContain(digit))
                {
                    var (columns1, _) = PossiblePlacesForDigit(box1, pencilMarks, digit);
                    var (columns2, _) = PossiblePlacesForDigit(box2, pencilMarks, digit);

                    if (columns1[0] > 0 && columns1[1] > 0 && columns1[2] == 0 &&
                        columns2[0] > 0 && columns2[1] > 0 && columns2[2] == 0)
                    {
                        numberRemoved += RemoveFromColumn(target, pencilMarks, 0, digit);
                        numberRemoved += RemoveFromColumn(target, pencilMarks, 1, digit);
                    }

                    if (columns1[0] > 0 && columns1[1] == 0 && columns1[2] > 0 &&
                        columns2[0] > 0 && columns2[1] == 0 && columns2[2] > 0)
                    {
                        numberRemoved += RemoveFromColumn(target, pencilMarks, 0, digit);
                        numberRemoved += RemoveFromColumn(target, pencilMarks, 2, digit);
                    }

                    if (columns1[0] == 0 && columns1[1] > 0 && columns1[2] > 0 &&
                        columns2[0] == 0 && columns2[1] > 0 && columns2[2] > 0)
                    {
                        numberRemoved += RemoveFromColumn(target, pencilMarks, 1, digit);
                        numberRemoved += RemoveFromColumn(target, pencilMarks, 2, digit);
                    }
                }

            }
            return numberRemoved;
        }

        private int RemoveFromRow(Box target, PencilMarks pencilMarks, int rowOffset, char digit)
        {
            var numberRemoved = 0;
            for (int columnOffset = 0; columnOffset < 3; columnOffset++)
            {
                numberRemoved += pencilMarks.TryRubOutMark(columnOffset + target.Left,
                                                           rowOffset + target.Top,
                                                           digit);
            }
            return numberRemoved;
        }

        private int RemoveFromColumn(Box target, PencilMarks pencilMarks, int columnOffset, char digit)
        {
            var numberRemoved = 0;
            for (int rowOffset = 0; rowOffset < 3; rowOffset++)
            {
                numberRemoved += pencilMarks.TryRubOutMark(columnOffset + target.Left,
                                                           rowOffset + target.Top,
                                                           digit);
            }
            return numberRemoved;
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
