using System.IO;

namespace Sudoku
{
    public class Box
    {
        private readonly char _square1;
        private readonly char _square2;
        private readonly char _square3;
        private readonly char _square4;
        private readonly char _square5;
        private readonly char _square6;
        private readonly char _square7;
        private readonly char _square8;
        private readonly char _square9;

        public Box(char square1, char square2, char square3, char square4, char square5, char square6, char square7, char square8, char square9)
        {
            _square1 = square1;
            _square2 = square2;
            _square3 = square3;
            _square4 = square4;
            _square5 = square5;
            _square6 = square6;
            _square7 = square7;
            _square8 = square8;
            _square9 = square9;
        }

        internal void Write(TextWriter textWriter)
        {
            textWriter.WriteLine($"{_square1}|{_square2}|{_square3}");
            textWriter.WriteLine($"{_square4}|{_square4}|{_square6}");
            textWriter.WriteLine($"{_square7}|{_square8}|{_square9}");
        }

        internal bool Contains(char digit) =>
            _square1 == digit ||
            _square2 == digit ||
            _square3 == digit ||
            _square4 == digit ||
            _square5 == digit ||
            _square6 == digit ||
            _square7 == digit ||
            _square8 == digit ||
            _square9 == digit;

    }
}