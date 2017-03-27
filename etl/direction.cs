/*cois 2020h assignment 2 part b
 * Theodore Cooke 0560425
 * 
 * Direction class
 * holds queue for carrs waiting to drive through the intersection from a specific direction
 * counts number of cars and their total wait times
 */

using LinearDataStructures;

public class Direction
{
    static int numCars;
    static double totalWaitTimes;
    int numCar;
    double totalWaitTime;
    string name;
    LinkedQueue<Event> Arrivals;
    double meanArrivalTime;
    double lightDuration;

    //static constructor
    static Direction()
    {
        numCars = 0;
        totalWaitTimes = 0;
    }

    //constructor
    public Direction(string n)
    {
        this.name = n;
        this.Arrivals = new LinkedQueue<Event>();
        this.totalWaitTime = 0;
        numCar = 0;
    }//end of constructor

    public static int NumCars
    { get { return Direction.numCars; } }

    public static double TotalWaitTimes
    { get { return Direction.totalWaitTimes; } }

    public int NumCar
    { get { return this.numCar; } }

    public double TotalWaitTime
    { get { return totalWaitTime; } }

    public string Name
    { get { return name; } }

    public double MeanArrivalTime
    {
        get { return meanArrivalTime; }
        set
        {
            if (0 == value)
                throw new System.Exception("Attempted to set a mean arrival time of 0 for direction " + this.name);
            
meanArrivalTime = value;
        }
    }

    public double LightDuration
    {
        get { return lightDuration; }
        set
        {
            if (0 == value)
                throw new System.Exception("Attempted to set a light duration of 0 for direction " + this.name);

lightDuration = value;
        }
    }

    public void ArriveCar(Event e)
    { Arrivals.Add(e); }

    public void DriveThrough(double t)
    {
        Event e = this.Arrivals.Front(); this.Arrivals.Remove();
            
Direction.numCars++;
        this.numCar++;
        Direction.totalWaitTimes += t - e.Time;
        this.totalWaitTime += t -e.Time;
            }

    public int numWaiting()
    { return Arrivals.Size(); }

    public void Reset()
    {
        Direction.numCars -= this.numCar;
        this.numCar = 0;
        Direction.totalWaitTimes -= this.totalWaitTime;
        if (-0.1 < Direction.totalWaitTimes && Direction.totalWaitTimes < 0.1)
            Direction.totalWaitTimes = 0; //in case floating point subtraction isn't exact
        this.totalWaitTime = 0;
        Arrivals.MakeEmpty();
    }
}//end of class direction
