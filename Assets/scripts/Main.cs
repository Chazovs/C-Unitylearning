using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    internal static List<Card> safetyCards;
    internal static List<Card> dangerousCards;
    internal static Book book;
    public Position goalPosition;
    public Card[,] gameFields = new Card[(int)Constants.fieldSize, (int)Constants.fieldSize];
    public GameObjects gameObjects;

    public bool isCardShowing = false;

    public Position heroPosition = Constants.startPosition;
    public Position newPosition = new Position();
    public Position previousPosition = new Position();

    private Timer timer;

    CardService cardService;
    GridService gridService;
    GameFieldService gameFieldService;
    HeroService heroService;

    // Start is called before the first frame update
    void Start()
    {
        new GameObjects();

        cardService = ServiceLocator.GetService<CardService>();
        gridService = ServiceLocator.GetService<GridService>();
        gameFieldService = ServiceLocator.GetService<GameFieldService>();
        heroService = ServiceLocator.GetService<HeroService>();

        timer = new Timer();

        cardService.hideCard();
        gridService.setGoal();
        gameFieldService.FillGameFields();

        //рантайм тесты
        FieldTest.showField(); //показать все поле с координатами и карточками
    }

    // Update is called once per frame
    void Update()
    {
        timer.updateTimer();
        heroService.Move();
    }
}
