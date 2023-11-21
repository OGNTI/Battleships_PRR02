﻿public class GameBoard
{

    int _boardSize = 10;

    public int boardSize
    {
        get
        {
            return _boardSize;
        }
        set
        {
            _boardSize = value;

            if (_boardSize < 5)
            {
                _boardSize = 5;
            }
            else if (_boardSize > alphabet.Length)
            {
                _boardSize = 26;
            }
        }
    }

    char[,] grid;
    int[,] shipGrid;

    char hit = 'x';
    char miss = '¤';
    char unknown = 'o';

    List<Ship> ships = new List<Ship>();

    public char[] alphabet = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (char)i).ToArray();


    public void GenerateBoard()
    {
        GenerateGrid();
        GenerateShips();
    }

    void GenerateGrid()
    {
        grid = new char[boardSize, boardSize];

        for (int y = 0; y < grid.GetLength(1); y++) //set each space in list to unknown char
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                grid[x, y] = unknown;
            }
        }
    }

    void GenerateShips()
    {
        shipGrid = new int[boardSize, boardSize];
        ships.Clear();

        int f = (int)(boardSize/5);
        if (boardSize >= 20)
        {
            f++;
        }
        
        for (int i = 0; i < f; i++)
        {
            Fleet();
        }


        foreach (Ship s in ships) 
        {
            bool acceptedPositioning = false;
            while (acceptedPositioning == false)
            {
                bool acceptedStartPosition = false;
                while (acceptedStartPosition == false)
                {
                    //first get random starting point
                    s.position[0,0] = Random.Shared.Next(boardSize);
                    s.position[0,1] = Random.Shared.Next(boardSize);
                    
                    if (shipGrid[s.position[0,0], s.position[0,1]] == 0) //if starting position not occupied
                    {
                        shipGrid[s.position[0,0], s.position[0,1]] = 1; //make occupied
                        acceptedStartPosition = true;
                    }
                }

                bool blockedLeft = false;
                bool blockedUp = false;
                bool blockedRight = false;
                bool blockedDown = false;
                //would the ship go outside the board in any direction
                if (s.position[0,0] + 1 - s.size < 0)
                {
                    blockedLeft = true;
                }
                if (s.position[0,1] + 1 - s.size < 0)
                {
                    blockedUp = true;
                }
                if (s.size + s.position[0,0] > boardSize)
                {
                    blockedRight = true;
                }   
                if (s.size + s.position[0,1] > boardSize)
                {
                    blockedDown = true;
                }

                bool acceptedExtensionPositions = false;
                bool noSpaceForExtension = false;
                while (acceptedExtensionPositions == false && noSpaceForExtension == false)
                {
                    int dir = Random.Shared.Next(4); //randomize one direction to check
                    if (dir == 0)
                    {
                        if (!blockedLeft)
                        {
                            for (int i = 1; i < s.size; i++) //set rest of ship positions
                            {
                                s.position[i,0] = s.position[0,0] - i; 
                                s.position[i,1] = s.position[0,1];

                                if (shipGrid[s.position[i,0], s.position[i,1]] == 1) //if one is occupied
                                {
                                    for (int j = 0; j < i; j++) //make the previously placed positions not occupied again
                                    {
                                        shipGrid[s.position[i-1,0], s.position[i-1,1]] = 0;
                                    }
                                    blockedLeft = true;
                                }
                            }

                            if (!blockedLeft)
                            {
                                acceptedExtensionPositions = true;
                                acceptedPositioning = true;
                            }
                        }
                    }
                    else if (dir == 1)
                    {
                        if (!blockedUp)
                        {
                            for (int i = 1; i < s.size; i++)
                            {
                                s.position[i,0] = s.position[0,0];
                                s.position[i,1] = s.position[0,1] - i;

                                if (shipGrid[s.position[i,0], s.position[i,1]] == 1)
                                {
                                    for (int j = 0; j < i; j++)
                                    {
                                        shipGrid[s.position[i-1,0], s.position[i-1,1]] = 0;   
                                    }
                                    blockedUp = true;
                                }
                            }

                            if (!blockedUp)
                            {
                                acceptedExtensionPositions = true;
                                acceptedPositioning = true;
                            }
                        }
                    }
                    else if (dir == 2)
                    {
                        if (!blockedRight)
                        {
                            for (int i = 1; i < s.size; i++)
                            {
                                s.position[i,0] = s.position[0,0] + i;
                                s.position[i,1] = s.position[0,1];

                                if (shipGrid[s.position[i,0], s.position[i,1]] == 1)
                                {
                                    for (int j = 0; j < i; j++)
                                    {
                                        shipGrid[s.position[i-1,0], s.position[i-1,1]] = 0;   
                                    }
                                    blockedRight = true;
                                }
                            }

                            if (!blockedRight)
                            {
                                acceptedExtensionPositions = true;
                                acceptedPositioning = true;
                            }
                        }
                    }
                    else if (dir == 3)
                    {
                        if (!blockedDown)
                        {
                            for (int i = 1; i < s.size; i++)
                            {
                                s.position[i,0] = s.position[0,0];
                                s.position[i,1] = s.position[0,1] + i;

                                if (shipGrid[s.position[i,0], s.position[i,1]] == 1)
                                {
                                    for (int j = 0; j < i; j++)
                                    {
                                        shipGrid[s.position[i-1,0], s.position[i-1,1]] = 0;   
                                    }
                                    blockedDown = true;
                                }
                            }

                            if (!blockedDown)
                            {
                                acceptedExtensionPositions = true;
                                acceptedPositioning = true;
                            }
                        }
                    }
                    
                    if (blockedLeft && blockedUp && blockedRight && blockedDown)
                    {

                        noSpaceForExtension = true;
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
        Console.ForegroundColor = ConsoleColor.White;
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
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{nums} ");

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                if (shipGrid[x,y] == 1) //see ships
                {
                    grid[x,y] = hit;
                }
                
                if (grid[x,y] == hit)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (grid[x,y] == miss)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                Console.Write(grid[x, y] + " ");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        Console.WriteLine();
    }

    public void CheckHit(int[] targetCoords)
    {
        if (shipGrid[targetCoords[0], targetCoords[1]] == 1)
        {
            Console.WriteLine("Hit");
            grid[targetCoords[0], targetCoords[1]] = hit;
        }
        else
        {
            Console.WriteLine("Miss");
            grid[targetCoords[0], targetCoords[1]] = miss;
        }
    }

    void Fleet()
    {
        ships.Add(new Battleship());
        ships.Add(new Cruiser());
        ships.Add(new Destroyer());
        ships.Add(new Corvette());
        ships.Add(new Corvette());
    }
}
