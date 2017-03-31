using System;
using LinearDataStructures;
public enum EventType { CarArriving, LightChange, IntersectionClear };

public class Simulation
{
    static Random randNum = new Random();
    public static ulong time;
    public static LinkedListPriorityQueue<Event> futureEvents;
        const uint GRID_WIDTH = 2;
    const uint GRID_HEIGHT = 2;

    public static void Main()
    {
        futureEvents = new LinkedListPriorityQueue<Event>();
        Intersection[,] intersections = new Intersection[GRID_WIDTH, GRID_HEIGHT];

        /*initialise intersections*/
        for (int x = 0; x < GRID_WIDTH; x++)
            for (int y = 0; y < GRID_HEIGHT; y++)
                intersections[x, y] = new Intersection( x, y);

        /*connect intersections from east to west*/
        for (int x = 0; x < GRID_WIDTH; x++)
            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                new Road(intersections[x, y], intersections[(x + 1) % GRID_WIDTH, y]);
                new Road(intersections[x, y], intersections[x, (y + 1) % GRID_HEIGHT]);
            }//end of connecting intersection x,y northwards and eastwards

        futureEvents.Add(new EndOfRoadEvent(1, intersections[0, 0].leave[0]));
        Event e;
        while (!futureEvents.Empty())
        {
            e = futureEvents.pop();
            
            time = e.time;
            Console.WriteLine("actuating {0} at {1}", e.ToString(), time);
            
            switch (e.GetType().ToString())
            {
                case "EndOfRoadEvent":
                    EndOfRoadEvent EOR = (EndOfRoadEvent)e;
                      EOR.road.to.push( EOR.road);
                    
                    break;
                default:
                    e.actuate();
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
