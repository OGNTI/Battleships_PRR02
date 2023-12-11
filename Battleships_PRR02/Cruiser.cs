public class Cruiser : Ship
{
    public Cruiser()
    {
        name = "Cruiser";
        size = 4;
        position = new int[size, 2];
        hits = new bool[size];
    }
}
