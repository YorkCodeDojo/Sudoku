namespace Sudoku
{
    public class SolverResult
    {
        public bool ProgressMade { get; }
        public Grid Grid { get; }
        public Cell Location { get; }
        public string Description { get; }

        private SolverResult(bool progressMade, Grid grid, Cell location, string description)
        {
            ProgressMade = progressMade;
            Grid = grid;
            Location = location;
            Description = description;
        }

        public static SolverResult Success(Grid grid, Cell location, string description) => new SolverResult(true, grid, location, description);
        public static SolverResult Failed(Grid grid) => new SolverResult(false, grid, null, null);



    }
}