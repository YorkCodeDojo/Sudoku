using System;

namespace Sudoku
{
    //Rules in order
    //Test cases
    //Advanced stuff

    class Program
    {
        static void Main(string[] args)
        {
            var grid = GridBuilder.Easy();
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
                var result = s.Execute(grid);

                if (result.ProgressMade)
                {
                    grid = result.Grid;
                    Console.Out.WriteLine(result.Description);
                    grid.Write(Console.Out, result.Location);
                    Console.Out.WriteLine("");
                    Console.Out.WriteLine("");
                }
            }

        }


    }
}
