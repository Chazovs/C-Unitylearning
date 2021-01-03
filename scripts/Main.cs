using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private Timer timer;
    public ServiceLocator serviceLocator;
    public Card[,] gameFields;

    // Start is called before the first frame update
    void Start()
    {
        gameFields = new Card[(int)Constants.fieldSize, (int)Constants.fieldSize];

        GameObjects gameObjects = new GameObjects();

        serviceLocator = new ServiceLocator(gameObjects);

        timer = new Timer(gameObjects.timerBarImage);
        
        serviceLocator.cardService.hideCard();

        serviceLocator.gridService.createGrid(gameObjects.grid, gameObjects.block);
        serviceLocator.gridService.setGoal(gameObjects.endPoint);

        gameFields = serviceLocator.gameFieldService.fillGameFields();
    }

    // Update is called once per frame
    void Update()
    {
        timer.updateTimer();
        serviceLocator.heroService.move();
    }
}
