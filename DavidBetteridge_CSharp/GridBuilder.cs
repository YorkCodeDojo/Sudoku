namespace Sudoku
{
    public class GridBuilder
    {
        public static Grid Easy()
        {
            var Squares = new char[9, 9];

            Squares[0, 0] = ' ';
            Squares[1, 0] = ' ';
            Squares[2, 0] = ' ';
            Squares[3, 0] = '7';
            Squares[4, 0] = '5';
            Squares[5, 0] = '2';
            Squares[6, 0] = '8';
            Squares[7, 0] = '1';
            Squares[8, 0] = ' ';

            Squares[0, 1] = ' ';
            Squares[1, 1] = ' ';
            Squares[2, 1] = ' ';
            Squares[3, 1] = ' ';
            Squares[4, 1] = '4';
            Squares[5, 1] = ' ';
            Squares[6, 1] = ' ';
            Squares[7, 1] = ' ';
            Squares[8, 1] = '6';

            Squares[0, 2] = '2';
            Squares[1, 2] = ' ';
            Squares[2, 2] = ' ';
            Squares[3, 2] = ' ';
            Squares[4, 2] = '8';
            Squares[5, 2] = '3';
            Squares[6, 2] = ' ';
            Squares[7, 2] = ' ';
            Squares[8, 2] = '5';

            Squares[0, 3] = ' ';
            Squares[1, 3] = ' ';
            Squares[2, 3] = '9';
            Squares[3, 3] = ' ';
            Squares[4, 3] = ' ';
            Squares[5, 3] = ' ';
            Squares[6, 3] = '2';
            Squares[7, 3] = '8';
            Squares[8, 3] = '3';

            Squares[0, 4] = ' ';
            Squares[1, 4] = '8';
            Squares[2, 4] = ' ';
            Squares[3, 4] = '3';
            Squares[4, 4] = '9';
            Squares[5, 4] = '7';
            Squares[6, 4] = ' ';
            Squares[7, 4] = '4';
            Squares[8, 4] = ' ';

            Squares[0, 5] = '3';
            Squares[1, 5] = '1';
            Squares[2, 5] = '4';
            Squares[3, 5] = ' ';
            Squares[4, 5] = ' ';
            Squares[5, 5] = ' ';
            Squares[6, 5] = '7';
            Squares[7, 5] = ' ';
            Squares[8, 5] = ' ';

            Squares[0, 6] = '1';
            Squares[1, 6] = ' ';
            Squares[2, 6] = ' ';
            Squares[3, 6] = '4';
            Squares[4, 6] = '3';
            Squares[5, 6] = ' ';
            Squares[6, 6] = ' ';
            Squares[7, 6] = ' ';
            Squares[8, 6] = '7';

            Squares[0, 7] = '4';
            Squares[1, 7] = ' ';
            Squares[2, 7] = ' ';
            Squares[3, 7] = ' ';
            Squares[4, 7] = '7';
            Squares[5, 7] = ' ';
            Squares[6, 7] = ' ';
            Squares[7, 7] = ' ';
            Squares[8, 7] = ' ';

            Squares[0, 8] = ' ';
            Squares[1, 8] = '9';
            Squares[2, 8] = '5';
            Squares[3, 8] = '2';
            Squares[4, 8] = '6';
            Squares[5, 8] = '1';
            Squares[6, 8] = ' ';
            Squares[7, 8] = ' ';
            Squares[8, 8] = ' ';

            return new Grid(Squares);
        }
    }
}
