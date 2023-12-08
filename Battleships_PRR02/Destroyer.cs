public class Destroyer  : Ship
{
    public Destroyer()
    {
        name = "Destroyer";
        size = 3;
        position = new int[size,2];
        hit = new bool[size];
    }
}
