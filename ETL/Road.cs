
public class Road
{
    Intersection from;
    Intersection to;
    uint length;
    uint numWaiting;

    public Road(Intersection f, Intersection t, uint l)
    {
        from = f;
        to = t;
        from.addOutgoingRoad(this);
        to.addIncomingRoad(this);
    }//end constructor 

    public void push()
    {
        //Simulation.futureEvents.Add(new CarArrivalEvent(CarArrivalEvent, Simulation.time + this.length, this));
    }

    public void pop()
    { numWaiting--; }

    public bool isEmpty()
    { return 0 == numWaiting; }
}//end class Road

