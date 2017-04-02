  using System;

 public class Direction
    {public static  Direction  northwards = new Direction( 0, 1);
     public static Direction  eastwards = new Direction( 1, 0);
     public static Direction  southwards = new Direction( 0, -1);
     public static Direction  westwards = new Direction( -1, 0);

     public int x {get; private set;}
        public  int  y{get; private set;}

        public Direction( int x, int  y)
        {
            if ((1 == Math.Abs(x) || 1 == Math.Abs(y)) && (0 == x || 0 == y)) //if either value is +-1 and the other is 0
            { this.x = x; this.y = y; }
            else
                throw new ArgumentException(string.Format("Direction initialised with invalid coordinates [ %d, %d]", x, y)); 
                    }//end constructor 

        public Direction inverseDirection()
        { return new Direction(-1 * this.x, -1 * this.y); }

public void turnLeft()
{//use arithmetic equivalent to a rotation matrix
    this.x = 0 * this.x + -1 * this.y;
    this.y = 1 * this.x + 0 * this.y;
}//end turnLeft

        public void   turnRight()
{//use arithmetic equivalent to a rotation matrix
    this.x = 0 * this.x + 1 * this.y;
    this.y = -1 * this.x + 0 * this.y;
}//end turnRight
   }//end class Direction
