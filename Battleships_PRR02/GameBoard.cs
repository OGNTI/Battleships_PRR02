public class GameBoard
{
    public char[,] grid = new char[10, 10];
    public int[,] shipGrid = new int[10, 10];

    public char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
    List<Ship> ships = new List<Ship>();


    public void GenerateBoard()
    {
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                grid[x, y] = 'o';
            }
        }
    }

    public void DrawBoard()
    {
        Console.Write("   ");
        foreach (char l in letters)
        {
            Console.Write(l + " ");
        }

        int nums = 0;
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            nums++;
            if (nums != 10)
            {
                Console.Write("\n ");
            }
            else
            {
                Console.Write("\n");
            }
            Console.Write($"{nums} ");

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                Console.Write(grid[x, y] + " ");
            }
        }
    }
}
