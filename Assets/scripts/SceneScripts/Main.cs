using UnityEngine;

public class Main : MonoBehaviour
{
    public static GetCardResponse gameData;

    public static Position goalPosition;
    public static Card[,] gameFields = new Card[Constants.fieldSize, Constants.fieldSize];
    public static bool isCardShowing = false;
    public static Position heroPosition = Constants.startPosition;
    public static Position newPosition = new Position();
    public static Position previousPosition = new Position();

    private Timer _timer;

    void Start()
    {
        Application.targetFrameRate = Settings.defaultFramRate;

        new GameObjects();

        _timer = new Timer();

        ServiceLocator.GetService<CardService>().HideCard();
        ServiceLocator.GetService<GridService>().SetGoal();
        ServiceLocator.GetService<GameFieldService>().FillGameFields();

        //рантайм тесты
        FieldTest.showField(); //показать все поле с координатами и карточками
    }

    // Update is called once per frame
    void Update()
    {
        _timer.UpdateTimer();
        ServiceLocator.GetService<HeroService>().Move();
    }
}
