public class Road
{

    public static int numSwitches = 0;
    public static int numTrafficJams = 0;

         public Intersection from { get; private set; }
    public Intersection to { get; private set; }
    public int dir;
    int  length;

    public  LinearDataStructures.LinkedQueue<Vehicle> waitingVehicles;
    private bool hasGreen;
        private bool wasEmptiedThisCycle;


     public  Road(Intersection f, Intersection t, int d, int l = Simulation.ROAD_LENGTH)
    {if ( 4 <= d)
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
         to.incoming[ (this.dir +2) %4] = this;
                                 }//end constructor 

     public override string ToString()
     { return string.Format("road{0}{1}", from.name, to.name);}

     public bool     HasGreen
     {
         get { return hasGreen; }
                  set {numSwitches++;
                  if (hasGreen && !value && !wasEmptiedThisCycle) // if is being switched to red, and the queue was never emptied this cycle
                  {
                      numTrafficJams++;
                      //System.Console.WriteLine("traffic jam #{0} for road leaving {1} from {0}", numTrafficJams, (direction)this.dir, this.to.name);
                  }
                  else if (!hasGreen && value) //if the light is being turned green
                      if (0 == waitingVehicles.Size())
                          wasEmptiedThisCycle = true;
                      else
                          wasEmptiedThisCycle = false;
             hasGreen = value;
         }
     }//end HasGreen property
    
                         public bool drive()
     {
         if (this.waitingVehicles.Empty())
                      return false;

         Vehicle v = waitingVehicles.pop();
         int  d = v.getDirection( (int)  this.dir);

         if (v.GetType().ToString() == "Ambulance")
         {//clear future intersections and reset current intesection 
             Ambulance a = (Ambulance) v;
             for ( int i = 0 ; i < Simulation.NUM_PREEMPTIVE_GREENS ; i++ )
           if ( a.pathDirs.Count > i )
             to.outgoing[a.getFuturePath(i)].resetLights();
         }//end if vehicle is an ambulance

                  if (0 <= d && d < 4)
         {
             Road r = this.to.outgoing[d];
             //System.Console.WriteLine("coming from {0}, to intersection {1}, and turning {2}, to leave {3} to {4}", 
             //(direction) ((this.dir+2) %4), this.to.name, (direction) d, (direction) r.dir, r.to.name);
             Simulation.futureEvents.Add(new EndOfRoadEvent(Simulation.time + r.length, r, v));
             this.to.block(Simulation.CLEARING_TIME);
             return true;
         }
         return false;
                              }

                         public void resetLights()
                         {
                             if (!hasGreen)
                                 to.switchLights();
                                 Simulation.futureEvents.removeItems(new SwitchLightEvent(0, this.to));
                                 Simulation.futureEvents.Add(new SwitchLightEvent(Simulation.time + Simulation.LIGHT_DURATION, this.to));
                                                      }//end reset lights

     public bool isEmpty()
     { return waitingVehicles.Empty(); }
}//end class Road

