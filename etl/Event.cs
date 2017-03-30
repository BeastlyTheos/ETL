public class  Event : System.IComparable
{
    protected EventType type;
            protected double time;

    public Event(EventType type, double time)
    {
                this.type = type;
        this.time = time;
    }

    public EventType Type
    { get { return type; } }

    public double Time
    { get { return this.time; } }

    public  override string ToString()
    {
        return this.type + " at " + (int)this.time;
    }

    public   void test(int x)
    { x = 1; return; }
    
    public int CompareTo(object obj)
    {
        Event other = obj as Event;
        if (other == null)
            return 1;
        else
            return other.time.CompareTo(this.time);
    }
}


public class CarArrivalEvent : Event
{
    Road road;

    //constructor
    public CarArrivalEvent(EventType type, uint time, Road r)
        : base(type, time)
    {
        this.road = r;
    }

    public int Road
    { get { return Road; } }

    public override string ToString()
    {
        return base.ToString() + " from " + road;
    }
    //public  override void test(int x)
    //{ x = 8; return; }
}//end of CarArrivingEvent class
