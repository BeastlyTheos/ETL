﻿using System;
using LinearDataStructures;
public enum EventType { CarArriving, LightChange, IntersectionClear };

public class Simulation
{
    public static bool debug = true;
const       int  TIME_SCALE = 100;
        const int LIGHT_DURATION = 30;
        const uint GRID_WIDTH = 2;
    const uint GRID_HEIGHT = 2;

    public static int time;
    static Random rand = new Random(); 
    public static LinkedListPriorityQueue<Event> futureEvents;


    public static void Main()
    {
                Intersection[,] intersections = new Intersection[GRID_WIDTH, GRID_HEIGHT];
                futureEvents = new LinkedListPriorityQueue<Event>();
                Event e;
                SwitchLightEvent sle;

        /*initialise intersections*/
        for (int x = 0; x < GRID_WIDTH; x++)
            for (int y = 0; y < GRID_HEIGHT; y++)
                intersections[x, y] = new Intersection();

        /*connect intersections from east to west*/
        for (int x = 0; x < GRID_WIDTH; x++)
            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                Intersection i = intersections[x, y];
                                                new Road(intersections[x, y], intersections[x, (y + 1) % GRID_HEIGHT], Direction.northwards);//north
                new Road(intersections[x, y], intersections[(x + 1) % GRID_WIDTH, y], Direction.eastwards); //east
                            new Road( intersections[x, (y +1) % GRID_HEIGHT], i, Direction.southwards);//south
            new Road( intersections[(x +1) % GRID_WIDTH, y], i, Direction.westwards); //west
            }///end  creating roads

        for (int x = 0; x < GRID_WIDTH; x++)
            for (int y = 0;  y < GRID_HEIGHT; y++)
            {            //initialise lights
                Intersection i = intersections[x, y];
                i.fromNorth.hasGreen = i.fromSouth.hasGreen = false;
                i.fromEast.hasGreen = i.fromWest.hasGreen = true;

                futureEvents.Add( new SwitchLightEvent( rand.Next( 0, 2 * LIGHT_DURATION), i));
            }//end of connecting intersection x,y northwards and eastwards

                while (!futureEvents.Empty())
        {
            e = futureEvents.pop();
            int r;
            //if (debug) System.Threading.Thread.Sleep( (int) ((e.time - time) * TIME_SCALE));
            time = e.time; 
                    //Console.WriteLine("actuating {0} at {1}", e.ToString(), time);

            switch (e.GetType().ToString())
            {
                case "SwitchLightEvent":
                    sle = (SwitchLightEvent) e;
                    sle.intersection.switchLights();
                    futureEvents.Add( new SwitchLightEvent( time+LIGHT_DURATION, sle.intersection));
                    break; //end SwitchLight
                case "EndOfRoadEvent":
                    /*
                    EndOfRoadEvent EOR = (EndOfRoadEvent)e;
                    if (EOR.road.to.isClear && EOR.road.hasGreen)
                    {EOR.road.pop()
                        EOR.road.to.isClear = false;
                        futureEvents.Add(new IntersectionClearEvent( EOR.road.to,  LIGHT_DURATION));
                        r = rand.NextDouble();
                        if (0.7 > r)
                                                    EOR.road.to.driveThrough(EOR.road.dir);
                        else if ( 0.85 > r)
                        EOR.road.to.driveThrough(EOR.road.dir.turnLeft());
                        else
                            EOR.road.to.driveThrough(EOR.road.dir.turnRight());
                    }//end if intersection is clear
                    else
                        EOR.road.addWaitingCar();
                    */
                    break;
                    case "IntersectionClearEvent":
                    /*IntersectionClearEvent ICE = (IntersectionClearEvent)e;
                    ICE.intersection.isClear = true;
                    ICE.intersection.pullCar();
                    for ( int i = 0 ; i < ICE.intersection.arrive.Count ; i++)
                        if ( ICE.intersection.arrive[i].hasGreen)
                        {ICE.intersection.arrive[i].pop()
                    */
                    break;
                default:
                    throw new  System.NotImplementedException("no implementation for event "+e.GetType());
                    break;
            }//end switch 
        }//end while there is a future event

        //printGrid(intersections);
        Console.Read();
    }//end of Main

        static void printGrid(Intersection[,] i)
    {//for( int x = 0; x < GRID_WIDTH ; x++)
        //for(int y = 0 ; y < GRID_HEIGHT ; y++)
        foreach (Intersection h in i)
            Console.WriteLine(h.ToString());

    }//end printGrid
}//end main
