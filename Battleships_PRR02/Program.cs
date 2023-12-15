Console.WriteLine("One player or two players?");
string answer = Console.ReadLine().ToLower().Trim();
if (answer == "2" || answer == "two") 
{
    Player player1 = new();
    Player player2 = new();

    player1.designation = 1;
    player2.designation = 2;

    player1.SetPlayer();
    player1.board.GenerateGrid();
    player2.SetPlayer();
    player2.board.GenerateGrid();

    for (int i = 0; i < 5; i++)
    {
        Console.Clear();
        Console.WriteLine("Ship Placement:");
        player1.board.DrawBoard(true);
        player1.board.PlaceShip(player1);
    }
    for (int i = 0; i < 5; i++)
    {
        Console.Clear();
        Console.WriteLine("Ship Placement:");
        player2.board.DrawBoard(true);
        player2.board.PlaceShip(player2);
    }

    Console.Clear();
    bool gaming = true;
    while (gaming)
    {
        if (player1.shots > 0)
        {
            Console.WriteLine($"{player1.name}, type the coordinate where you think your opponent has placed a ship. \nYou have {player1.shots} shots left.");
            player2.board.DrawBoard(false);
            player2.board.FireAndCheckHit(player1.GetTarget(), player1);
        }
        else Console.WriteLine($"{player1.name}, you have no more shots");

        if (player2.shots > 0)
        {
            Console.WriteLine($"{player2.name}, type the coordinate where you think your opponent has placed a ship. \nYou have {player2.shots} shots left.");
            player1.board.DrawBoard(false);
            player1.board.FireAndCheckHit(player2.GetTarget(), player2);
        }
        else Console.WriteLine($"{player2.name}, you have no more shots");

        if (player2.board.IsGameEnd())
        {
            Console.WriteLine($"Congrats {player1.name}, you won.");
            gaming = false;
        }
        else if (player1.board.IsGameEnd())
        {
            Console.WriteLine($"Congrats {player2.name}, you won.");
            gaming = false;
        }
        else if (player1.shots == 0 && player2.shots == 0)
        {
            Console.WriteLine($"Congrats, you both lost.");
            gaming = false;
        }
    }
}
else
{
    Player singlePlayer = new();

    singlePlayer.SetPlayer();
    singlePlayer.SetBoard();

    Console.Clear();
    bool gaming = true;
    while (gaming)
    {
        Console.WriteLine($"{singlePlayer.name}, type the coordinate where you a ship is placed. \nYou have {singlePlayer.shots} shots left.");
        singlePlayer.board.DrawBoard(false);
        singlePlayer.board.FireAndCheckHit(singlePlayer.GetTarget(), singlePlayer);

        if (singlePlayer.board.IsGameEnd())
        {
            Console.WriteLine("Congrats, you won.");
            gaming = false;
        }
        else if (singlePlayer.IsOutOfShots())
        {
            Console.WriteLine("Congrats, you lost.");
            gaming = false;
        }
    }
}

Console.WriteLine("End");
Console.ReadLine();