using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = GridBuilder.Easy();
            grid.Write(Console.Out);

            Console.Out.WriteLine("");
            Console.Out.WriteLine("");


            var allSolvers = new AllSolvers();

            var success = true;
            while (success)
            {
                var result = allSolvers.TryToSolveOneCell(grid);

                if (result.ProgressMade)
                {
                    grid = result.Grid;
                    Console.Out.WriteLine(result.Description);
                    grid.Write(Console.Out, result.Location);
                    Console.Out.WriteLine("");
                    Console.Out.WriteLine("");
                }
                success = result.ProgressMade;
            }

        }

    }
}
