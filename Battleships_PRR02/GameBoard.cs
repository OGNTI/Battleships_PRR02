using System.Text.RegularExpressions;

public class GameBoard
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

    ConsoleColor standardColour = ConsoleColor.Gray;
    ConsoleColor coorindateColour = ConsoleColor.White;

    char hit = 'x';
    ConsoleColor hitColour = ConsoleColor.DarkGray;

    char miss = '¤';
    ConsoleColor missColour = ConsoleColor.Red;

    char unknown = 'o';
    ConsoleColor unknownColour = ConsoleColor.Blue;

    List<Ship> ships = new List<Ship>();

    public char[] alphabet = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (char)i).ToArray();

    // public List<string> gridCoordinates = new List<string>();

    public void GenerateGrid()
    {
        grid = new char[boardSize, boardSize];
        shipGrid = new int[boardSize, boardSize];

        for (int y = 0; y < grid.GetLength(1); y++) //set each space in list to unknown char
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                grid[x, y] = unknown;
            }
        }
    }

    public void GenerateShips()
    {
        ships.Clear();

        int f = (int)(boardSize / 5);
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
            bool acceptedPlacement = false;
            while (acceptedPlacement == false)
            {
                //first get random starting point
                int[] startPosition = new int[2];
                startPosition[0] = Random.Shared.Next(boardSize);
                startPosition[1] = Random.Shared.Next(boardSize);

                int dir = Random.Shared.Next(4);

                acceptedPlacement = CheckShipPositioning(s, startPosition, dir);
            }

            SetShipPositions(s);
        }
    }

    public void DrawBoard(bool isPlacing)
    {
        Console.ForegroundColor = coorindateColour;
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
            Console.ForegroundColor = coorindateColour;
            Console.Write($"{nums} ");

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                if (isPlacing)
                {
                    if (shipGrid[x, y] == 1) //see ships during placement
                    {
                        grid[x, y] = hit;
                    }
                }

                if (grid[x, y] == hit)
                {
                    Console.ForegroundColor = hitColour;
                }
                else if (grid[x, y] == miss)
                {
                    Console.ForegroundColor = missColour;
                }
                else
                {
                    Console.ForegroundColor = unknownColour;
                }
                Console.Write(grid[x, y] + " ");
            }
            Console.ForegroundColor = standardColour;
        }
        Console.WriteLine();
    }

    public void FireAndCheckHit(int[] targetCoords)
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

    bool CheckShipPositioning(Ship s, int[] placementTarget, int dir)
    {
        s.position[0, 0] = placementTarget[0];
        s.position[0, 1] = placementTarget[1];

        if (shipGrid[s.position[0, 0], s.position[0, 1]] == 1) //if starting position occupied
        {
            Console.WriteLine("Occupied \nTry again.");
            return false;
        }

        bool acceptedPositioning = false;
        bool blockedLeft = false;
        bool blockedUp = false;
        bool blockedRight = false;
        bool blockedDown = false;
        //would the ship go outside the board in any direction
        if (s.position[0, 0] + 1 - s.size < 0)
        {
            blockedLeft = true;
        }
        if (s.position[0, 1] + 1 - s.size < 0)
        {
            blockedUp = true;
        }
        if (s.size + s.position[0, 0] > boardSize)
        {
            blockedRight = true;
        }
        if (s.size + s.position[0, 1] > boardSize)
        {
            blockedDown = true;
        }

        if (dir == 0)
        {
            if (!blockedLeft)
            {
                for (int i = 1; i < s.size; i++) //check rest of ship positions
                {
                    s.position[i, 0] = s.position[0, 0] - i;
                    s.position[i, 1] = s.position[0, 1];

                    if (shipGrid[s.position[i, 0], s.position[i, 1]] == 1) //if one is occupied
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
                for (int i = 1; i < s.size; i++)
                {
                    s.position[i, 0] = s.position[0, 0];
                    s.position[i, 1] = s.position[0, 1] - i;

                    if (shipGrid[s.position[i, 0], s.position[i, 1]] == 1)
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
                for (int i = 1; i < s.size; i++)
                {
                    s.position[i, 0] = s.position[0, 0] + i;
                    s.position[i, 1] = s.position[0, 1];

                    if (shipGrid[s.position[i, 0], s.position[i, 1]] == 1)
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
                for (int i = 1; i < s.size; i++)
                {
                    s.position[i, 0] = s.position[0, 0];
                    s.position[i, 1] = s.position[0, 1] + i;

                    if (shipGrid[s.position[i, 0], s.position[i, 1]] == 1)
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

        if (!acceptedPositioning) Console.WriteLine("Blocked. \nTry again.");

        return acceptedPositioning;
    }

    public void PlaceShip(Player player)
    {
        int a = ships.Count();
        if (a == 0)
        {
            ships.Add(new Battleship());
        }
        else if (a == 1)
        {
            ships.Add(new Cruiser());
        }
        else if (a == 2)
        {
            ships.Add(new Destroyer());
        }
        else if (a == 3 || a == 4)
        {
            ships.Add(new Corvette());
        }

        Console.WriteLine($"{player.name} \nType starting coordinate followed by direction for ship to be placed in. [e.g A1 Right, B5 Down, K9 Up] \nCurrently placing {ships[a].size} spaces long ship.");

        bool acceptedPlacement = false;
        while (acceptedPlacement == false)
        {
            int[] placementTarget = new int[2];
            int dir = 0;
            bool acceptedInput = false;
            while (acceptedInput == false)
            {
                bool acceptedDirection = false;

                string userInput = Console.ReadLine().ToLower().Trim();

                bool acceptedCoordinates = false;
                if (userInput.Contains(" "))
                {
                    int to = userInput.IndexOf(" ");
                    string possibleCoordinates = userInput.Substring(0, to);

                    var result = GetTargetFromUserInput(possibleCoordinates);
                    placementTarget = result.target;
                    acceptedCoordinates = result.accepted;
                }
                else
                {
                    Console.WriteLine("You did not space your coordinates and direction like instructed. \nTry again.");
                }

                if (userInput.Contains("left")) { dir = 0; acceptedDirection = true; }
                else if (userInput.Contains("up")) { dir = 1; acceptedDirection = true; }
                else if (userInput.Contains("right")) { dir = 2; acceptedDirection = true; }
                else if (userInput.Contains("down")) { dir = 3; acceptedDirection = true; }
                else Console.WriteLine("You did not type an accepted direction. \nTry again.");

                if (acceptedCoordinates && acceptedDirection)
                {
                    acceptedInput = true;
                }
            }

            acceptedPlacement = CheckShipPositioning(ships[a], placementTarget, dir);
            if (acceptedPlacement)
            {
                SetShipPositions(ships[a]);
            }
        }
    }

    public (int[] target, bool accepted) GetTargetFromUserInput(string userInput)
    {
        int[] coordinates = new int[2];
        bool acceptedInput = false;

        string lettersOnly = Regex.Replace(userInput, "[^a-z.]", "");
        string numbersOnly = Regex.Replace(userInput, "[^0-9.]", "");

        if (lettersOnly.Length <= 0 && numbersOnly.Length <= 0)
        {
            Console.WriteLine("You did not type coordinates. \nTry again.");
        }
        else if (lettersOnly.Length <= 0)
        {
            Console.WriteLine("You did not type a letter coordinate. \nTry again.");
        }
        else if (numbersOnly.Length <= 0)
        {
            Console.WriteLine("You did not type a number coordinate. \nTry again.");
        }
        else
        {
            char.TryParse(lettersOnly.Substring(0, 1).ToUpper(), out char xChar);
            int.TryParse(numbersOnly, out int y);

            int x = Array.IndexOf(alphabet, xChar);
            y -= 1;

            if (x < boardSize && y < boardSize)
            {
                coordinates[0] = x;
                coordinates[1] = y;
                acceptedInput = true;
            }
            else
            {
                Console.WriteLine("Your coordinates are out of range. \nTry again.");
            }

        }

        if (lettersOnly.Length > 1)
        {
            Console.WriteLine("Took the first letter as coordinate.");
        }

        var result = (coordinates, acceptedInput);
        return result;
    }

    void SetShipPositions(Ship s)
    {
        for (int i = 0; i < s.size; i++)
        {
            shipGrid[s.position[i, 0], s.position[i, 1]] = 1;
        }
    }

    // void SetCoordinates()
    // {
    //     for (int y = 0; y < grid.GetLength(1); y++) //set each space in list to unknown char
    //     {
    //         for (int x = 0; x < grid.GetLength(0); x++)
    //         {
    //             string coordinate = alphabet[x].ToString() + (y + 1);
    //             gridCoordinates.Add(coordinate);
                
    //         }
    //     }
    // }
}