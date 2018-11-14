namespace Sudoku
{
    public class GridBuilder
    {
        public static Grid Easy()
        {
            return GridLoader.Load(@"...|752|81.
                                     ...|.4.|..6
                                     2..|.83|..5
                                     -----------   
                                     ..9|...|283
                                     .8.|397|.4.
                                     314|...|7..
                                     -----------
                                     1..|43.|..7
                                     4..|.7.|...
                                     .95|261|...");

        }
    }
}
