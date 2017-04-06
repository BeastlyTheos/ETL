using System;
using System.Collections.Generic;
using LinearDataStructures;
public enum direction { north, east, south, west};

public class Simulation
{
    public static bool debug = true;
    const int TIME_SCALE = 0;
    const int MAX_TIME = 2000;
    const int CARS_PER_ROAD = 10;
    const int LIGHT_DURATION = 20;
    public const int YELLOW_LIGHT_DURATION = 2;
    public const int CLEARING_TIME = 1;
    public const int ROAD_LENGTH = 20;
    const uint GRID_WIDTH = 2;
    const uint GRID_HEIGHT = 2;

    public static int time;
    public  static Random rand = new Random(); 
    public static LinkedListPriorityQueue<Event> futureEvents;

        public static void Main()
    {
        Console.Title = "emergency";
                Intersection[,] intersections = new Intersection[GRID_WIDTH, GRID_HEIGHT];
                futureEvents = new LinkedListPriorityQueue<Event>();
                Event e;
                SwitchLightEvent sle;
            EndOfRoadEvent eore;
            double r = 0.0;
                        bool hasDriven = r == 0 ? true : false;

                        futureEvents.Add(new ResetStatisticsEvent(0));

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

                i.incoming[(int)direction.north].HasGreen = false;
                                                    i.incoming[ (int)    direction.east].HasGreen = true;
                                                    i.incoming[(int)direction.south].HasGreen = false;
                                                    i.incoming[(int)direction.west].HasGreen = true;

                                                    int light  = rand.Next(-1 * LIGHT_DURATION, LIGHT_DURATION);
                                                    futureEvents.Add(new SwitchLightEvent( light, i));    

                for (int j = 0; j < 4 ; j++)
                                        for ( int k = 0 ; k < CARS_PER_ROAD ; k++ )
                                        futureEvents.Add(new EndOfRoadEvent(0, i.incoming[j], new Vehicle()));
                                                                                                                        }//end of connecting intersection x,y northwards and eastwards

        
                                    while ( time < MAX_TIME && !futureEvents.Empty())
                            {
                                Console.WriteLine("num events = " + futureEvents.Size());
            e = futureEvents.pop();
                        //if (debug && e.time > time ) System.Threading.Thread.Sleep( (int) ((e.time - time) * TIME_SCALE));
            time = e.time;
            Console.Write("{0}: {1}. ", e.time, e.GetType().ToString());

            switch (e.GetType().ToString())
            {
                case "SwitchLightEvent":
                    Console.WriteLine();
                    sle = (SwitchLightEvent) e;
                    sle.intersection.switchLights();
                    sle.intersection.block( YELLOW_LIGHT_DURATION);
                    futureEvents.Add( new SwitchLightEvent( time+LIGHT_DURATION, sle.intersection));
                    Console.Read();
                    break;
                                    case "EndOfRoadEvent":
                                         eore = (EndOfRoadEvent)e;
                    eore.road.waitingVehicles.Add( eore.vehicle);
                    Console.WriteLine("road is {0} clear, with {1} cars waiting, and a {2} light",  eore.road.to.isClear()? "": "not", eore.road.waitingVehicles.Size(), eore.road.HasGreen? "green": "red");
                    if (eore.road.to.isClear() && eore.road.HasGreen)
                    {
                                                eore.road.drive();
                    }
                    else Console.WriteLine("just waiting at {0}", eore.road);
                                                                                    break;
                case "IntersectionClearEvent":
                                                                                IntersectionClearEvent ICE = (IntersectionClearEvent)e;
                                                            Console.WriteLine("clearing "+ICE.intersection.name);
                                                            ICE.intersection.unblock();
                                                            if (ICE.intersection.isClear())
                                                            {//cycle through incoming roads, looking for a car to drive
                                                                hasDriven = false;
                                                                for (int i = 0; i < 4 && !hasDriven; i++)
                                     
                                                                    if (ICE.intersection.incoming[i].HasGreen && !ICE.intersection.incoming[i].waitingVehicles.Empty())
                                                                    {
                                                                                                                                                ICE.intersection.incoming[i].drive();
                                                                        hasDriven = true;
                                                                    }
                                                                if (hasDriven)
                                                                    ICE.intersection.block(CLEARING_TIME);
                                                            }//end if road is clear
                    //else there is another blocking event (either a drive or switch light)
                                                            break;
                case "ResetStatisticsEvent":
                                                            Road.numTrafficJams = Road.numSwitches = 0;
                                                            break;
                                                                            default:
                    throw new  System.NotImplementedException("no implementation for event "+e.GetType());
                                }//end switch 
        }//end while there is a future event

                Console.WriteLine("There were {0} jams out of a total of {1} switches, for a rate of {2}%", Road.numTrafficJams, Road.numSwitches, (double)Road.numTrafficJams / Road.numSwitches);
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
