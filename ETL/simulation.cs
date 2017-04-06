using System;
using System.Collections.Generic;
using LinearDataStructures;
public enum direction { north, east, south, west };

public class Simulation
{
    public static bool debug = true;
    const int TIME_SCALE = 0;

    const int MAX_TIME = 20000000; //maximum simulated time
    const int MAX_TRIPS = 100; //max ambulance trips
    const int CARS_PER_ROAD = 3;
    public const int LIGHT_DURATION = 20;
    public const int NUM_PREEMPTIVE_GREENS = 3;
    public const int YELLOW_LIGHT_DURATION = 2;
    public const int CLEARING_TIME = 1;
    public const int ROAD_LENGTH = 20;
    const uint GRID_WIDTH = 6;
    const uint GRID_HEIGHT = 6;

    public static int time;
    public static Random rand = new Random();
    public static LinkedListPriorityQueue<Event> futureEvents;

    public static void Main()
    {
        Console.Title = "emergency";
        Intersection[,] intersections = new Intersection[GRID_WIDTH, GRID_HEIGHT];
        futureEvents = new LinkedListPriorityQueue<Event>();
        Event e;
        SwitchLightEvent sle;
        EndOfRoadEvent eore;
        bool hasDriven;

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

                new Road(i, intersections[x, (y + 1) % GRID_HEIGHT], (int)direction.north);//north
                new Road(i, intersections[(x + 1) % GRID_WIDTH, y], (int)direction.east); //east
                new Road(intersections[x, (y + 1) % GRID_HEIGHT], i, (int)direction.south);//south
                new Road(intersections[(x + 1) % GRID_WIDTH, y], i, (int)direction.west); //west
            }///end  creating roads

        /*initialise lights*/
        for (int x = 0; x < GRID_WIDTH; x++)
            for (int y = 0; y < GRID_HEIGHT; y++)
            {//initialise lights
                Intersection i = intersections[x, y];

                i.incoming[(int)direction.north].HasGreen = false;
                i.incoming[(int)direction.east].HasGreen = true;
                i.incoming[(int)direction.south].HasGreen = false;
                i.incoming[(int)direction.west].HasGreen = true;

                /*initialise light switching*/
                int light = rand.Next(-1 * LIGHT_DURATION, LIGHT_DURATION);
                futureEvents.Add(new SwitchLightEvent(light, i));

                /*place cars on road*/
                for (int j = 0; j < 4; j++)
                    for (int k = 0; k < CARS_PER_ROAD; k++)
                        futureEvents.Add(new EndOfRoadEvent(0, i.incoming[j], new Vehicle()));
            }//end of connecting intersection x,y northwards and eastwards

        futureEvents.Add(new GetDestinationEvent(0, new Ambulance()));

        while (time < MAX_TIME && !futureEvents.Empty()) //while there is an event to simulate
        {
            e = futureEvents.pop();
            time = e.time;

            switch (e.GetType().ToString())
            {
                case "SwitchLightEvent":
                    sle = (SwitchLightEvent)e;
                    sle.intersection.switchLights();
                    sle.intersection.block(YELLOW_LIGHT_DURATION); //simulate the light being yellow, and the brief time that both are red
                    futureEvents.Add(new SwitchLightEvent(time + LIGHT_DURATION, sle.intersection)); //create event to switch again
                    break;

                case "EndOfRoadEvent":
                    eore = (EndOfRoadEvent)e;
                    eore.road.waitingVehicles.Add(eore.vehicle); //place in queue at edge of intersection

                    /*check if the vehicle can drive through */
                    if (eore.road.to.isClear() && eore.road.HasGreen)
                        eore.road.drive();
                    break;

                case "IntersectionClearEvent":
                    IntersectionClearEvent ICE = (IntersectionClearEvent)e;
                    ICE.intersection.unblock();

                    /* if the intersection is now clear, then attempt to drive a car through */
                    if (ICE.intersection.isClear())
                    {//cycle through incoming roads, looking for a car to drive
                        hasDriven = false;
                        for (int i = 0; i < 4 && !hasDriven; i++)
                            if (ICE.intersection.incoming[i].HasGreen && !ICE.intersection.incoming[i].waitingVehicles.Empty()) //if the road is green, and there is a vehicle
                            {
                                ICE.intersection.incoming[i].drive();
                                hasDriven = true;
                            }

                        if (hasDriven)
                            ICE.intersection.block(CLEARING_TIME);
                    }//end if road is clear
                    //else there is another blocking event (either a drive or switch light)
                    break;

                case "ResetStatisticsEvent": //reset statistics to clear away any fluff caused by initialisation
                    Road.numTrafficJams = Road.numSwitches = Ambulance.totalTime = Ambulance.numTrips = 0;
                    break;

                case "GetDestinationEvent": //sets a path for an ambulance to follow, then drives it
                    GetDestinationEvent gde = (GetDestinationEvent)e;

                    gde.ambulance.createPath(2);
                    gde.ambulance.startTime = time;

                    Road startingRoad = intersections[0, 0].outgoing[(int)direction.north]; //hardcode starting road 

                    futureEvents.Add(new EndOfRoadEvent(time, startingRoad, gde.ambulance)); //put it on the road

                    if (Ambulance.numTrips >= MAX_TRIPS) //if enough trips have been taken
                        futureEvents.MakeEmpty(); //trick the event loop to end
                    break;

                default:
                    throw new System.NotImplementedException("no implementation for event " + e.GetType());
            }//end switch 
        }//end while there is a future event

        Console.WriteLine("There were {0} jams out of a total of {1} switches, for a rate of {2}%", Road.numTrafficJams, Road.numSwitches, (double)Road.numTrafficJams / Road.numSwitches);
        Console.WriteLine("The ambulance took {0} trips, with an average time of {1}", Ambulance.numTrips, (double)Ambulance.totalTime / Ambulance.numTrips);
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
                    Console.WriteLine("{0}, {1}, is {3} {2}", x, y, i[x, y].ToString(), ++counter);
    }//end printGrid

    static void printRoads(Intersection[,] i)
    {
        for (int x = 0; x < GRID_WIDTH; x++)
            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                Intersection j = i[x, y];
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
