public class Card
{
    /**
     * текст карточки
     */
    public System.Int64 id;

    /**
     * текст карточки
     */
    public string text;

    /**
     * название файла изображения
     */
    public string imageName;

    /*
     * является ли поле безопасным
     */
    public bool isSafe;

    /*
     * метка для победной клетки
     */
    public bool isWin;

    /*
     * открыта ли карта
     */
    public bool isOpen;

    /*
    * позиция на карте
    */
    public Position position;

    /**
     * является ли стартовым полем
     */
    internal bool isStart;
}
