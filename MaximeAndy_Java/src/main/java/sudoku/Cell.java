package sudoku;

import java.util.Arrays;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class Cell {
    private Integer value;
    Set<Integer> options = new HashSet<>(Arrays.asList(1, 2, 3, 4, 5, 6, 7, 8, 9));

    private List<Cell> row = null;
    private List<Cell> column = null;
    private List<Cell> grid = null;

    public Integer getValue() {
        return value;
    }

    public void setValue(Integer value) {
        this.value = value;
        this.row.forEach( cell -> cell.options.remove(value));
        this.column.forEach( cell -> cell.options.remove(value));
        this.grid.forEach( cell -> cell.options.remove(value));
    }

    public boolean isEmpty() {
        return value == null;
    }

    public Set<Integer> getOptions() {
        return options;
    }

    public void setRow(List<Cell> cells) {
        row = cells;
    }
    public void setColumn(List<Cell> cells) {
        column = cells;
    }
    public void setGrid(List<Cell> cells) {
        grid = cells;
    }

    public List<Cell> getRow() {
        return row;
    }

    public List<Cell> getColumn() {
        return column;
    }

    public List<Cell> getGrid() {
        return grid;
    }
}
