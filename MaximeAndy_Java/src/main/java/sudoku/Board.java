package sudoku;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;

public class Board {

    protected final List<List<Cell>> groups = new ArrayList<>(27);
    protected final List<Cell> cells;

    public Board() {
        for (int i = 0; i < 27; i++) {
            groups.add(new ArrayList<>());
        }

        cells = new ArrayList<>();
        for (int i = 0; i < 81; i++) {
            Cell cell = new Cell();
            cells.add(cell);

            int xPos = Math.floorDiv(i,9);
            int yPos = i % 9;
            int gridNo = Math.floorDiv(xPos, 3) + (3 * Math.floorDiv(yPos, 3));
            groups.get(xPos).add(cell); // Row
            groups.get(9 + yPos).add(cell); // Column
            groups.get(18 + gridNo).add(cell); // Sub Grid

            cell.setRow(groups.get(xPos));
            cell.setColumn(groups.get(9 + yPos));
            cell.setGrid(groups.get(18 + gridNo));
        }
    }

    public Board(Integer ... values) {
        this();
        for (int i = 0 ; i< values.length ; i++) {
            if (values[i] != null){
                setCellValue(i, values[i]);
            }
        }
    }

    private void setCellValue(int index, int value){
        cells.get(index).setValue(value);
    }

    public boolean isComplete() {
        return cells.stream().noneMatch(c -> c.isEmpty());
    }

    public Cell getNextEmptyCell() {
        return cells.stream().filter(c -> c.isEmpty()).findFirst().get();
    }

    public Cell getBestCell(){
        return cells.stream().filter(c-> c.options.size() > 0).sorted(Comparator.comparingInt(c -> c.getOptions().size())).findFirst().get();
    }

    public void solve() {
        while(!isComplete()){
            Cell cell = getBestCell();
            cell.setValue(cell.options.iterator().next());
        }
    }
}
