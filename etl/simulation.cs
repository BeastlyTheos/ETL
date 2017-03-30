using System;
using LinearDataStructures;
public enum EventType { CarArriving, LightChange, IntersectionClear };

public class Simulation
{
    static Random randNum = new Random();
    public static uint time;
    public static LinkedListPriorityQueue<Event> futureEvents;

    const uint GRID_WIDTH = 2;
    const uint GRID_HEIGHT = 2;

    public static void Main()
    {

        Intersection[,] intersections = new Intersection[GRID_WIDTH, GRID_HEIGHT];

        /*initialise intersections*/
        for (int x = 0; x < GRID_WIDTH; x++)
            for (int y = 0; y < GRID_HEIGHT; y++)
                intersections[x, y] = new Intersection();

        /*connect intersections from east to west*/
        for (int x = 0; x < GRID_WIDTH; x++)
            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                new Road(intersections[x, y], intersections[(x + 1) % GRID_WIDTH, y]);
                new Road(intersections[x, y], intersections[x, (y + 1) % GRID_HEIGHT]);
            }//end of connecting intersection x,y northwards and eastwards

        Event e;
        while (!futureEvents.Empty())
        {
            e = futureEvents.Front();
            futureEvents.Remove();
            time = e.Time;
            e.actuate();
        }

        //printGrid(intersections);
        Console.WriteLine("hello");
        Console.Read();
    }//end of testing main

    static void printGrid(Intersection[,] i)
    {//for( int x = 0; x < GRID_WIDTH ; x++)
        //for(int y = 0 ; y < GRID_HEIGHT ; y++)
        foreach (Intersection h in i)
            Console.WriteLine(h.ToString());

    }//end printGrid
}//end main
