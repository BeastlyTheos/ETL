using System.Collections.Generic;

public class Intersection
{
    public  List<Road> arrive;
    public  List<Road> leave;
    public int x { get; private set; }
    public int y { get; private set; }
    public bool isClear { get; private set; }
        public string name { get; private set; }
            static uint numIntersections = 0;

    public Intersection( int x, int y, string name = null)
    {
        arrive = new List<Road>(4);
        leave = new List<Road>(4);
        isClear = true;
        numIntersections++;

        this.x = x;
        this.y = y;
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
    System.Random rand = new System.Random();
    public void push(Road fromRoad)
    {
        if (isClear)
        {
            this.leave[ rand.Next( 0,4)].push( );
            isClear = false;
                        Simulation.futureEvents.Add(new IntersectionClearEvent(Simulation.time + 2, this));
        }
        else
            fromRoad.addWaitingCar();
    }//end driveThrough

    public void clearIntersection()
    { isClear = true; 
        for( int i = 0; i < arrive.Count ; i++)
        if( arrive[i].hasGreen && ! arrive[i].isEmpty())
        {arrive[i].pop();
            leave[ rand.Next( 0, 3)].push();
                        Simulation.futureEvents.Add(new IntersectionClearEvent(Simulation.time + 2, this));
break;
        }//end if has green
        }//end clear
}//end class Intersection
