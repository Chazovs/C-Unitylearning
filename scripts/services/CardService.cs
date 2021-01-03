using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardService
{
    SpriteRenderer cardObjectComponent;
    Text cardTextComponent;
    RawImage cardImageComponent;
    Button goCardButtonComponent;
    Button backCardButtonComponent;

    private Main mainComponent;
    private ServiceLocator serviceLocator;
    private GameObject cardObject;
    private GameObject cardText;
    private GameObject cardImage;
    private GameObject goCardButton;
    private GameObject backCardButton;
    private GameObject main;
    private Texture2D cardImageFile;

    Card currentCard;

    public CardService(GameObjects gameObjects)
    {
        cardObject = gameObjects.card;
        cardText = gameObjects.cardText;
        cardImage = gameObjects.cardImage;
        goCardButton = gameObjects.goCardButton;
        backCardButton = gameObjects.backCardButton;
        main = gameObjects.main;

        cardObjectComponent = cardObject.GetComponent<SpriteRenderer>();
        cardTextComponent = cardText.GetComponent<Text>();
        cardImageComponent = cardImage.GetComponent<RawImage>();
        goCardButtonComponent = goCardButton.GetComponent<Button>();
        backCardButtonComponent = backCardButton.GetComponent<Button>();

        mainComponent = main.GetComponent<Main>();
        serviceLocator = mainComponent.serviceLocator;
    }

    public void showCard(Card card)
    {
        currentCard = card;

        cardObjectComponent.enabled = true;
        cardTextComponent.enabled = true;
        cardImageComponent.enabled = true;
        goCardButton.SetActive(true);
        backCardButton.SetActive(true);

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
        serviceLocator.heroService.setCardInField(currentCard);
        serviceLocator.heroService.isInputBlocked = false;

        hideCard();
    }

    private void dangerousAction()
    {
        SceneManager.LoadScene("End");
    }

    private void backAction()
    {
        hideCard();
        serviceLocator.heroService.goBack();
        serviceLocator.heroService.isInputBlocked = false;
    }

    public void hideCard()
    {
        cardObjectComponent.enabled = false;
        cardTextComponent.enabled = false;
        cardImageComponent.enabled = false;

        goCardButton.SetActive(false);
        backCardButton.SetActive(false);
    }
}
