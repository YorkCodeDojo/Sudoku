package sudoku;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class BoardTest {

    @Test
    public void givenEmptyBoard_shouldReturnIsCompleteFalse(){
        final Board board = new Board();
        assertFalse(board.isComplete());

        assertEquals(27, board.groups.size() );

    }

    @Test
    public void givenCompletedBoard_shouldReturnIsCompleteTrue(){
        final Board board = new Board(
                1,2,3,4,5,6,7,8,9,
                1,2,3,4,5,6,7,8,9,
                1,2,3,4,5,6,7,8,9,
                1,2,3,4,5,6,7,8,9,
                1,2,3,4,5,6,7,8,9,
                1,2,3,4,5,6,7,8,9,
                1,2,3,4,5,6,7,8,9,
                1,2,3,4,5,6,7,8,9,
                1,2,3,4,5,6,7,8,9);
        assertTrue(board.isComplete());
    }

    @Test
    public void givenAlmostCompletedBoard_shouldReturnIsCompleteFalse(){
        final Board board = new Board(
                1,8,9,5,3,4,2,7,6,
                4,3,2,1,7,6,5,9,8,
                7,5,6,2,9,8,1,4,3,
                6,4,3,9,2,7,8,1,5,
                9,2,5,8,null,1,6,3,7,    // Missing value should be 4
                8,7,1,3,6,5,4,2,9,
                5,9,8,4,1,3,7,6,2,
                3,6,4,7,8,2,9,5,1,
                2,1,7,6,5,9,3,8,4);
        assertFalse(board.isComplete());
        Cell cell = board.getNextEmptyCell();
        assertNotNull(cell);
        assertEquals(1, cell.options.size());
        assertTrue(cell.options.contains(4));
    }

    @Test
    public void givenAlmostCompletedBoard_WhenLastValuedIsFilled_shouldReturnIsCompleteTrue(){
        final Board board = new Board(
                1,8,9,5,3,4,2,7,6,
                4,3,2,1,7,6,5,9,8,
                7,5,6,2,9,8,1,4,3,
                6,4,3,9,2,7,8,1,5,
                9,2,5,8,null,1,6,3,7,    // Missing value should be 4
                8,7,1,3,6,5,4,2,9,
                5,9,8,4,1,3,7,6,2,
                3,6,4,7,8,2,9,5,1,
                2,1,7,6,5,9,3,8,4);
        assertFalse(board.isComplete());
        Cell cell = board.getNextEmptyCell();
        cell.setValue(4);
        assertTrue(board.isComplete());
    }

    @Test
    public void givenNotCompletedBoard_shouldReturnNextEmptyCell(){
        final Board board = new Board();
        Cell cell = board.getNextEmptyCell();
        assertNotNull(cell);
    }

    @Test
    public void givenNewValueInACell_shouldRemoveTheValueFromOptionOfOtherCellInGroups(){
        Board board = new Board();
        Cell cell = board.getNextEmptyCell();
        cell.setValue(1);

        assertNotInGroups(1, cell);
    }

    @Test
    public void givenAlmostFullBoard_shouldSolveIt(){
        final Board board = new Board(
                null,null,9,5,3,4,2,7,6,
                4,3,2,1,7,6,5,9,8,
                7,5,6,2,9,8,1,4,3,
                6,4,3,9,2,7,8,1,5,
                9,2,5,8,4,1,6,3,7,    // Missing value should be 4
                8,7,1,3,6,5,4,2,9,
                5,9,8,4,1,3,7,6,2,
                3,6,4,7,8,2,9,5,1,
                2,1,7,6,5,9,3,8,4);

        board.solve();
        assertTrue(board.isComplete());
        assertEquals(new Integer(1),board.cells.get(0).getValue());
        assertEquals(new Integer(8),board.cells.get(1).getValue());

    }

    private void assertNotInGroups(int valueSet, Cell cell) {
        for(Cell cellInGroup : cell.getRow()){
            assertFalse(cellInGroup.getOptions().contains(valueSet));
        }
        for(Cell cellInGroup : cell.getColumn()){
            assertFalse(cellInGroup.getOptions().contains(valueSet));
        }
        for(Cell cellInGroup : cell.getGrid()){
            assertFalse(cellInGroup.getOptions().contains(valueSet));
        }
    }

}