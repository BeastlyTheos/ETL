public class Road
{
         public Intersection from { get; private set; }
    public Intersection to { get; private set; }
    public int dir;
    int  length;

    public bool hasGreen;
    public  LinearDataStructures.LinkedQueue<Vehicle> waitingVehicles;

     public  Road(Intersection f, Intersection t, int d, int l = Simulation.ROAD_LENGTH)
    {if ( 4 <= d)
        throw new System.ArgumentException("invalid direction");

                 from = f; //connect road to intersection 
        to = t; //connect road to intersection
                        this.dir = d;
                        this.length = l;

                        waitingVehicles = new LinearDataStructures.LinkedQueue<Vehicle>();
         hasGreen = true;

         //connect intersections to road
         from.outgoing[this.dir] = this;
         to.incoming[ (this.dir +2) %4] = this;
                                 }//end constructor 

     public override string ToString()
     { return string.Format("road{0}{1}", from.name, to.name);}

     public bool drive()
     {
         if (this.waitingVehicles.Empty())
                      return false;

         Vehicle v = waitingVehicles.pop();
         int  d = v.getDirection( (int)  this.dir);
                  Road r = this.to.outgoing[ d];
         //System.Console.WriteLine("coming from {0}, to intersection {1}, and turning {2}, to leave {3} to {4}", 
             //(direction) ((this.dir+2) %4), this.to.name, (direction) d, (direction) r.dir, r.to.name);
         Simulation.futureEvents.Add( new EndOfRoadEvent( Simulation.time + r.length, r, v));
         this.to.block(Simulation.CLEARING_TIME);
         return true;
     }

     public bool isEmpty()
     { return waitingVehicles.Empty(); }
}//end class Road

