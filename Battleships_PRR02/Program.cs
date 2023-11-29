Player player1 = new();
Player player2 = new();

bool singleplayer = true;

Console.WriteLine("One player or two players?");
string answer = Console.ReadLine().ToLower().Trim();
if (answer == "2" || answer == "two") //make this actually do something next
{
    singleplayer = false;
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
}
else
{
    singleplayer = true;
    player1.SetPlayer();
    player1.SetBoard();
}

Console.Clear();

bool gaming = true;
while (gaming)
{

    if (singleplayer)
    {
        player1.board.DrawBoard(false);

        player1.board.FireAndCheckHit(player1.GetTarget());
    }
    else
    {
        Console.WriteLine($"{player1.name}, type the coordinate where you think your opponent has placed a ship.");
        player2.board.DrawBoard(false);
        player2.board.FireAndCheckHit(player1.GetTarget());


        Console.WriteLine($"{player2.name}, type the coordinate where you think your opponent has placed a ship.");
        player1.board.DrawBoard(false);
        player1.board.FireAndCheckHit(player2.GetTarget());
    }
}

