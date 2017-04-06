public class Vehicle
{
    public virtual int  getDirection( int currentDirection)
    {
        double  r = Simulation.rand.NextDouble();
        
        if ( (Simulation.debug ? 1:  0.7) > r)
            return currentDirection;
        else if (0.85 > r)
            return (currentDirection + 1) % 4; //turn right
        else
            return (currentDirection + 3) % 4; // turn left
                                    }//end getDirection
}//end Vehicle

public class Ambulance : Vehicle
{
    public override int getDirection(int currentDirection)
    {
        return base.getDirection(currentDirection);
    }
}//end class ambulance
