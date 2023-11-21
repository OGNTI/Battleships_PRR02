using System.Text.RegularExpressions;

GameBoard gameBoard1 = new GameBoard();

gameBoard1.GenerateBoard();

bool gaming = true;
while (gaming)
{
    gameBoard1.DrawBoard();

    gameBoard1.CheckHit(GetTargetFromUserInput(gameBoard1));
}

int[] GetTargetFromUserInput(GameBoard board)
{
    int[] coordinates = new int[2];
    bool acceptedInput = false;
    while (acceptedInput == false)
    {
        string userInput = Console.ReadLine().ToLower().Trim();

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
            char.TryParse(lettersOnly.Substring(0,1).ToUpper(), out char xChar);
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
    }

    return coordinates;
}




