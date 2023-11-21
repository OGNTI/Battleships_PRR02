using System.Text.RegularExpressions;

List<Player> players = new();

bool singleplayer = true;

Console.WriteLine("One player or two players?");
string answer = Console.ReadLine().ToLower().Trim();
if (answer == "2" || answer == "two") //make this actually do something next
{
    singleplayer = false;
    players.Add(new Player());
    players[0].SetPlayer();
    players[0].designation = 1;

    players.Add(new Player());
    players[1].SetPlayer();
    players[1].designation = 2;
}
else
{
    singleplayer = true;
    players.Add(new Player());
    players[0].SetPlayer();
}

if (singleplayer)
{
    players[0].SetBoard();    
}

bool gaming = true;
while (gaming)
{
    foreach (Player p in players)
    {
        p.board.DrawBoard();

        p.board.CheckHit(GetTargetFromUserInput(p.board));     
    }
}

int[] GetTargetFromUserInput(GameBoard board)
{
    int[] coordinates = new int[2];
    bool acceptedInput = false;
    while (acceptedInput == false)
    {
        string userInput = Console.ReadLine().ToLower().Trim();
        Console.WriteLine();

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

            int x = Array.IndexOf(board.alphabet, xChar);
            y -= 1;

            if (x < board.boardSize && y < board.boardSize)
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
    }

    return coordinates;
}




