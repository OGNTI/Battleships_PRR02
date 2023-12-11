public class Corvette : Ship
{
    public Corvette()
    {
        name = "Corvette";
        size = 2;
        position = new int[size, 2];
        hits = new bool[size];
    }
}
