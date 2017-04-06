public class PathNode
{
    public Road  road { get; private set; }
    public PathNode next { get; private set; }
    public PathNode last;
    public int TotalDistance;

    public PathNode(Road r)
    {
        this.road = r;
        this.next = null;
        this.last = null;
        this.TotalDistance = 0;
    }

        }//end PathNode