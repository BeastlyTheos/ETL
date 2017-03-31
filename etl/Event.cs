public abstract class Event : System.IComparable
{
    public ulong time { get; private set; }
    
        public Event(ulong time)
    {
        this.time = time;
    }

        public abstract void actuate();

        public override string ToString()
    {
        return this.GetType() + " at " +this.time;
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
    public Road road { get; private set; }

        public EndOfRoadEvent(ulong time, Road r): base(time)
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
    public IntersectionClearEvent( ulong t, Intersection i)
        : base(t)
    { this.i = i; }

    public override void actuate()
    { i.clearIntersection(); }
    }//end IntersectionClearEvent
