using System;
using System.IO;

namespace Sudoku
{
    public static class GridPrinter
    {
        internal static void Write(this Grid grid, TextWriter textWriter)
        {
            Write(grid, textWriter, null);
        }

        internal static void Write(this Grid grid, TextWriter textWriter, Cell lastMove)
        {
            var writingToConsole = (textWriter == Console.Out);

            for (int row = 0; row < 9; row++)
            {
                if (row == 3 || row == 6)
                    textWriter.WriteLine("-----------");

                for (int column = 0; column < 9; column++)
                {
                    if (column == 3 || column == 6) Console.Write("|");

                    if (writingToConsole && lastMove != null && lastMove.ColumnNumber == column && lastMove.RowNumber == row)
                        Console.ForegroundColor = ConsoleColor.Red;

                    textWriter.Write(grid.Square(column, row));

                    if (writingToConsole)
                        Console.ResetColor();
                }
                textWriter.WriteLine("");
            }
        }
    }
}
