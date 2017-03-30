using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Intersection
{
    List<Road> arrive;
    List<Road> leave;

    public Intersection()
    {
        arrive = new List<Road>(4);
        leave = new List<Road>(4);
    }

    public void addIncomingRoad(Road r)
    { arrive.Add(r); }

    public void addOutgoingRoad(Road r)
    { leave.Add(r); }
}//end class Intersection
