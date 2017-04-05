using System;
using LinearDataStructures;
public enum direction { north, east, south, west};

public class Simulation
{
    public static bool debug = true;
const       int  TIME_SCALE = 1;
        const int LIGHT_DURATION = 30;
        public const int YELLOW_LIGHT_DURATION = 5;
        public  const int CLEARING_TIME = 2;
        public const int ROAD_LENGTH = 4;
        const uint GRID_WIDTH = 1;
        const uint GRID_HEIGHT = 1;

    public static int time;
    public  static Random rand = new Random(); 
    public static LinkedListPriorityQueue<Event> futureEvents;

        public static void Main()
    {
                Intersection[,] intersections = new Intersection[GRID_WIDTH, GRID_HEIGHT];
                futureEvents = new LinkedListPriorityQueue<Event>();
                Event e;
                SwitchLightEvent sle;
            EndOfRoadEvent eore;
            double r = 0.0;
                        bool hasDriven = r == 0 ? true : false;

        /*initialise intersections*/
        for (int x = 0; x < GRID_WIDTH; x++)
            for (int y = 0; y < GRID_HEIGHT; y++)
                intersections[x, y] = new Intersection();

        /*connect intersections from east to west*/
        for (int x = 0; x < GRID_WIDTH; x++)
            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                Intersection i = intersections[x, y];

                                                new Road( i, intersections[x, (y + 1) % GRID_HEIGHT], (int) direction.north);//north
                new Road( i, intersections[(x + 1) % GRID_WIDTH, y], (int) direction.east); //east
                                            new Road( intersections[x, (y +1) % GRID_HEIGHT], i, (int) direction.south);//south
            new Road( intersections[(x +1) % GRID_WIDTH, y], i, (int) direction.west); //west
            }///end  creating roads
        
        for (int x = 0; x < GRID_WIDTH; x++)
            for (int y = 0;  y < GRID_HEIGHT; y++)
            {//initialise lights
                Intersection i = intersections[x, y];
                Console.WriteLine("initialising lights for intersection {0}", i.name);
                i.incoming[(int)direction.north].hasGreen = false;
                i.incoming[(int)direction.south].hasGreen = false;
                                    i.incoming[ (int)    direction.east].hasGreen  = true;
                                    i.incoming[ (int)  direction.west].hasGreen = true;
                
                futureEvents.Add( new SwitchLightEvent( rand.Next( 0, 2 * LIGHT_DURATION), i));
            }//end of connecting intersection x,y northwards and eastwards

        Console.WriteLine("initialised intersections");
        //futureEvents.Add(new EndOfRoadEvent(1, intersections[0, 0].fromWest));

                while (!futureEvents.Empty())
        {
            e = futureEvents.pop();
                        if (debug) System.Threading.Thread.Sleep( (int) ((e.time - time) * TIME_SCALE));
            time = e.time;
                        Console.WriteLine("actuating {0} at {1}", e.GetType().ToString(), time);

            switch (e.GetType().ToString())
            {
                case "SwitchLightEvent":
                    sle = (SwitchLightEvent) e;
                    sle.intersection.switchLights();
                    sle.intersection.block( YELLOW_LIGHT_DURATION);
                    futureEvents.Add( new SwitchLightEvent( time+LIGHT_DURATION, sle.intersection));
                    break;
                                    case "EndOfRoadEvent":
                                         eore = (EndOfRoadEvent)e;
                    eore.road.waitingVehicles.Add( eore.vehicle);

                    if (eore.road.to.isClear && eore.road.hasGreen)
                        eore.road.drive();
                                                                                    break;
                    case "IntersectionClearEvent":
                                        /*                    IntersectionClearEvent ICE = (IntersectionClearEvent)e;
                                                            ICE.intersection.isClear = true;

                                                            //cycle through incoming roads, looking for a car to drive
                                                            hasDriven = false;
                                                            foreach ( Road r in sle.intersection.incoming )
                                                            {if ( r.hasGreen )
                                                            {                        r.pop()
                                                                futureEvents.Add(new EndOfRoadEvent(time, r.to.outgoing[ r.dir[);
                                                                hasDriven = true;
                                                            }
                                                            if ( hasDriven )
                                                                    futureEvents.Add( new IntersectionClearEvent( time + CLEARING_TIME, sle.intersection);
                                                            brea
                                                            for ( int i = 0 ; i < ICE.intersection.arrive.Count ; i++)
                                                                if ( ICE.intersection.arrive[i].hasGreen)
                                                                {ICE.intersection.arrive[i].pop()
                                                            */
                                        break;
                default:
                    throw new  System.NotImplementedException("no implementation for event "+e.GetType());
                                }//end switch 
        }//end while there is a future event

                Console.WriteLine("clearances are {0}, {1}, and {2}", intersections[0, 0].isClear, intersections[1, 0].name, intersections[2, 0].name);
                Console.WriteLine("done");
        //printGrid(intersections);
        Console.Read();
    }//end of Main

        static void printGrid(Intersection[,] i)
        {
            int counter = 0;
            Console.WriteLine("printing {0} {1} intersections", i.GetLength(0), i.GetLength(1));
            for (int x = 0; x < GRID_WIDTH; x++)
                for (int y = 0; y < GRID_HEIGHT; y++)
                                            if (null == i)
                            Console.WriteLine("{0}, {1} is null", x, y);
            else
            Console.WriteLine("{0}, {1}, is {3} {2}",x,y, i[x,y].ToString(), ++counter);
    }//end printGrid

        static void printRoads(Intersection[,] i)
        {for ( int x = 0 ; x < GRID_WIDTH ; x++ )
            for (int y = 0; y < GRID_HEIGHT; y++)
            {Intersection j = i[ x, y];
                Console.Write("{0} connects to: ", j.name);
                for (int r = 0; r < 4; r++)
                    Console.Write("{0}, ", j.outgoing[r]);
                Console.Write("\nconnects from: ");
                for (int r = 0; r < 4; r++)
                    Console.Write("{0}, ", j.incoming[r]);
                Console.WriteLine();
            }//end printing intersection
        }//end print roads
}//end simulation
