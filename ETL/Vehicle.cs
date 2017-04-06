using System;
using System.Collections.Generic;

public class Vehicle
{
    public virtual int  getDirection( int currentDirection)
    {
        double  r = Simulation.rand.NextDouble();
        
        if ( 0.7 > r)
            return currentDirection %4;
        else if (0.85 > r)
            return (currentDirection + 1) % 4; //turn right
        else
            return (currentDirection + 3) % 4; // turn left
                                    }//end getDirection
}//end Vehicle

public class Ambulance : Vehicle
{
    private List<int> pathDirs;

        public void createPath( int totalRoads)
    {
                       this.pathDirs = new List<int>();

               pathDirs.Add( 0); //first direction is always north (for algorithmic convenience)
                                    while (pathDirs.Count < totalRoads)
                                                                                                                    pathDirs.Add( base.getDirection( pathDirs[pathDirs.Count-1]));
                                            }//end create path

    public override int getDirection(int currentDirection)
        {
           int  d  = pathDirs[0];
            pathDirs.RemoveAt(0);
            return d;
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
    }//end class ambulance
