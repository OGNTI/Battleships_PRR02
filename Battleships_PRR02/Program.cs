using System.Text.RegularExpressions;

GameBoard gameBoard1 = new GameBoard();

gameBoard1.GenerateBoard();

bool gaming = true;
while (gaming)
{
    gameBoard1.DrawBoard();

    GetUserInput();

}

int[,] GetUserInput()
{
    int[,] coordinates = new int[1,1];
    bool acceptedInput = false;
    while (acceptedInput == false)
    {
        string userInput = Console.ReadLine().ToLower().Trim();

        string lettersOnly = Regex.Replace(userInput, "[^a-z.]", "");
        string numbersOnly = Regex.Replace(userInput, "[^0-9.]", "");

        Console.WriteLine(numbersOnly);
        Console.WriteLine(lettersOnly);

        bool acceptedLetter = false;
        bool acceptedNumber = false;

        string firstLetter = " ";
        if (lettersOnly.Length > 0)
        {
            firstLetter = lettersOnly.Substring(0,1);
        }

        bool isLetter = char.TryParse(firstLetter, out char xChar);
        bool isNumber = int.TryParse(numbersOnly, out int y);
        if (!isLetter && !isNumber)
        {
            Console.WriteLine("You did not type coordinates. \nTry again.");
        }
        else if (!isLetter)
        {
            Console.WriteLine("You did not type a letter coordinate. \nTry again.");
        }
        else if (!isNumber)
        {
            Console.WriteLine("You did not type a number coordinate. \nTry again.");
        }
    }

    return coordinates;
}




