using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    public CardService cardService;
    public HeroService heroService;
    public GridService gridService;
    public ButtonService buttonService;
    public GameFieldService gameFieldService;

    public ServiceLocator(ref GameObjects gameObjects)
    {
        cardService = new CardService(ref gameObjects);
        heroService = new HeroService(ref gameObjects);
        gridService = new GridService(ref gameObjects);
        buttonService = new ButtonService();
        gameFieldService = new GameFieldService(ref gameObjects);
    }
}
