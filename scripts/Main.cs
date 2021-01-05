using UnityEngine;

public class Main : MonoBehaviour
{
    private Timer timer;
    public ServiceLocator serviceLocator;
    private Position goalPosition;
    public Card[,] gameFields;

    // Start is called before the first frame update
    void Start()
    {
        gameFields = new Card[(int)Constants.fieldSize, (int)Constants.fieldSize];

        GameObjects gameObjects = new GameObjects();

        serviceLocator = new ServiceLocator(ref gameObjects);

        timer = new Timer(gameObjects.timerBarImage);
        
        serviceLocator.cardService.hideCard();

        serviceLocator.gridService.createGrid(gameObjects.grid, gameObjects.block);

        goalPosition = serviceLocator.gridService.setGoal(gameObjects.endPoint);
        gameFields = serviceLocator.gameFieldService.fillGameFields();
    }

    // Update is called once per frame
    void Update()
    {
        timer.updateTimer();

        Position heroPosition = serviceLocator.heroService.move();

        serviceLocator.cardService.showController(
            gameFields[(int) heroPosition.x - 1, (int) heroPosition.y - 1],
           heroPosition,
           goalPosition
            );
    }
}
