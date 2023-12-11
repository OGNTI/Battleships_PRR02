public class Player
{
    public string name;
    public int? designation = null;
    public GameBoard board = new();
    public int _shots = 30;
    public int shots 
    {
        get
        {
            return _shots;
        }
        set
        {
            _shots = value;

            if (_shots < 0)
            {
                _shots = 0;
            }
        }
    }


    public void SetPlayer()
    {
        Console.WriteLine($"Player{designation}, What is your name?");
        name = Console.ReadLine().Trim();
        if (name == "")
        {
            name = "fucker" + designation;
        }
    }

    public void SetBoard()
    {
        Console.WriteLine("How big of a board?");
        bool trulyNumber = int.TryParse(Console.ReadLine(), out int a);
        if (trulyNumber)
        {
            board.boardSize = a;
            shots = board.boardSize * 3;
        }

        board.GenerateGrid();
        board.GenerateShips();
    }

    public int[] GetTarget()
    {
        int[] target = new int[2];
        bool acceptedCoordinates = false;
        while (acceptedCoordinates == false)
        {
            string possibleCoordinates = Console.ReadLine().ToLower().Trim();

            var result = board.GetTargetFromUserInput(possibleCoordinates);
            target = result.target;
            acceptedCoordinates = result.accepted;
        }

        return target;
    }

    public bool IsOutOfShots()
    {
        bool isOut = false;
        if (shots == 0)
        {
            isOut = true;
        }

        return isOut;
    }
}
