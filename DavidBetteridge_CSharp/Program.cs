using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new Grid();
            grid.Write(Console.Out);

            Console.Out.WriteLine("");
            Console.Out.WriteLine("");

            var s = new OnlyOneSquareInRowIsValidForDigit();

            var success = true;
            while (success)
            {
                var cell = default(Cell);
                (success, grid, cell) = s.Execute(grid);
                if (success)
                {
                    Display(grid, cell);
                    Console.Out.WriteLine("");
                    Console.Out.WriteLine("");
                }
            }

        }

        internal static void Display(Grid grid, Cell cell)
        {
            for (int row = 0; row < 9; row++)
            {
                if (row == 3 || row == 6)
                    Console.WriteLine("-----------");


                for (int column = 0; column < 9; column++)
                {
                    if (column == 3 || column == 6) Console.Write("|");

                    if (cell.ColumnNumber == column && cell.RowNumber == row)
                        Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write(grid.Square(column, row));

                    Console.ResetColor();
                }
                Console.WriteLine("");
            }
        }
    }
}
