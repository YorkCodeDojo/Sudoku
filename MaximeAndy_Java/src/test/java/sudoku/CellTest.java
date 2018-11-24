package sudoku;

import org.junit.jupiter.api.Test;

import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

class CellTest {

    @Test
    public void givenEmptyCell_shouldReturnIsEmptyCellTrue(){
        final Cell cell = new Cell();
        cell.setValue(null);
        assertTrue(cell.isEmpty());
    }

    @Test
    public void givenEmptyCell_shouldHaveNineOptions(){
        final Cell cell = new Cell();
        cell.setValue(null);
        assertEquals(9, cell.getOptions().size());
    }

    @Test
    public void givenFullBoard_shouldGroupsBePopulated(){
        final Board board = new Board(
                null,2,3,4,5,6,7,8,9,
                1,2,3,4,5,6,7,8,9,
                1,2,3,4,5,6,7,8,9,
                1,2,3,4,5,6,7,8,9,
                1,2,3,4,5,6,7,8,9,
                1,2,3,4,5,6,7,8,9,
                1,2,3,4,5,6,7,8,9,
                1,2,3,4,5,6,7,8,9,
                1,2,3,4,5,6,7,8,9);
        Cell cell = board.getNextEmptyCell();
        List<Cell> row = cell.getRow();
        assertNotNull(row);
        assertEquals(9, row.size());
        List<Cell> column = cell.getColumn();
        assertNotNull(column);
        assertEquals(9, column.size());
        List<Cell> grid = cell.getGrid();
        assertNotNull(grid);
        assertEquals(9, grid.size());

    }


}