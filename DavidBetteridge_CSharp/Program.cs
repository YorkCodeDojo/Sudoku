using System;

namespace Sudoku
{
    //Only in block
    //Display reason
    //Rules in order
    //Test cases
    //Advanced stuff
    //Move grid builder

    class Program
    {
        static void Main(string[] args)
        {
            var grid = new Grid();
            grid.Write(Console.Out);

            Console.Out.WriteLine("");
            Console.Out.WriteLine("");

            //var s = new OnlyOneSquareInRowIsValidForDigit();
           // var s = new OnlyOneDigitIsValidForSquare();
            //var s = new OnlyOneSquareInColumnIsValidForDigit();
            var s = new OnlyOneSquareInBoxIsValidForDigit();

            var success = true;
            while (success)
            {
                var cell = default(Cell);
                (success, grid, cell) = s.Execute(grid);
                if (success)
                {
                    grid.Write(Console.Out, cell);
                    Console.Out.WriteLine("");
                    Console.Out.WriteLine("");
                }
            }

        }


    }
}
