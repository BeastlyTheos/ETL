using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Intersection
{
    List<Road> arrive;
    List<Road> leave;
    bool isClear;    

    public string name { get; private set; }
    
        static uint numIntersections = 0;

    public Intersection( string name = null)
    {
        arrive = new List<Road>(4);
        leave = new List<Road>(4);
        isClear = true;
        numIntersections++;

        this.name = null!= name ? name : string.Format("int{0}", numIntersections);
           }

    ~Intersection()
    { numIntersections--; }

    public override string ToString()
    {
        string s = this.name + " connects to: ";
        foreach (Road r in leave)
            s += r.to.name + ", ";
        s += "\nconnects from: ";
                foreach (Road r in arrive)
                    s += r.from.name + ", ";
                return s;
            }//end toString

    public void addIncomingRoad(Road r)
    {
        arrive.Add(r);
                    }

    public void addOutgoingRoad(Road r)
    { leave.Add(r); }

    public void push(Road fromRoad)
    {
        if (isClear)
        {
            fromRoad.leadsTo.push();
            isClear = false;
            Simulation.futureEvents.Add(new IntersectionClearEvent(Simulation.time + 2, this));
        }
        else
            fromRoad.addWaitingCar();
    }//end driveThrough

    public void clearIntersection()
    { isClear = true; }
}//end class Intersection
