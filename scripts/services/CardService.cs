using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CardService
{
    SpriteRenderer cardObjectComponent;
    Text cardTextComponent;
    RawImage cardImageComponent;
    Button goCardButtonComponent;
    Button backCardButtonComponent;

    private Main mainComponent;
    private ServiceLocator serviceLocator;
    private Texture2D cardImageFile;

    Card currentCard;
    private GameObjects gameObjects;

    bool isCardShowing;

    public CardService(GameObjects _gameObjects)
    {
        gameObjects = _gameObjects;

        cardObjectComponent = gameObjects.card.GetComponent<SpriteRenderer>();
        cardTextComponent = gameObjects.cardText.GetComponent<Text>();
        cardImageComponent = gameObjects.cardImage.GetComponent<RawImage>();
        goCardButtonComponent = gameObjects.goCardButton.GetComponent<Button>();
        backCardButtonComponent = gameObjects.backCardButton.GetComponent<Button>();

        mainComponent = gameObjects.main.GetComponent<Main>();
        serviceLocator = mainComponent.serviceLocator;
    }

    internal void showController(Card card, Position heroPosition, Position goalPosition)
    {
        if (
            heroPosition.x == goalPosition.x 
            && heroPosition.y == goalPosition.y
            && heroPosition.onTheWay == false
            )
        {
            SceneManager.LoadScene("End");
            return;
        }

        if (!heroPosition.onTheWay 
            && !isCardShowing 
            && !card.isWin
            && (heroPosition.x != 1 || heroPosition.y != Constants.fieldSize)
            )
        {
            showCard(card);
            isCardShowing = true;
            serviceLocator.heroService.isInputBlocked = true;
        }
    }

    public void showCard(Card card)
    {
        currentCard = card;

        cardObjectComponent.enabled = true;
        cardTextComponent.enabled = true;
        cardImageComponent.enabled = true;
        gameObjects.goCardButton.SetActive(true);
        gameObjects.backCardButton.SetActive(true);

        cardTextComponent.text = card.text;

        string imagePath = "img/cards/" + card.imageFileName;
        cardImageFile = Resources.Load(imagePath) as Texture2D;
        cardImageComponent.texture = cardImageFile;

        if (card.isSafe)
        {
            goCardButtonComponent.onClick.AddListener(safetyAction);
        }
        else { 
            goCardButtonComponent.onClick.AddListener(dangerousAction);
        }

        backCardButtonComponent.onClick.AddListener(backAction);
    }

    private void safetyAction()
    {
        currentCard.isOpen = true;
        mainComponent.gameFields[(int)currentCard.position.x, (int)currentCard.position.y] = currentCard;
        serviceLocator.heroService.isInputBlocked = false;

        hideCard();

        isCardShowing = false;
    }

    private void dangerousAction()
    {
        SceneManager.LoadScene("End");
        isCardShowing = false;
    }

    private void backAction()
    {
        hideCard();
        isCardShowing = false;
        serviceLocator.heroService.goBack();
        serviceLocator.heroService.isInputBlocked = false;
    }

    public void hideCard()
    {
        cardObjectComponent.enabled = false;
        cardTextComponent.enabled = false;
        cardImageComponent.enabled = false;

        gameObjects.goCardButton.SetActive(false);
        gameObjects.backCardButton.SetActive(false);
    }
}
