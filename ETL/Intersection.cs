using System;
using System.Collections.Generic;

public class Intersection
{
    public Road[] outgoing; //roads that lead to the intersection
    public Road[] incoming; //roads that leave the intersection

    private int numBlocking; //number of events that have blocked the intersection (vehicles driving through, light switching)
    public string name { get; private set; }
    static uint numIntersections = 0;

    public Intersection(string name = null)
    {
        numIntersections++;
        this.name = null != name ? name : string.Format("int{0}", numIntersections);
        numBlocking = 0;

        outgoing = new Road[4];
        incoming = new Road[4];
    }

    ~Intersection()
    { numIntersections--; }

    public override string ToString()
    { return this.name;}
    

    /* switch lights
     * switches the lights for each road between green and red */
    public void switchLights()
    {
        foreach (Road r in this.incoming)
            r.HasGreen = r.HasGreen ? false : true;
    }//end switch lights

    /* block
     * blocks any vehicle from going through the intersection for a given time */
     public void block(int time)
    {
        numBlocking++;
        Simulation.futureEvents.Add(new IntersectionClearEvent(Simulation.time + time, this));
    }//end block

    public void unblock()
    {
        numBlocking--;
        if (0 > numBlocking)
            throw new System.Exception("Excessive clearing: numBlocking for intersection " + this.name + " is negative!");
    }//end unblock

    public bool isClear()
    {
        if (0 > numBlocking)
            throw new System.Exception("Excessive clearing: numBlocking for intersection " + this.name + " is negative!");
        return 0 == numBlocking;
    }//end isClear
}//end class Intersection
