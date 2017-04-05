public class Vehicle
{
    public int  getDirection( int currentDirection)
    {
        double  r = Simulation.rand.NextDouble();
        if (1 > r)
            return currentDirection;
        else if (0.85 > r)
            return (currentDirection + 1) % 4; //turn right
        else
            return (currentDirection - 1) % 4; // turn left
                                    }//end getDirection
}//end Car
