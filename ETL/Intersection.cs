using System.Collections.Generic;

public class Intersection
{
    public  Road toNorth;
    public  Road toEast;
    public  Road toSouth;
    public Road toWest;
    public Road fromNorth;
    public Road fromEast;
    public Road fromSouth;
    public Road fromWest;
    
    public bool isClear { get; private set; }
        public string name { get; private set; }
        static uint numIntersections = 0;

    public Intersection( string name = null)
    {numIntersections++;
    this.name = null != name ? name : string.Format("int{0}", numIntersections); 
        isClear = true;

        toNorth = null;
        toEast = null;
        toSouth = null;
        toWest = null;
        fromNorth = null;
        fromEast = null;
        fromSouth = null;
        fromWest = null;
                   }

    ~Intersection()
    { numIntersections--; }

    public override string ToString()
    {
        string s = this.name + " connects to: ";
        /*foreach (Road r in leave)
            s += r.to.name + ", ";
        s += "\nconnects from: ";
                foreach (Road r in arrive)
                    s += r.from.name + ", ";
                */return s;
            }//end toString

    public void switchLights()
    {
        fromNorth.hasGreen = fromNorth.hasGreen ? false : true;
        fromEast.hasGreen = fromEast.hasGreen ? false: true;
        fromSouth.hasGreen = fromSouth.hasGreen ? false: true;
        fromWest.hasGreen = fromWest.hasGreen ? false : true;
        printLights();
    }//end switch lights

    public void printLights()
    {System.Console.WriteLine("Lights for {0} are {1], {2}, {3}, {4}\n", this.name.ToString(), fromNorth.hasGreen.ToString(), 
        fromEast.hasGreen.ToString(), 
        fromSouth.hasGreen.ToString(), fromWest.hasGreen.ToString());}

        public void push(Road fromRoad)
    {
        if (isClear)
        {
            
            isClear = false;
                        Simulation.futureEvents.Add(new IntersectionClearEvent(Simulation.time + 2, this));
        }
        else
            fromRoad.addWaitingCar();
    }//end driveThrough
}//end class Intersection
