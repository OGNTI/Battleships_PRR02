public class Ship
{
    public string name;
    public int size;
    public int[,] position;
    public bool[] hits;


    public bool IsShipSunk()
    {
        bool sunk = true;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == false)
            {
                sunk = false;
            }
        }

        return sunk;
    }
}

