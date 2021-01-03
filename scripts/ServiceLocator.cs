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

    public ServiceLocator(GameObjects gameObjects)
    {
        cardService = new CardService(gameObjects);
        heroService = new HeroService(gameObjects);
        gridService = new GridService();
        buttonService = new ButtonService();
        gameFieldService = new GameFieldService(gameObjects);
    }
}
