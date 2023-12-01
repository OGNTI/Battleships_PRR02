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
        Console.WriteLine($"{player1.name}, type the coordinate where you think your opponent has placed a ship.");
        player2.board.DrawBoard(false);
        player2.board.FireAndCheckHit(player1.GetTarget());

        Console.WriteLine($"{player2.name}, type the coordinate where you think your opponent has placed a ship.");
        player1.board.DrawBoard(false);
        player1.board.FireAndCheckHit(player2.GetTarget());
    }
}
else
{
    Player SinglePlayer = new();

    SinglePlayer.SetPlayer();
    SinglePlayer.SetBoard();

    Console.Clear();
    bool gaming = true;
    while (gaming)
    {
        SinglePlayer.board.DrawBoard(false);
        SinglePlayer.board.FireAndCheckHit(SinglePlayer.GetTarget());
    }
}


