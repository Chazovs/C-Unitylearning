
using System.Collections.Generic;

public class Constants 
{
    //шаг сетки
    public const float step = 16;

    public const int gridWidth = 10;

    //скорость передвижения героя
    public const float speed = 10f;

    //размер игрового поля
    public const float fieldSize = 10;

    //время игры в секундах
    internal static float totalTime = 200f;
    
    //позиция по Z открытого поля
    internal static float openedFieldZ = -3;

    //стартовая позиция героя
    internal static Position startPosition = new Position(){ x = 1, y = 10 };

    //соседние позиции
    internal static List<Position> adjacentPositions = new List<Position> {
            new Position {x = 0, y = 1},
            new Position {x = 0, y = -1},
            new Position {x = 1, y = 0},
            new Position {x = -1, y = 0},
        };

    internal static Position leftPosition = new Position { x = -1, y = 0 };
    internal static Position rightPosition = new Position { x = 1, y = 0 };
    internal static Position upPosition = new Position { x = 0, y = 1 };
    internal static Position downPosition = new Position { x = 0, y = -1 };
}
