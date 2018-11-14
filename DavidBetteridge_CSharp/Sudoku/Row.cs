using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class Row
    {
        public int RowNumber { get; }

        private readonly char[] _columns = new char[9];
        public Row(int rowNumber, char column0, char column1, char column2, char column3, char column4, char column5, char column6, char column7, char column8)
        {
            RowNumber = rowNumber;
            _columns[0] = column0;
            _columns[1] = column1;
            _columns[2] = column2;
            _columns[3] = column3;
            _columns[4] = column4;
            _columns[5] = column5;
            _columns[6] = column6;
            _columns[7] = column7;
            _columns[8] = column8;
        }

        internal int CountNumberOf(char digit) => _columns.Count(d => d == digit);

        internal IEnumerable<int> EmptyColumns()
        {
            return _columns.Select((digit, index) => (digit, index))
                           .Where(digitAndIndex => digitAndIndex.digit == ' ')
                           .Select(digitAndIndex => digitAndIndex.index);
        }

        internal bool Contains(char digit) => _columns.Any(d => d == digit);

        internal bool DoesNotContain(char digit) => _columns.All(d => d != digit);

        internal bool DoesNotContain(PencilMarks pencilMarks, char digit)
        {
            for (int columnNumber = 0; columnNumber < 9; columnNumber++)
            {
                if (pencilMarks.HasMark(columnNumber, RowNumber, digit)) return false;
            }
            return true;
        }
    }
}