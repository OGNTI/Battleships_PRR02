public class Player
{
    public string name;
    public int? designation = null;
    public GameBoard board = new();


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
        }


        board.GenerateGrid();
        board.GenerateShips();
    }
}
