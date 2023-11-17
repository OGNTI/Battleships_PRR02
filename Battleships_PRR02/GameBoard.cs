public class GameBoard
{
    public int boardSize = 10;

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
        ships.Clear();
        shipGrid = new int[boardSize, boardSize];
        ships.Add(new Battleship());
        ships.Add(new Cruiser());
        ships.Add(new Destroyer());
        ships.Add(new Corvette());
        ships.Add(new Corvette());

        foreach (Ship s in ships) //something like if s.size is smaller than s.positions values, not that way
        {
            bool acceptedStartPosition = false;
            while (acceptedStartPosition == false)
            {
                //first get random starting point
                s.position[0,0] = Random.Shared.Next(boardSize);
                s.position[0,1] = Random.Shared.Next(boardSize);
                
                if (shipGrid[s.position[0,0], s.position[0,1]] == 1) //if starting position occupied
                {
                    Console.WriteLine("gg");
                }
                else
                {
                    shipGrid[s.position[0,0], s.position[0,1]] = 1;
                    grid[s.position[0,0], s.position[0,1]] = 'x';
                    acceptedStartPosition = true;
                }
            }

            bool blockedLeft = false;
            bool blockedUp = false;
            bool blockedRight = false;
            bool blockedDown = false;
            if (s.position[0,0] + 1 - s.size < 0) //works, yay
            {
                blockedLeft = true;
                Console.WriteLine("Blocked Left");
            }
            if (s.position[0,1] + 1 - s.size < 0)
            {
                blockedUp = true;
                Console.WriteLine("Blocked Up");
            }
            if (s.size + s.position[0,0] > boardSize)
            {
                blockedRight = true;
                Console.WriteLine("Blocked Right");
            }   
            if (s.size + s.position[0,1] > boardSize)
            {
                blockedDown = true;
                Console.WriteLine("Blocked Down");
            }

            bool acceptedPositioning = false;
            while (acceptedPositioning == false)
            {
                int dir = Random.Shared.Next(4);
                if (dir == 0)
                {
                    if (!blockedLeft)
                    {
                        for (int i = 0; i < s.size; i++)
                        {
                            s.position[i,0] = s.position[0,0] - i;
                            s.position[i,1] = s.position[0,1];

                            if (shipGrid[s.position[i,0], s.position[i,1]] == 1)
                            {
                                blockedLeft = true;
                            }
                        }

                        if (!blockedLeft)
                        {
                            acceptedPositioning = true;
                        }
                    }
                }
                else if (dir == 1)
                {
                    if (!blockedUp)
                    {
                        for (int i = 0; i < s.size; i++)
                        {
                            s.position[i,0] = s.position[0,0];
                            s.position[i,1] = s.position[0,1] - i;

                            if (shipGrid[s.position[i,0], s.position[i,1]] == 1)
                            {
                                blockedUp = true;
                            }
                        }

                        if (!blockedUp)
                        {
                            acceptedPositioning = true;
                        }
                    }
                }
                else if (dir == 2)
                {
                    if (!blockedRight)
                    {
                        for (int i = 0; i < s.size; i++)
                        {
                            s.position[i,0] = s.position[0,0] + i;
                            s.position[i,1] = s.position[0,1];

                            if (shipGrid[s.position[i,0], s.position[i,1]] == 1)
                            {
                                blockedRight = true;
                            }
                        }

                        if (!blockedRight)
                        {
                            acceptedPositioning = true;
                        }
                    }
                }
                else if (dir == 3)
                {
                    if (!blockedDown)
                    {
                        for (int i = 0; i < s.size; i++)
                        {
                            s.position[i,0] = s.position[0,0];
                            s.position[i,1] = s.position[0,1] + i;

                            if (shipGrid[s.position[i,0], s.position[i,1]] == 1)
                            {
                                blockedDown = true;
                            }
                        }

                        if (!blockedDown)
                        {
                            acceptedPositioning = true;
                        }
                    }
                }
            }
            
            for (int i = 0; i < s.size; i++)
            {
                shipGrid[s.position[i,0], s.position[i,1]] = 1;  
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
                if (shipGrid[x,y] == 1)
                {
                    grid[x, y] = 'x';
                }
                Console.Write(grid[x, y] + " ");
            }
        }
    }
}
