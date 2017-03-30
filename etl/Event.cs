public abstract class Event : System.IComparable
{
    private uint time;


    public Event(uint time)
    {
        this.time = time;
    }

    public double Time
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


public class CarArrivalEvent : Event
{
    Road road;

    //constructor
    public CarArrivalEvent(uint time, Road r): base(time)
    {
        this.road = r;
    }

    public int Road
    { get { return Road; } }

    public override string ToString()
    {
        return base.ToString() + " from " + road;
    }
    }//end of CarArrivingEvent class
