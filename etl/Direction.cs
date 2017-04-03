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
            { this.x = 5; this.y = 5; }
                //throw new ArgumentException(string.Format("Direction initialised with invalid coordinates [ %d, %d]", x, y)); 
                    }//end constructor 

        public override string ToString()
        {
return string.Format("dir [{0}, {1}]", x, y);
        }

        public Direction turnAround()
        {
            this.x *= -1;
            this.y *= -1;
            return this;
        }

public Direction turnLeft()
{//use arithmetic equivalent to a rotation matrix
    int x = 0 * this.x + -1 * this.y;
    int y = 1 * this.x + 0 * this.y;
    this.x = x; this.y = y;
    return this;
}//end turnLeft

        public Direction   turnRight()
{//use arithmetic equivalent to a rotation matrix
    int x = 0 * this.x + 1 * this.y;
    int y = -1 * this.x + 0 * this.y;
    this.x = x; this.y = y;
    return this;
}//end turnRight
   }//end class Direction
