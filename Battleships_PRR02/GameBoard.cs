public class GameBoard
{
    public int boardSize = 3;

    char[,] grid;
    public int[,] shipGrid;

    public char[] alphabet = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (char)i).ToArray();

    List<Ship> ships = new List<Ship>();

    public void GenerateBoard()
    {
        GenerateGrid();
        GenerateShips();
    }

    void GenerateGrid()
    {
        grid = new char[boardSize, boardSize];

        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                grid[x, y] = 'O';
            }
        }
    }

    void GenerateShips()
    {
        shipGrid = new int[boardSize, boardSize];
        ships.Add(new Battleship());
        ships.Add(new Cruiser());
        ships.Add(new Destroyer());
        ships.Add(new Corvette());
        ships.Add(new Corvette());

        foreach (Ship s in ships) //something like if s.size is smaller than s.positions values, redo positioning
        {
            bool acceptedPosition = false;
            while (acceptedPosition == false)
            {
                for (int i = 0; i < s.position.Length; i++)
                {
                    s.position[0][i,0] = Random.Shared.Next(boardSize);
                    s.position[0][0,i] = Random.Shared.Next(boardSize);
                }

                if (shipGrid[s.position[0][0,0], s.position[0][0,1]] == 1) //don't fucking know, hurt itself in confusion
                {
                    Console.WriteLine("gg");
                }
                else
                {
                    shipGrid[s.position[0], s.position[1]] = 1;
                    grid[s.position[0], s.position[1]] = '1';
                }
            }
        }
    }

    public void DrawBoard()
    {
        Console.Write("   ");
        for (int i = 0; i < grid.GetLength(1); i++)
        {
            Console.Write(alphabet[i] + " ");
        }

        int nums = 0;
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            nums++;
            if (nums < 10)
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
