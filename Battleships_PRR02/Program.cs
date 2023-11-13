
GameBoard gameBoard = new GameBoard();

int a = 0;

for (int y = 0; y < gameBoard.grid.GetLength(0); y++)
{
    Console.WriteLine();
    for (int x = 0; x < gameBoard.grid.GetLength(1); x++)
    {
        Console.Write(gameBoard.grid[x, y] + " ");
        a++;
    }
}

Console.WriteLine();
Console.WriteLine(a);

Console.ReadLine();



