namespace Sudoku
{
    public static class GridChecker
    {
        public static string Validate(this Grid grid)
        {

            foreach (var row in grid.Rows())
            {
                foreach (var digit in grid.AllowedDigits)
                {
                    if (row.DoesNotContain(digit)) return $"Row {row.RowNumber} does not contain a {digit}";
                }
            }

            foreach (var column in grid.Columns())
            {
                foreach (var digit in grid.AllowedDigits)
                {
                    if (column.DoesNotContain(digit)) return $"Column {column.ColumnNumber} does not contain a {digit}";
                }
            }

            foreach (var box in grid.Boxes())
            {
                foreach (var digit in grid.AllowedDigits)
                {
                    if (box.DoesNotContain(digit)) return $"Box {box.Left},{box.Top} -> {box.Left + 2},{box.Top + 2} does not contain a {digit}";
                }
            }

            return "";
        }
    }
}
