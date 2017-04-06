using System;
using System.Collections.Generic;

public class Vehicle
{
    public virtual int getDirection(int currentDirection)
    {
        double r = Simulation.rand.NextDouble();

        if (0.7 > r)
            return currentDirection % 4; //go straight
        else if (0.85 > r)
            return (currentDirection + 1) % 4; //turn right
        else
            return (currentDirection + 3) % 4; // turn left
    }//end getDirection
}//end Vehicle

public class Ambulance : Vehicle
{
    public static int numTrips = 0;// number of trips that all ambulances have taken
    public static int totalTime = 0; //total of all ambulance trip times

    public int startTime; //starting time of a trip
    public List<int> pathDirs; //directions for the ambulances path 

    public void createPath(int totalRoads)
    {
        this.pathDirs = new List<int>();

        pathDirs.Add(0); //first direction is always north (for algorithmic convenience)

        while (pathDirs.Count < totalRoads) //while there are not enough dirs 
            pathDirs.Add(base.getDirection(pathDirs[pathDirs.Count - 1])); //use base getDirection to create an arbitrary route
    }//end create path

    public override int getDirection(int currentDirection)
    {
        /*check if ambulance has arrived*/
        if (0 == pathDirs.Count)
        {

            /* record stats for this trip */
            numTrips++;
            totalTime += Simulation.time - startTime;

            /* create event to start next trip */
            Simulation.futureEvents.Add(new GetDestinationEvent(Simulation.time, this));
            return -1;
        }
        else
        {
            int d = pathDirs[0]; //get next direction
            pathDirs.RemoveAt(0); //remove it from the list
            return d;
        }
    }

        public int getFuturePath(int i)
    { return pathDirs[i]; }

    public override string ToString()
    {
        System.Console.WriteLine("getting string");
        string s = "path dirs are: ";
        foreach (int i in pathDirs)
            s += i.ToString() + ", ";
        return s;
    }
}//e2nd class ambulance
