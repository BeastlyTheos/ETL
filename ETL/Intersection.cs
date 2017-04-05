using System;
using System.Collections.Generic;

public class Intersection
{
    public Road[] outgoing;
    public Road[] incoming;

     private   int numBlocking;
        public string name { get; private set; }
        static uint numIntersections = 0;

    public Intersection( string name = null)
    {numIntersections++;
    this.name = null != name ? name : string.Format("int{0}", numIntersections);
    numBlocking = 0;

        outgoing = new Road[4];
        incoming = new Road[4];
                   }

    ~Intersection()
    { numIntersections--; }

    public override string ToString()
    {
        return this.name;}

    public void sanityCheck()
    {
        for (int i = 0; i < 4 ; i++)
            Console.WriteLine("Road from {0} thinks its going {1}", (direction)i, (direction)  incoming[i].dir);
        for (int i = 0; i < 4 ; i++)
            Console.WriteLine("Road to {0} thinks its going {1}", (direction)i, (direction) outgoing[i].dir);
        }//end sanity check

    public void switchLights()
    {
        foreach (Road r in this.incoming)
            r.HasGreen = r.HasGreen ? false : true;
                printLights();
    }//end switch lights

    public void printLights()
    {
        System.Console.WriteLine("Lights for {0} are {1}, {2}, {3}, {4}\n", this.name.ToString(), 
            incoming[0].HasGreen, incoming[1].HasGreen, incoming[2 ].HasGreen, incoming[3].HasGreen);
               }

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
            throw new System.Exception("Excessive clearing: numBlocking for intersection "+this.name+" is negative!");
        return 0 == numBlocking;
    }//end isClear
}//end class Intersection
