using System;

namespace Sudoku
{
    public class GridLoader
    {
        public static Grid Load(string text)
        {
            text = text.Replace("-", "")
                       .Replace(" ", "")
                       .Replace("|", "");

            var rows = text.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (rows.Length != 9) throw new Exception($"We need 9 rows not {rows.Length}.");

            var squares = new char[9, 9];
            for (int rowNumber = 0; rowNumber < 9; rowNumber++)
            {
                var row = rows[rowNumber].Trim().Replace("."," ");
                if (row.Length != 9) throw new Exception($"Row {rowNumber} should have 9 columns not {row.Length}");

                for (int columnNumber = 0; columnNumber < 9; columnNumber++)
                {
                    var digit = row.Substring(columnNumber, 1)[0];
                    if (!char.IsDigit(digit) && digit != ' ')
                        throw new Exception($"Cell {columnNumber},{rowNumber} can only contain a numeric digit or blank,  not '{digit}'");
                    squares[columnNumber, rowNumber] = digit;
                }
            }

            return new Grid(squares);
        }

    }
}
