using UnityEngine;

public class GameObjects : MonoBehaviour
{
    public GameObject grid;
    public GameObject endPoint;
    public GameObject timerBarImage;
    public GameObject hero;
    public GameObject card;
    public GameObject cardText;
    public GameObject cardImage;
    public GameObject goCardButton;
    public GameObject backCardButton;
    public GameObject main;
    public GameObject openField;

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
    }
}
