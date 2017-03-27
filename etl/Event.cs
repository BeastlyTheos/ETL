/*cois 2020h assignment 2 part b
 * Theodore Cooke 0560425
 * 
 * class Event
 * holds type of event and time of occurance
 * 
 * class CarArrivalEvent : Event
 * adds direction as an int which corresponds to index of its associated direction object
 */


public class Event : System.IComparable
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
    int dir;

    //constructor
    public CarArrivalEvent(EventType type, double time, int dir)
        : base(type, time)
    {
        this.dir = dir;
            }

    public int Dir
    { get { return dir; } }

    public  override string ToString()
    {
        return base.ToString() + " from "+dir;
    }
    //public  override void test(int x)
    //{ x = 8; return; }
}//end of CarArrivingEvent class
