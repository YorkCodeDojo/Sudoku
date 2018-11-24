package co.jfactory.sudoku

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments
import org.junit.jupiter.params.provider.MethodSource


internal class CellTest {

    @ParameterizedTest
    @MethodSource("aligmentParameters")
    fun alignmentTest(position: Int, x: Int, y: Int, expected: Boolean){
        assertEquals( expected, position.alignsWith(x,y))
    }

    companion object {
        @Suppress("unused")
        @JvmStatic
        fun aligmentParameters() = listOf(
                Arguments.of(0, 1, 1, true),
                Arguments.of(1, 1, 1, true),
                Arguments.of(2, 1, 1, true),
                Arguments.of(3, 1, 1, false),
                Arguments.of(6, 1, 1, false),
                Arguments.of(9, 1, 1, true),
                Arguments.of(10, 1, 1, true),
                Arguments.of(11, 1, 1, true),
                Arguments.of(12, 1, 1, true),
                Arguments.of(18, 1, 1, true),
                Arguments.of(19, 1, 1, true),
                Arguments.of(20, 1, 1, true),
                Arguments.of(21, 1, 1, false),
                Arguments.of(28, 1, 1, true)
                )
    }

}


internal class GridTest {

    @Test
    fun `Given a list of Integers check then Grid is created properly`(){
        val grid = listOf(
                0,0,5,2,0,7,8,0,0,
                0,1,0,0,0,0,0,2,0,
                4,0,0,0,0,0,0,0,7,
                0,7,3,0,5,0,1,8,0,
                0,9,0,4,0,8,0,5,0,
                0,8,0,0,0,0,0,9,0,
                0,0,0,0,0,0,0,0,0,
                1,5,0,0,0,0,0,7,6,
                0,0,6,9,0,3,2,0,0).toGrid()

        assertNotNull(grid)
        println(grid.asString())
    }

    @Test
    fun `Given an Empty Grid when an element is set then it is removed from appropriate cells`(){
        val grid = emptyList<Int>().toGrid()
        println(grid.asString())
        val newGrid = grid.setCell(2,0,7)
        println(newGrid.asString())
        assertTrue( newGrid[2] is FixedCell)
        assertTrue( newGrid[0] is OpenCell)
        assertFalse( 7 in (newGrid[0] as OpenCell).options)
        assertFalse( 7 in (newGrid[7] as OpenCell).options)
        assertTrue( 7 in (newGrid[16] as OpenCell).options)
    }

    @Test
    fun `Given a list of Integers check when Grid is solved then answer is generated`(){
        val grid = listOf(
                0,0,5,2,0,7,8,0,0,
                0,1,0,0,0,0,0,2,0,
                4,0,0,0,0,0,0,0,7,
                0,7,3,0,5,0,1,8,0,
                0,9,0,4,0,8,0,5,0,
                0,8,0,0,0,0,0,9,0,
                0,0,0,0,0,0,0,0,0,
                1,5,0,0,0,0,0,7,6,
                0,0,6,9,0,3,2,0,0).toGrid()

        assertNotNull(grid)
        val solution = solve(grid)
        assertNotNull(solution)
        println(solution?.asString())
    }

    @Test
    fun `Given a difficult When Grid is solved Then answer is generated`(){
        val grid = listOf<Int>(
                0,0,9,0,7,0,0,0,0,
                0,8,0,4,0,0,0,0,0,
                0,0,3,0,0,0,0,2,8,
                1,0,0,0,0,0,6,7,0,
                0,2,0,0,1,3,0,4,0,
                0,4,0,0,0,7,8,0,0,
                6,0,0,0,3,0,0,0,0,
                0,1,0,0,0,0,0,0,0,
                0,0,0,0,0,0,2,8,4).toGrid()
        assertNotNull(grid)
        println(grid.asString())

        val solution = solve(grid)
        assertNotNull(solution)
        println("\nSolution\n==========")
        println(solution?.asString())
    }


    @Test
    fun `Given the sample solution When Grid is solved Then answer is generated`(){
        val grid = "...75281.....4...62...83..5..9...283.8.397.4.314...7..1..43...74...7.....95261...".toGrid()
        assertNotNull(grid)
        println(grid.asString())

        val solution = solve(grid)
        assertNotNull(solution)
        println("\nSolution\n==========")
        println(solution?.asString())
    }

    @Test
    fun `Given a string When Grid is solved Then answer is generated`(){
        val grid = """..9.7.....8.4.......3....281.....67..2..13.4..4...78..6...3.....1.............284""".toGrid()
        assertNotNull(grid)
        println(grid.asString())

        val solution = solve(grid)
        assertNotNull(solution)
        println("\nSolution\n==========")
        println(solution?.asString())
    }


}
