
using System.Collections.Generic;

public static class Constants 
{
    //версия игры
    internal static string gameVersion = "100";

    //шаг сетки
    public const float step = 16;

    public const int gridWidth = 10;

    //скорость передвижения героя
    public const float speed = 10f;

    //размер игрового поля
    public const int fieldSize = 10;

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

    //последний  слайд с правилами
    internal static int lastRulesSlide = 5;
    //последний  слайд с историей
    internal static int lastHistorySlide = 6;

    internal static string serverUrl = "http://g4v.ru/";

    internal static Book defaultBook = new Book() { 
        name = Langs.GetMessge("NEW_HOME"),
        code = "new-home",
        qrCode = "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",
        pdfUri= "res/books/new-home-ru.pdf",
        uri = "https://g4v.ru/books/new-home"
    };

    internal static string myBooksType = "myBooks";
    internal static string newBooksType = "newBooks";
}
