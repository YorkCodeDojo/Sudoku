namespace Sudoku
{
    public class Cell
    {
        public int ColumnNumber { get;}
        public int RowNumber { get;}

        public Cell(int column, int row)
        {
            ColumnNumber = column;
            RowNumber = row;
        }
    }
}