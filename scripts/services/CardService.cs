using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CardService 
{
    SpriteRenderer cardObjectComponent;
    Text cardTextComponent;
    RawImage cardImageComponent;
    public GameObject goCardButton;
    public GameObject backCardButton;
    Button goCardButtonComponent;
    Button backCardButtonComponent;

    private Main mainComponent;
    private Texture2D cardImageFile;

    Card currentCard;
    private GameObjects gameObjects;

    bool isCardShowing;

    public CardService(ref GameObjects _gameObjects)
    {
        gameObjects = _gameObjects;

        mainComponent = gameObjects.main.GetComponent<Main>();

        cardObjectComponent = gameObjects.card.GetComponent<SpriteRenderer>();
        cardTextComponent = gameObjects.cardText.GetComponent<Text>();
        cardImageComponent = gameObjects.cardImage.GetComponent<RawImage>();
        goCardButtonComponent = gameObjects.goCardButton.GetComponent<Button>();
        backCardButtonComponent = gameObjects.backCardButton.GetComponent<Button>();
    }

    internal void showController(Card card, Position heroPosition, Position goalPosition)
    {
/*        Debug.Log(heroPosition.x);
        Debug.Log(goalPosition.x);
        Debug.Log(heroPosition.y);
        Debug.Log(goalPosition.y);
        Debug.Log(heroPosition.onTheWay);*/

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
            && !card.isOpen
            && (heroPosition.x != 1 || heroPosition.y != Constants.fieldSize)
            )
        {
            showCard(ref card);
            isCardShowing = true;
            mainComponent.serviceLocator.heroService.isInputBlocked = true;
        }
    }

    public void showCard(ref Card card)
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

    public void safetyAction()
    {
        hideCard();
        currentCard.isOpen = true;
        mainComponent.gameFields[(int)currentCard.position.x, (int)currentCard.position.y] = currentCard;
        mainComponent.serviceLocator.heroService.isInputBlocked = false;

        isCardShowing = false;
    }

    public void dangerousAction()
    {
        SceneManager.LoadScene("End");
        isCardShowing = false;
    }

    public void backAction()
    {
        hideCard();
        isCardShowing = false;
        mainComponent.serviceLocator.heroService.goBack();
        mainComponent.serviceLocator.heroService.isInputBlocked = false;
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
