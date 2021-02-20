using UnityEngine;

public class GameObjects
{
    public static GameObject grid;
    public static GameObject endPoint;
    public static GameObject timerBarImage;
    public static GameObject hero;
    public static GameObject card;
    public static GameObject cardText;
    public static GameObject cardImage;
    public static GameObject goCardButton;
    public static GameObject backCardButton;
    public static GameObject main;
    public static GameObject openField;
    public static GameObject apiController;

    public GameObjects()
    {
        grid = GameObject.Find("grid");
        endPoint = GameObject.Find("EndPoint");
        timerBarImage = GameObject.Find("colorLine");
        hero = GameObject.Find("Hero");
        card = GameObject.Find("card");
        cardText = GameObject.Find("cardText");
        cardImage = GameObject.Find("cardImage");
        goCardButton = GameObject.Find("goCardButton");
        backCardButton = GameObject.Find("backCardButton");
        main = GameObject.Find("main");
        openField = GameObject.Find("openField");
        apiController = GameObject.Find("apiController");
    }
}
