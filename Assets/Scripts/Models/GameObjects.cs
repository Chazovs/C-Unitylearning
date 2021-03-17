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
    public static GameObject openField_1;
    public static GameObject openField_2;
    public static GameObject openField_3;
    public static GameObject openField_4;
    public static GameObject openField_5;
    public static GameObject openField_6;
    public static GameObject apiController;
    public static GameObject toggleMuteBtn;
    public static GameObject mainMusic;

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
        openField_1 = GameObject.Find("openField_1");
        openField_2 = GameObject.Find("openField_2");
        openField_3 = GameObject.Find("openField_3");
        openField_4 = GameObject.Find("openField_4");
        openField_5 = GameObject.Find("openField_5");
        openField_6 = GameObject.Find("openField_6");
        apiController = GameObject.Find("apiController");
        toggleMuteBtn = GameObject.Find("muteBtn");
        mainMusic = GameObject.Find("mainMusic");
    }
}
