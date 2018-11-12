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
            var pencilMarkReducer = new PencilMarkReducer();

            var success = true;
            while (success)
            {
                var pencilMarks = new PencilMarks(grid);
                var reductions = pencilMarkReducer.Reduce(grid, pencilMarks);
                foreach (var reduction in reductions)
                {
                    Console.Out.WriteLine(reduction);
                }

                var result = allSolvers.TryToSolveOneCell(grid, pencilMarks);

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
