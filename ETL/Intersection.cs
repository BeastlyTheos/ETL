
using System.Collections.Generic;

public class Intersection
{
    public Road[] outgoing;
    public Road[] incoming;

    public bool isClear;
        public string name { get; private set; }
        static uint numIntersections = 0;

    public Intersection( string name = null)
    {numIntersections++;
    this.name = null != name ? name : string.Format("int{0}", numIntersections); 
        isClear = true;

        outgoing = new Road[4];
        incoming = new Road[4];
                   }

    ~Intersection()
    { numIntersections--; }

    public override string ToString()
    {
        return this.name;}
                    
    public void switchLights()
    {
        foreach (Road r in this.incoming)
            r.hasGreen = r.hasGreen ? false : true;
                printLights();
    }//end switch lights

    public void printLights()
    {
        System.Console.WriteLine("Lights for {0} are {1}, {2}, {3}, {4}\n", this.name.ToString(), 
            incoming[0].hasGreen, incoming[1].hasGreen, incoming[2 ].hasGreen, incoming[3].hasGreen);
               }

    public void block(int time)
    {
        this.isClear = false;
        Simulation.futureEvents.Add(new IntersectionClearEvent(Simulation.time + time, this));
    }
}//end class Intersection
