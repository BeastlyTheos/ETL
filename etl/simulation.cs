using System;
using LinearDataStructures;
public enum EventType { CarArriving, LightChange, IntersectionClear };

public class Simulation
{
    static Random randNum = new Random();
    public  static  uint time;
    public static LinkedListPriorityQueue<Event> futureEvents;
    
    const uint  GRID_WIDTH = 1;
    const uint   GRID_HEIGHT = 1;

    public static void Main()
    {
        
        Intersection[,] intersections = new Intersection[ GRID_WIDTH, GRID_HEIGHT];

        /*initialise intersections*/
        for ( int x = 0 ; x < GRID_WIDTH ; x++ )
            for (int y = 0 ; y < GRID_HEIGHT ; y++ )
                intersections[ x, y] = new Intersection();

        /*connect intersections from east to west*/
                for ( int x = 0 ; x < GRID_WIDTH ; x++ )
            for (int y = 0 ; y < GRID_HEIGHT ; y++ )
                            {
                new Road( intersections[x,y], intersections[(x+1)%GRID_WIDTH, y]);
                                new Road( intersections[x,y], intersections[x, (y+1)%GRID_HEIGHT]);
            }//end of connecting intersection x,y northwards and eastwards

        

    }//end of testing main


    public static void xMain()
    {
        const int MAX_LIGHTCYCLES = 100;
        int numLightCycles;
        int greenLight; //greenlight=0 means greenlight for north road, greenlight =1 means greenlight for east road
        bool isIntersectionClear;
                 futureEvents  = new LinkedListPriorityQueue<Event>();
        Event e;
        CarArrivalEvent ce;
        //Direction[] directions = new Direction[] { new Direction("north"), new Direction("east") };
        string inputString;
        double inputDouble = 0;
        bool isInputInvalid;

Console.Title = "Ted's speedy traffic simulator";
Console.WriteLine("Welcome to " + Console.Title + "\n");

        do
      {//loop through test cases until user requests to stop

            //get mean arrival times
            for (int i = 0; 4/*directions.Length */> i; i++)
            {
                isInputInvalid = true;

                //get input
                do
                {
                    //Console.Write("Enter the mean inter-arrival time for cars from the {0}> ", directions[i].Name);

                    try
                    {
                        inputDouble = double.Parse(Console.ReadLine());

                        if (0 < inputDouble)
                            isInputInvalid = false;
                        else
                            Console.WriteLine("{0} prohibited. Only non-negative valuse permitted.", inputDouble);
                    }//end of try
                    catch (FormatException)
                    { Console.WriteLine("That is not a number."); }
                } while (isInputInvalid);

                //directions[i].MeanArrivalTime = inputDouble;
                            }//end of getting mean arrival times
        
            //loop through simulation for varrying light durations for the given mean arrival times
            do
          {

                //reset counters and initialise queue
                numLightCycles = 0;
                greenLight = 0;
                isIntersectionClear = true;
                time = 0;
                Simulation.futureEvents.MakeEmpty();
                
                //for each direction, get its light duration, reset its counters, then add its first event to the queue 
                for (int i = 0; 4/*directions.Length */> i; i++)
                {
                    isInputInvalid = true;

                    //get input
                    do
                    {
                        //Console.Write("Enter the light duration for cars from the {0}> ", directions[i].Name);

                        try
                        {
                            inputDouble = double.Parse(Console.ReadLine());

                            if (0 < inputDouble)
                                isInputInvalid = false;
                            else
                                Console.WriteLine("{0} is prohibited. Only positive light durations permitted.", inputDouble);
                        }//end of try
                        catch (FormatException)
                        { Console.WriteLine("That is not a number."); }
                    } while (isInputInvalid);

                    //directions[i].LightDuration = inputDouble;
                                        //directions[i].Reset();
                    //Simulation.futureEvents.Add(new CarArrivalEvent(EventType.CarArriving, (uint) RandomExponential(directions[i].MeanArrivalTime), new roa
                }//end of getting and resetting  light durations

        //add first light change
                //Simulation.futureEvents.Add(new Event(EventType.LightChange, time + directions[0].LightDuration));

//run simulation for 100 lightcycles
                while (MAX_LIGHTCYCLES > numLightCycles)
                {                    
                   e = Simulation.futureEvents.Front(); Simulation.futureEvents.Remove(); //pop and remove first top event
                                        time = (uint)  e.Time; //increment time to current event
                    Console.Write((int)time + ": "); //write time of current event to prepare for listing of events at this time

                    //process the event
                    EventType u = EventType.IntersectionClear;
                    switch (u)
                    {
                        case EventType.CarArriving:
                            ce = e as CarArrivalEvent; //cast the event as a CarArrivalEvent to gain access to the direction it's on
                            //directions[ce.Dir].ArriveCar(e); //add car to the line at the intersection
                            //Console.Write("car arrives from {0}> ", directions[ce.Dir].Name);

                            //events.Add(new CarArrivalEvent(EventType.CarArriving, time + RandomExponential(directions[ce.Dir].MeanArrivalTime), ce.Dir)); //create then add next car to the queue
                            break;
                        case EventType.IntersectionClear:
                            isIntersectionClear = true;
                            Console.Write("intersection clears> ");
                            break;
                        case EventType.LightChange:
                            greenLight = (greenLight + 1) % 2;                                                               //switch greenlight between values 0 and 1
numLightCycles++;
//Console.Write("greenlight#{1} for {0}> ", directions[greenLight].Name, numLightCycles);


//                            events.Add(new Event(EventType.LightChange, directions[greenLight].LightDuration + time)); //add next lightchange to queue
                            break;
                    }// end of processing event

                    //drive a car through the intersection if it's clear and there's one waiting on the correct direction
                    if (isIntersectionClear ) //&& 0 < directions[greenLight].numWaiting())
                    {
//                        directions[greenLight].DriveThrough(time);
                        isIntersectionClear = false;
                        //Console.Write("Drive through from {0}> ", directions[greenLight].Name);

//                        events.Add(new Event(EventType.IntersectionClear, time + 3.0)); //clear the intersection in 3 seconds
                    }

                    Console.WriteLine(""); //newline after listing actions at a given time
                }//end of simulating
                Console.WriteLine("");

                //drive through remaining cars to force their wait times to count
                for (int i = 0; 4/*directions.Length */> i; i++)
                    while ( false) // 0 < directions[i].numWaiting())
                        //directions[i].DriveThrough(time);

                //print statistics
        //string rowFormat = "{0,5} | {1,5} | {2,8:f2}";
                //Console.WriteLine(rowFormat, "dir", "cars", "avg wait"); //table header
//                for (int i = 0; directions.Length > i; i++)
                    //Console.WriteLine(rowFormat, directions[i].Name, directions[i].NumCar, directions[i].TotalWaitTime / directions[i].NumCar); //data for each direction

                //Console.WriteLine(rowFormat, "total", Direction.NumCars, Direction.TotalWaitTimes / Direction.NumCars); //total data
                //Console.WriteLine("");
        
                do
                {
                    Console.WriteLine("Press 'n' to stop simulating these light durations, anything else to continue> ");
                } while (0 == (inputString = Console.ReadLine()).Length);
                inputString = "";
            } while ('n' != inputString[0]);
        
            do
            {
                Console.WriteLine("Press 'n' to stop simulating, anything else to continue> ");
            } while (0 == (inputString = Console.ReadLine()).Length);
        } while ('n' != inputString[0]);

        Console.Write("Simulations finished");
        Console.Read();
    }

    public static double RandomExponential(double mean)
    {
        return mean * -1 * Math.Log(randNum.NextDouble());
    }

        /*    
public static void TestRandomVariableMain()
    {
        Random randNum = new Random();
        double x, m = 5;
        double total = 0;
        
        for (int i = 0; i < 1000 ; i++)
                    {
                                x = RandomExponential(m);
                                total += x;
            Console.Write("{0:f1} ", x);
                    } Console.WriteLine("");
        Console.WriteLine("The average with m = {0} of 1000 elements is {1}", m, total/1000);
                        Console.Read();
    }//end of test function for RandomExponential 
*/
}//end of PartB
