using System.Collections.Generic;
using System.IO;

namespace Sudoku
{
    public class Box
    {
        public int Left { get; }
        public int Top { get; }

        private readonly char[,] _squares = new char[3, 3];

        public Box(int left, int top,
                   char square1, char square2, char square3,
                   char square4, char square5, char square6,
                   char square7, char square8, char square9)
        {
            Left = left;
            Top = top;
            _squares[0, 0] = square1;
            _squares[1, 0] = square2;
            _squares[2, 0] = square3;
            _squares[0, 1] = square4;
            _squares[1, 1] = square5;
            _squares[2, 1] = square6;
            _squares[0, 2] = square7;
            _squares[1, 2] = square8;
            _squares[2, 2] = square9;
        }

        internal void Write(TextWriter textWriter)
        {
            for (int row = 0; row < 3; row++)
                textWriter.WriteLine($"{_squares[0, row]}|{_squares[1, row]}|{_squares[2, row]}");
        }

        internal int CountNumberOf(char digit)
        {
            var count = 0;
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    if (_squares[column, row] == digit) count++;
                }
            }
            return count;
        }

        internal IEnumerable<(int columnNumber, int rowNumber)> EmptyCells()
        {
            var result = new List<(int column, int row)>();

            for (int column = 0; column < 3; column++)
            {
                for (int row = 0; row < 3; row++)
                {
                    if (_squares[column, row] == ' ')
                        result.Add((column + Left, row + Top));
                }
            }
            return result;
        }

        internal bool Contains(char digit) =>
            _squares[0, 0] == digit ||
            _squares[1, 0] == digit ||
            _squares[2, 0] == digit ||
            _squares[0, 1] == digit ||
            _squares[1, 1] == digit ||
            _squares[2, 1] == digit ||
            _squares[0, 2] == digit ||
            _squares[1, 2] == digit ||
            _squares[2, 2] == digit;

        internal bool DoesNotContain(char digit) => !Contains(digit);

        internal bool DoesNotContain(PencilMarks pencilMarks, char digit)
        {

            for (int column = 0; column < 3; column++)
            {
                for (int row = 0; row < 3; row++)
                {
                    if (pencilMarks.HasMark(column + Left, row + Top, digit)) return false;
                }
            }

            return true;
        }
    }
}