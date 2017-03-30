using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Intersection
{
    List<Road> arrive;
    List<Road> leave;
    
    public string name { get; private set; }
    
        static uint numIntersections = 0;

    public Intersection( string name = null)
    {
        arrive = new List<Road>(4);
        leave = new List<Road>(4);
        numIntersections++;

        this.name = null!= name ? name : string.Format("int{0}", numIntersections);
           }

    ~Intersection()
    { numIntersections--; }

    public override string ToString()
    {
        string s = this.name + " connects to: ";
        foreach (Road r in leave)
            s += r.To.name + ", ";
        s += "\nconnects from: ";
                foreach (Road r in arrive)
                    s += r.From.name + ", ";
                return s;
            }//end toString

    public void addIncomingRoad(Road r)
    { arrive.Add(r); }

    public void addOutgoingRoad(Road r)
    { leave.Add(r); }
}//end class Intersection
