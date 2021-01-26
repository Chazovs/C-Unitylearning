using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public CardService()
    {
        mainComponent = GameObjects.main.GetComponent<Main>();

        cardObjectComponent = GameObjects.card.GetComponent<SpriteRenderer>();
        cardTextComponent = GameObjects.cardText.GetComponent<Text>();
        cardImageComponent = GameObjects.cardImage.GetComponent<RawImage>();
        goCardButtonComponent = GameObjects.goCardButton.GetComponent<Button>();
        backCardButtonComponent = GameObjects.backCardButton.GetComponent<Button>();
    }

    internal void ShowController()
    {
        //если герой пришел к цели
        if (mainComponent.heroPosition.x == mainComponent.goalPosition.x
            && mainComponent.heroPosition.y == mainComponent.goalPosition.y
            && mainComponent.heroPosition.onTheWay == false
            )
        {
            SceneManager.LoadScene("End");

            return;
        }

        Card card = mainComponent.gameFields[(int)mainComponent.heroPosition.x - 1, (int)mainComponent.heroPosition.y - 1];

        if (mainComponent.heroPosition.onTheWay
            || mainComponent.isCardShowing
            || card.isWin
            || card.isOpen
            || mainComponent.heroPosition.x == 1 && mainComponent.heroPosition.y == Constants.fieldSize
            )
        {
            return;
        }

        showCard(ref card);
        mainComponent.isCardShowing = true;
        mainComponent.serviceLocator.heroService.isInputBlocked = true;
    }

    public void showCard(ref Card card)
    {
        currentCard = card;

        cardObjectComponent.enabled = true;
        cardTextComponent.enabled = true;
        cardImageComponent.enabled = true;

        GameObjects.goCardButton.SetActive(true);
        GameObjects.backCardButton.SetActive(true);

        cardTextComponent.text = card.text;

        string imagePath = "img/cards/" + card.imageFileName;
        cardImageFile = Resources.Load(imagePath) as Texture2D;
        cardImageComponent.texture = cardImageFile;

        if (card.isSafe)
        {
            goCardButtonComponent.onClick.RemoveListener(safetyAction);
            goCardButtonComponent.onClick.AddListener(safetyAction);
        }
        else {
            goCardButtonComponent.onClick.RemoveListener(dangerousAction);
            goCardButtonComponent.onClick.AddListener(dangerousAction);
        }

        goCardButtonComponent.onClick.RemoveListener(backAction);
        backCardButtonComponent.onClick.AddListener(backAction);
    }

    public void safetyAction()
    {
        hideCard();
        mainComponent.serviceLocator.gameFieldService.setOpenField(currentCard.position);

        mainComponent.gameFields[(int)currentCard.position.x-1, (int)currentCard.position.y-1].isOpen = true;
        mainComponent.serviceLocator.heroService.isInputBlocked = false;
        mainComponent.isCardShowing = false;
    }

    public void dangerousAction()
    {
        SceneManager.LoadScene("End");
        mainComponent.isCardShowing = false;
    }

    public void backAction()
    {
        hideCard();

        mainComponent.isCardShowing = false;
        mainComponent.serviceLocator.heroService.isInputBlocked = false;

        mainComponent.serviceLocator.heroService.goBack();
    }

    public void hideCard()
    {
        cardObjectComponent.enabled = false;
        cardTextComponent.enabled = false;
        cardImageComponent.enabled = false;

        GameObjects.goCardButton.SetActive(false);
        GameObjects.backCardButton.SetActive(false);
    }
}
