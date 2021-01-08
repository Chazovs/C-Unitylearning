using UnityEngine;

public class Main : MonoBehaviour
{
    public ServiceLocator serviceLocator;
    public Position goalPosition;
    public Card[,] gameFields = new Card[(int)Constants.fieldSize, (int)Constants.fieldSize];
    public GameObjects gameObjects;

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
        serviceLocator.gameFieldService.fillGameFields();

        //рантайм тесты
        FieldTest.showField(); //показать все поле с координатами и карточками
    }

    // Update is called once per frame
    void Update()
    {
        timer.updateTimer();

        Position heroPosition = serviceLocator.heroService.move();

        serviceLocator.cardService.showController(heroPosition);
    }
}
