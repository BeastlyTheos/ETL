
public class Road
{
    public Intersection from { get; private set; }
    public Intersection to { get; private set; }
    uint length;
    uint numWaiting;
    bool hasGreen;
    public  Road leadsTo { get; private  set; }

    public Road(Intersection f, Intersection t, uint l = 1)
    {
        from = f;
        to = t;
         from.addOutgoingRoad(this);
       to.addIncomingRoad(this);
       hasGreen = true;
       leadsTo = null;
    }//end constructor 

    public void push()
    {
        Simulation.futureEvents.Add(new EndOfRoadEvent(Simulation.time + this.length, this));
    }
     
    public void addWaitingCar()
    {numWaiting++;}

    public void pop()
    { numWaiting--; }

    public bool isEmpty()
    { return 0 == numWaiting; }
}//end class Road

