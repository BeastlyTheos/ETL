public class Road
{
     public Intersection from { get; private set; }
    public Intersection to { get; private set; }
    ulong length;
    uint numWaiting;
    public  bool hasGreen;
    

    public Road(Intersection f, Intersection t, uint l = 1)
    {
        from = f;
        to = t;
         from.addOutgoingRoad(this);
       to.addIncomingRoad(this);
       hasGreen = true;
           }//end constructor 

    public void push()
    {
        Simulation.futureEvents.Add(new EndOfRoadEvent((ulong) Simulation.time + this.length, this));
    }
     
    public void addWaitingCar()
    {numWaiting++;}

    public void pop()
    { numWaiting--; }

    public bool isEmpty()
    { return 0 == numWaiting; }
}//end class Road

