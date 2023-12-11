public class Battleship : Ship
{
    public Battleship()
    {
        name = "Battleship";
        size = 5;
        position = new int[size, 2];
        hits = new bool[size];
    }
}
