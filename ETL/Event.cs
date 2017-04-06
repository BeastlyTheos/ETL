public abstract class Event : System.IComparable
{
    public int time { get; private set; }

    public Event(int time)
    {
        this.time = time;
    }

    public override string ToString()
    {
        return this.GetType() + " at " + this.time;
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

public class ResetStatisticsEvent : Event
{ public ResetStatisticsEvent(int t) : base(t) { } }

public class EndOfRoadEvent : Event
{
    public Road road { get; private set; }
    public Vehicle vehicle;

    public EndOfRoadEvent(int time, Road r, Vehicle v)
        : base(time)
    {
        this.road = r;
        this.vehicle = v;
    }

    public override string ToString()
    {
        return base.ToString() + " from " + road;
    }
}//end of CarArrivingEvent class

class IntersectionClearEvent : Event
{
    public Intersection intersection;

    public IntersectionClearEvent(int t, Intersection i)
        : base(t)
    { this.intersection = i; }
}//end IntersectionClearEvent

public class SwitchLightEvent : Event
{
    public Intersection intersection;

    public SwitchLightEvent(int time, Intersection i)
        : base(time)
    { this.intersection = i; }

    public override bool Equals(object obj)
    {
        SwitchLightEvent other = obj as SwitchLightEvent;
        if (other == null)
            return false;
        else
            return this.GetType() == other.GetType() && this.intersection.name == other.intersection.name;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}//end class SwitchLightEvent

public class GetDestinationEvent : Event
{
    public Ambulance ambulance;

    public GetDestinationEvent(int t, Ambulance a)
        : base(t)
    { this.ambulance = a; }
}//end GetDestination
