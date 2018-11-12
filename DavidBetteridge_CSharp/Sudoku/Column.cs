using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class Column
    {
        public int ColumnNumber { get; }

        private readonly char[] _rows = new char[9];
        public Column(int columnNumber, char row0, char row1, char row2, char row3, char row4, char row5, char row6, char row7, char row8)
        {
            ColumnNumber = columnNumber;
            _rows[0] = row0;
            _rows[1] = row1;
            _rows[2] = row2;
            _rows[3] = row3;
            _rows[4] = row4;
            _rows[5] = row5;
            _rows[6] = row6;
            _rows[7] = row7;
            _rows[8] = row8;
        }

        internal IEnumerable<int> EmptyRows()
        {
            return _rows.Select((digit, index) => (digit, index))
                           .Where(digitAndIndex => digitAndIndex.digit == ' ')
                           .Select(digitAndIndex => digitAndIndex.index);
        }

        internal bool Contains(char digit) => _rows.Any(d => d == digit);
        internal bool DoesNotContain(char digit) => _rows.All(d => d != digit);
        internal bool DoesNotContain(PencilMarks pencilMarks, char digit)
        {
            for (int row = 0; row < 9; row++)
            {
                if (pencilMarks.HasMark(ColumnNumber, row, digit)) return false;
            }
            return true;
        }
    }
}