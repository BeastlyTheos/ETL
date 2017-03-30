
public class Road
{
    public Intersection From { get; private set; }
    public Intersection To { get; private set; }
    uint length;
    uint numWaiting;

    public Road(Intersection f, Intersection t, uint l = 1)
    {
        From = f;
        To = t;
         From.addOutgoingRoad(this);
       To.addIncomingRoad(this);
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

