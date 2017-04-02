public class Road
{
     public Intersection from { get; private set; }
    public Intersection to { get; private set; }
    int  length;
    uint numWaiting;
    public  bool hasGreen;
    public Direction  dir;
    
     public  Road(Intersection f, Intersection t, Direction  d, uint l = 1)
    {
                 from = f;
        to = t;
                        this.dir = d;
         hasGreen = true;

         if (Direction.northwards == dir)
         {
             from.toNorth = this;
             to.fromSouth = this;
         }
         else if (Direction.eastwards == dir)
         {
             from.toEast = this;
             to.fromWest = this;
         }
         else if (Direction.southwards == dir)
         {
             from.toSouth = this;
             to.fromNorth = this;
         }
         else if (Direction.westwards == dir)
         {
             from.toWest = this;
             to.fromEast = this;
         }
         else
             throw new System.ArgumentException("invalid direction");
                    }//end constructor 

    public void push()
    {
        Simulation.futureEvents.Add(new EndOfRoadEvent( Simulation.time + this.length, this));
    }
     
    public void addWaitingCar()
    {numWaiting++;}

    public void pop()
    { numWaiting--; }

    public bool isEmpty()
    { return 0 == numWaiting; }
}//end class Road

