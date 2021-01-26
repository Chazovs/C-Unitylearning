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

    public ServiceLocator()
    {
        cardService = new CardService();
        heroService = new HeroService();
        gridService = new GridService();
        buttonService = new ButtonService();
        gameFieldService = new GameFieldService();
    }
}
