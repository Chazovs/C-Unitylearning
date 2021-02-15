using System.Collections.Generic;

public class Position 
{
    public float x;
    public float y;

    //меняется ли в данный момент положение героя
    public bool onTheWay;

    public bool visited;

    public List<Position> openFields;
}
