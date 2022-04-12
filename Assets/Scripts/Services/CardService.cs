using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardService 
{
    public bool isCardShowing = false;

    SpriteRenderer cardObjectComponent;
    Text cardTextComponent;
    RawImage cardImageComponent;
    Button goCardButtonComponent;
    Button backCardButtonComponent;

    Card currentCard;

    public CardService()
    {
        cardObjectComponent = GameObjects.card.GetComponent<SpriteRenderer>();
        cardTextComponent = GameObjects.cardText.GetComponent<Text>();
        cardImageComponent = GameObjects.cardImage.GetComponent<RawImage>();
        goCardButtonComponent = GameObjects.goCardButton.GetComponent<Button>();
        backCardButtonComponent = GameObjects.backCardButton.GetComponent<Button>();
    }

    internal void ShowController()
    {
        HeroService heroService = ServiceLocator.GetService<HeroService>();
        GameFieldService gameFieldService = ServiceLocator.GetService<GameFieldService>();

        //если герой пришел к цели
        if (heroService.heroPosition.x == gameFieldService.goalPosition.x
            && heroService.heroPosition.y == gameFieldService.goalPosition.y
            && heroService.heroPosition.onTheWay == false
            )
        {
            Settings.endType = "happyEnd";
            SceneManager.LoadScene("Menu");
        }

        Card card = gameFieldService.gameFields[heroService.heroPosition.arX(), heroService.heroPosition.arY()];

        if (heroService.heroPosition.onTheWay
            || isCardShowing
            || card.isWin
            || card.isOpen
            || card.isStart
            || heroService.heroPosition.x == 1 && heroService.heroPosition.y == Constants.fieldSize
            )
        {
            return;
        }

        showCard(ref card);
        isCardShowing = true;
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

        GameObjects.apiController.GetComponent<ApiController>()
            .loadCardImageAction(card.imageName);

        if (card.isSafe)
        {
            goCardButtonComponent.onClick.RemoveListener(dangerousAction);
            goCardButtonComponent.onClick.RemoveListener(safetyAction);
            goCardButtonComponent.onClick.AddListener(safetyAction);
        }
        else {
            goCardButtonComponent.onClick.RemoveListener(dangerousAction);
            goCardButtonComponent.onClick.RemoveListener(safetyAction);
            goCardButtonComponent.onClick.AddListener(dangerousAction);
        }

        goCardButtonComponent.onClick.RemoveListener(backAction);
        backCardButtonComponent.onClick.AddListener(backAction);
    }

    public void safetyAction()
    {
        Main.timer = new Timer();
        AudioSource music = GameObjects.mainMusic.GetComponent<AudioSource>();

        music.Stop();
        music.Play();
        HideCard();
        GameFieldService gameFieldService = ServiceLocator.GetService<GameFieldService>();
        gameFieldService.setOpenField(currentCard.position);

        gameFieldService.gameFields[currentCard.position.arX(), currentCard.position.arY()].isOpen = true;
        isCardShowing = false;
    }

    public void dangerousAction()
    {
        Settings.endType = "sadEnd";
        SceneManager.LoadScene("Menu");
    }

    public void backAction()
    {
        HideCard();

        isCardShowing = false;

        ServiceLocator.GetService<HeroService>().goBack();
    }

    public void HideCard()
    {
        cardObjectComponent.enabled = false;
        cardTextComponent.enabled = false;
        cardImageComponent.enabled = false;

        GameObjects.goCardButton.SetActive(false);
        GameObjects.backCardButton.SetActive(false);
    }
}
