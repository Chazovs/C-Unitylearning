using System;
using System.Collections.Generic;

public class Position 
{
    //отсчет идет от 1
    public int x;
    public int y;

    //меняется ли в данный момент положение героя
    public bool onTheWay;

    public bool visited;

    public List<Position> openFields;

    internal Position GetLeft()
    {
        int newX = x - 1;

        return newX > 0 ? new Position() {x = newX, y = y } : null;
    }

    internal Position GetRight()
    {
        int newX = x + 1;

        return newX < 11 ? new Position() { x = newX, y = y } : null;
    }

    internal Position GetUp()
    {
        int newY = y + 1;

        return newY < 11 ? new Position() { x = x, y = newY } : null;
    }

    internal Position GetDown()
    {
        int newY = y - 1;

        return newY > 0 ? new Position() { x = x, y = newY } : null;
    }

    internal int arX()
    {
        return x - 1;
    }

    internal int arY()
    {
        return y - 1;
    }
}
