
using System.Collections.Generic;

public static class Constants 
{
    //шаг сетки
    public const float step = 16;

    public const int gridWidth = 10;

    //скорость передвижения героя
    public const float speed = 10f;

    //размер игрового поля
    public const int fieldSize = 10;

    //время игры в секундах
    internal const float totalTime = 200f;
    
    //позиция по Z открытого поля
    internal const float openedFieldZ = -3;

    //последний  слайд с правилами
    internal static int lastRulesSlide = 5;
    //последний  слайд с историей
    internal static int lastHistorySlide = 6;

    internal static string serverUrl = "http://server.g4v.ru/";

    internal static Book defaultBook = new Book() { 
        name = Langs.GetMessge("NEW_HOME"),
        code = "new-home",
        qrCode = "iVBORw0KGgoAAAANSUhEUgAAAMsAAADLAQMAAAD6NfVwAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAGUExURQAAAP///6XZn90AAAAJcEhZcwAADsMAAA7DAcdvqGQAAAJZSURBVFjD1dixbsIwEAbgszJ4axgzoPhReLTk0XiUoA6sdMsQ5Xp3doC2/FZ16pBaaqn5csSu7bMD5TLITzvRsFJkPtNzeUG9ixpMHaZYa2FgKeeBeaakf8qL1vj2hilgajBFF1Va2FJYSCtrM7fTaeSFWrlQOqwtRESYoisqYTr8ooXbe4318su9flLCFDF1rntV6PhE28R60Yxv1LmikouoSnlG2aDQILVbmVGdi3pMyUVUo7KWwyL9P5eXrxngJyVMAVPlXtEV1RHnIlPJOrTE8gZfXMSYZkxX1wfKv76Z81q2YchZtORDSAlTdEURphZTr9Qyj0E7dBaScpIate+YJkwzJnbRBdNildIvmUo6GtOJ7UIXzZgmF73XokKp2JImu4I53DPASyJMh7/+wFiLkgUgv+3IYIlI8xHlKQrpgClgIhelGtmUk6IZiMft2CNrpHdRg4lcVGnhG2nuv++6uca82v4F6Yipc1HlXqkWpe9NunptbHQPTray5XwIqcMUXZQwhVrUaqNG+cA58qPMmK6uqA9M7KJrWcsylXRijZaWJIA0Y0OqRH1gWjGxqxnWp5Yf+5ftZiUDQOoxBUxHTBFTh8mOWHLG5zW/WC/13Bl5xcSYFkwTpqvrXmvJVEO5gprtpNlWaME0/TVdalFl8tuOdRptNOwExz5iV9QN06VGudhDluaj/Hib77V/2p7NdbLKkm5Kv2zX2z/lJ0TtEOlhzbpZDks7p/uT72OEcjKN/4nKoNgQ6eZL9C8oz6j8uMLlwFm+jtg9lbX89J0alYP/3on5Ezz5Q0rg9+cNAAAAAElFTkSuQmCC",
        pdfUri= "pdf/new-home.pdf",
        uri = "https://g4v.ru/books/new-home"
    };

    internal static string myBooksType = "myBooks";
    internal static string newBooksType = "newBooks";
}
