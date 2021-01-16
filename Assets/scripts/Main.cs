using UnityEngine;

public class Main : MonoBehaviour
{
    public ServiceLocator serviceLocator;
    public Position goalPosition;
    public Card[,] gameFields = new Card[(int)Constants.fieldSize, (int)Constants.fieldSize];
    public GameObjects gameObjects;

    public bool isCardShowing = false;

    public Position heroPosition = Constants.startPosition;
    public Position newPosition = new Position();
    public Position previousPosition = new Position();

    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = new GameObjects();
        serviceLocator = new ServiceLocator(ref gameObjects);
        timer = new Timer(gameObjects.timerBarImage);
        
        serviceLocator.cardService.hideCard();
        serviceLocator.gridService.createGrid();
        serviceLocator.gridService.setGoal();
        serviceLocator.gameFieldService.FillGameFields();

        //рантайм тесты
        FieldTest.showField(); //показать все поле с координатами и карточками
    }

    // Update is called once per frame
    void Update()
    {
        timer.updateTimer();
        serviceLocator.heroService.Move();
    }
}
