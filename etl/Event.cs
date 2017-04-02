﻿public abstract class Event : System.IComparable
{
    public ulong time { get; private set; }
    
        public Event(ulong time)
    {
        this.time = time;
    }

        //public abstract void actuate();

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

    /*public override void actuate()
    {if ( this.road.to.isClear)
    {//{this.road.to.r
    }//end if isClear
    }//end actuate 
     * */

    
    public override string ToString()
    {
        return base.ToString() + " from " + road;
    }
    }//end of CarArrivingEvent class

class IntersectionClearEvent : Event
{
    public Intersection intersection;

    public IntersectionClearEvent( ulong t, Intersection i)
        : base(t)
    { this.intersection  = i; }

    /*public override void actuate()
    { i.clearIntersection(); }
     * */
    }//end IntersectionClearEvent

public class SwitchLightEvent : Event
{
    public Intersection intersection;

    public SwitchLightEvent(ulong time, Intersection i)
        : base(time)
    { this.intersection = i; }
}//end class SwitchLightEvent