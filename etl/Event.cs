public abstract class Event : System.IComparable
{
    private uint time;

    public abstract void actuate();
    public Event(uint time)
    {
        this.time = time;
    }

    public uint  Time
    { get { return this.time; } }

    public override string ToString()
    {
        return this.GetType() + " at " + (int)this.time;
    }

    public int CompareTo(object obj)
    {
        Event other = obj as Event;
        if (other == null)
            return 1;
        else
            return other.time.CompareTo(this.time);
    }
}//end of Event class


public class  EndOfRoadEvent: Event
{
    Road road;

    //constructor
    public EndOfRoadEvent(uint time, Road r): base(time)
    {
        this.road = r;
    }

    public override void actuate()
    { this.road.to.push(this.road); }
    
    public override string ToString()
    {
        return base.ToString() + " from " + road;
    }
    }//end of CarArrivingEvent class

class IntersectionClearEvent : Event
{
    Intersection i;
    public IntersectionClearEvent( uint  t, Intersection i)
        : base(t)
    { this.i = i; }

    public override void actuate()
    { i.clearIntersection(); }
    }//end IntersectionClearEvent
