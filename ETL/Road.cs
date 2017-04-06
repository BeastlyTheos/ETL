public class Road
{
    public static int numSwitches = 0; //total light switches for all roads
    public static int numTrafficJams = 0; //total number of times a light turned red on a road that was never able to empty itself

    public Intersection from { get; private set; } //intersection the road comes from
    public Intersection to { get; private set; } //the intersection the road goes to
    public int dir; //direction of the road
    int length;

    public LinearDataStructures.LinkedQueue<Vehicle> waitingVehicles;
    private bool hasGreen;
    private bool wasEmptiedThisCycle; //if the road queue was emptied in the given green light

    public Road(Intersection f, Intersection t, int d, int l = Simulation.ROAD_LENGTH)
    {
        if (4 <= d)
            throw new System.ArgumentException("invalid direction");

        from = f; //connect road to intersection 
        to = t; //connect road to intersection
        this.dir = d;
        this.length = l;

        waitingVehicles = new LinearDataStructures.LinkedQueue<Vehicle>();
        hasGreen = false;
        wasEmptiedThisCycle = true;

        //connect intersections to road
        from.outgoing[this.dir] = this;
        to.incoming[(this.dir + 2) % 4] = this;
    }//end constructor 

    public override string ToString()
    { return string.Format("road{0}{1}", from.name, to.name); }

    public bool HasGreen
    {
        get { return hasGreen; }
        set
        {
            if (hasGreen && !value) // if is being switched to red
            {
                numSwitches++;
                if (!wasEmptiedThisCycle)//the queue was never emptied this cycle
                    numTrafficJams++;
            }
            else if (!hasGreen && value) //if the light is being turned green
                if (waitingVehicles.Empty())
                    wasEmptiedThisCycle = true;
                else
                    wasEmptiedThisCycle = false;
            hasGreen = value;
        }
    }//end HasGreen property

    /* drive function
     * pulls a car out of the waiting queue, queries it for next direction, then sends it down the road */
    public bool drive()
    {
        if (this.waitingVehicles.Empty())
            return false;

        Vehicle v = waitingVehicles.pop();
        int d = v.getDirection((int)this.dir);

        /* handle preemptive light switching for ambulances */
        if (v.GetType().ToString() == "Ambulance")
        {//clear future intersections and reset current intesection 
            Ambulance a = (Ambulance)v;

            for (int i = 0; i < Simulation.NUM_PREEMPTIVE_GREENS; i++)
                if (a.pathDirs.Count > i)
                    to.outgoing[a.getFuturePath(i)].resetLights();
        }//end if vehicle is an ambulance

        /* drive the car through */
        if (0 <= d && d < 4) //ambulances will return -1 if they are done
        {
            Road r = this.to.outgoing[d]; //get the road to drive onto
            Simulation.futureEvents.Add(new EndOfRoadEvent(Simulation.time + r.length, r, v)); //send it down that road
            this.to.block(Simulation.CLEARING_TIME); //block the intersection briefly
            return true;
        }
        return false;
    }

    /* function to reset lights
     * insures the lights are currently green and will stay green for the next LIGHT_DURATION seconds
     * used to preemptively open the intersection for an incoming ambulance */
    public void resetLights()
    {
        if (!hasGreen)
            to.switchLights();

        /* remove whatever light switching event the queue already has, and replace it with one in LIGHT_DURATION seconds */
        Simulation.futureEvents.removeItems(new SwitchLightEvent(0, this.to));
        Simulation.futureEvents.Add(new SwitchLightEvent(Simulation.time + Simulation.LIGHT_DURATION, this.to));
    }//end reset lights

    public bool isEmpty()
    { return waitingVehicles.Empty(); }
}//end class Road
