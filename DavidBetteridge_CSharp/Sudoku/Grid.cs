using System;
using System.Collections.Generic;

namespace Sudoku
{
    public class Grid
    {
        private const int Size = 9;
        private readonly char[,] Squares = new char[Size, Size];

        public char[] AllowedDigits => new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        internal char Square(int columnNumber, int rowNumber)
        {
            if (rowNumber < 0 || rowNumber >= Size) throw new ArgumentOutOfRangeException(nameof(rowNumber));
            if (columnNumber < 0 || columnNumber >= Size) throw new ArgumentOutOfRangeException(nameof(columnNumber));

            return Squares[columnNumber, rowNumber];
        }

        public Grid(char[,] newGrid)
        {
            Squares = (char[,])newGrid.Clone();
        }

        public Grid FillInSquare(int columnNumber, int rowNumber, char digit)
        {
            if (rowNumber < 0 || rowNumber >= Size) throw new ArgumentOutOfRangeException(nameof(rowNumber));
            if (columnNumber < 0 || columnNumber >= Size) throw new ArgumentOutOfRangeException(nameof(columnNumber));

            var newGrid = (char[,])Squares.Clone();
            if (newGrid[columnNumber, rowNumber] != ' ') throw new Exception($"Square {columnNumber},{rowNumber} is not empty");
            newGrid[columnNumber, rowNumber] = digit;

            return new Grid(newGrid);
        }

        internal IEnumerable<Row> Rows()
        {
            for (int row = 0; row < Size; row++)
            {
                yield return this.Row(row);
            }
        }

        internal IEnumerable<Column> Columns()
        {
            for (int column = 0; column < Size; column++)
            {
                yield return this.Column(column);
            }
        }

        internal IEnumerable<Cell> EmptyCells()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    if (Squares[column, row] == ' ')
                    {
                        yield return new Cell(column, row);
                    }
                }
            }
        }


        internal IEnumerable<Box> Boxes()
        {
            yield return this.Box(0, 0);
            yield return this.Box(3, 0);
            yield return this.Box(6, 0);

            yield return this.Box(0, 3);
            yield return this.Box(3, 3);
            yield return this.Box(6, 3);

            yield return this.Box(0, 6);
            yield return this.Box(3, 6);
            yield return this.Box(6, 6);
        }

        public Box Box(int columnNumber, int rowNumber)
        {
            if (rowNumber < 0 || rowNumber >= Size) throw new ArgumentOutOfRangeException(nameof(rowNumber));
            if (columnNumber < 0 || columnNumber >= Size) throw new ArgumentOutOfRangeException(nameof(columnNumber));

            int cellToFirstInBox(int cellNumber)
            {
                switch (cellNumber)
                {
                    case 0:
                    case 1:
                    case 2:
                        return 0;
                    case 3:
                    case 4:
                    case 5:
                        return 3;
                    default:
                        return 6;
                };
            }

            var firstColumn = cellToFirstInBox(columnNumber);
            var firstRow = cellToFirstInBox(rowNumber);

            return new Box
            (
                firstColumn,
                firstRow,
                Squares[firstColumn, firstRow],
                Squares[firstColumn + 1, firstRow],
                Squares[firstColumn + 2, firstRow],
                Squares[firstColumn, firstRow + 1],
                Squares[firstColumn + 1, firstRow + 1],
                Squares[firstColumn + 2, firstRow + 1],
                Squares[firstColumn, firstRow + 2],
                Squares[firstColumn + 1, firstRow + 2],
                Squares[firstColumn + 2, firstRow + 2]
            );
        }

        public Row Row(int rowNumber)
        {
            if (rowNumber < 0 || rowNumber >= Size) throw new ArgumentOutOfRangeException(nameof(rowNumber));

            return new Row
            (
                rowNumber,
                Squares[0, rowNumber],
                Squares[1, rowNumber],
                Squares[2, rowNumber],
                Squares[3, rowNumber],
                Squares[4, rowNumber],
                Squares[5, rowNumber],
                Squares[6, rowNumber],
                Squares[7, rowNumber],
                Squares[8, rowNumber]
            );
        }

        public Column Column(int columnNumber)
        {
            if (columnNumber < 0 || columnNumber >= Size) throw new ArgumentOutOfRangeException(nameof(columnNumber));

            return new Column
            (
                columnNumber,
                Squares[columnNumber, 0],
                Squares[columnNumber, 1],
                Squares[columnNumber, 2],
                Squares[columnNumber, 3],
                Squares[columnNumber, 4],
                Squares[columnNumber, 5],
                Squares[columnNumber, 6],
                Squares[columnNumber, 7],
                Squares[columnNumber, 8]
            );
        }
    }
}
