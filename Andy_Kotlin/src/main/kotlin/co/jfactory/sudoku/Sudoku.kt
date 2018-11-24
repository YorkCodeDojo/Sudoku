package co.jfactory.sudoku

sealed class Cell {
    open val cardinality = Int.MAX_VALUE
}

class OpenCell(val options: List<Int> = (1..9).toList()) : Cell() {
    val impossible = options.isEmpty()
    override val cardinality = options.size
    override fun toString() = "◦"
    operator fun minus(value: Int) = OpenCell(this.options.filterNot { it == value })
}

class FixedCell(val value: Int) : Cell() {
    override fun toString() = "$value"
}

typealias Grid = List<Cell>

/**
 * Solve the Sudoko
 * Recursively solve the puzzle by filling in the best cell at a time.
 */
fun solve(grid: Grid) :Grid? = when {
    grid.solved() -> grid
    !grid.possible() -> null
    else -> childGrids(grid).mapNotNull { solve(it) }.firstOrNull()
}

/** Solved when all cells have been converted to Fixed Cells**/
fun Grid.solved(): Boolean = this.all { it is FixedCell }

/** Impossible if there are any Open Cells without any remaining options **/
fun Grid.possible() = this.none { it is OpenCell && it.impossible }

fun Grid.asString(): String {
    val lines = this.chunked(9)
    val lineBreaks = listOf(2, 5)
    val sb = StringBuilder()
    lines.forEachIndexed { lineNo, cells ->
        sb.append(cells.chunked(3).joinToString(separator = "|", postfix = "\n") {
            it.joinToString(separator = " ", prefix = " ", postfix = " ")
        })
        if (lineBreaks.contains(lineNo)) {
            sb.append("-------+-------+------\n")
        }
    }
    return sb.toString()
}

fun toIndex(x: Int, y: Int) = y * 9 + x
fun Int.toPosition(): Pair<Int, Int> = Pair(this.rem(9), this.div(9))
fun toSubGrid(x:Int, y:Int)= x.div(3) + (3 * y.div(3))

/**
 * Check if the Position is in the same Row, Column or Sub-Grid as the supplied x & y value
 */
fun Int.alignsWith(x: Int, y: Int): Boolean {
    val (x2, y2) = this.toPosition()
    return (x == x2) // Same Column
            || (y == y2)  // Same Row
            || toSubGrid(x2,y2) == toSubGrid(x,y) // Same small grid
}

/**
 * Select the OpenCell with the fewest options and generate a child grid for each optional value.
 * This should be the quickest way to locate a solution
 */
fun childGrids(grid: Grid): List<Grid> {
    val (index, targetcell) = grid
            .mapIndexed { index, cell -> index to cell }
            .sortedBy { it.second.cardinality }
            .first { it.second is OpenCell }
    return when (targetcell) {
        is OpenCell -> targetcell.options.map { grid.setCellByIndex(index, it) }
        else -> emptyList()
    }
}

/**
 * Set one of the Cells in the Grid to a specific value
 *
 * This will convert an OpenCell to a Fixed Cell and remove the value from the OpenCells with
 * align with this cell (i.e. same row, column or sub-grid
 */
fun Grid.setCell(x: Int, y: Int, value: Int): Grid {
    val cells = this.toMutableList()
    cells[toIndex(x, y)] = FixedCell(value)
    return cells.mapIndexed { index, cell ->
        when (cell) {
            is OpenCell -> when ((value in cell.options) && index.alignsWith(x, y)) {
                true -> cell - value
                else -> cell
            }
            is FixedCell -> cell
        }
    }
}

fun Grid.setCellByIndex(index: Int, value: Int): Grid {
    val (x, y) = index.toPosition()
    return this.setCell(x, y, value)
}

/**
 * Generate a Grid from a List of supplied integers
 * The order of the values in the list corresponds to filling the grid from the top, row by row
 * Start with 81 Open Cells and then apply each of the known numbers into the Grid to generate FixedCells and
 * remove options from the remaining OpenCells
 */
fun List<Int>.toGrid(): Grid {
    val grid: List<Cell> = (0..80).map { OpenCell() }
    return this.foldIndexed(grid) { index, acc, i -> if (i in (1..9)) acc.setCellByIndex(index, i) else acc }
}

fun CharSequence.toGrid() = this.map { if (it.isDigit()) it.toInt() - '0'.toInt() else 0 }.toGrid()

