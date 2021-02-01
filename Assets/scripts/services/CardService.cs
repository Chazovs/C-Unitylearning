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

        //если герой пришел к цели
        if (Main.heroPosition.x == Main.goalPosition.x
            && Main.heroPosition.y == Main.goalPosition.y
            && Main.heroPosition.onTheWay == false
            )
        {
            SceneManager.LoadScene("End");

            return;
        }

        Card card = Main.gameFields[(int)Main.heroPosition.x - 1, (int)Main.heroPosition.y - 1];

        if (Main.heroPosition.onTheWay
            || Main.isCardShowing
            || card.isWin
            || card.isOpen
            || Main.heroPosition.x == 1 && Main.heroPosition.y == Constants.fieldSize
            )
        {
            return;
        }

        showCard(ref card);
        Main.isCardShowing = true;
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

        string imagePath = "img/cards/" + card.imageName;
        cardImageComponent.texture = Resources.Load(imagePath) as Texture2D;

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
        HideCard();
        ServiceLocator.GetService<GameFieldService>()
            .setOpenField(currentCard.position);

        Main.gameFields[(int)currentCard.position.x-1, (int)currentCard.position.y-1].isOpen = true;
        Main.isCardShowing = false;
    }

    public void dangerousAction()
    {
        SceneManager.LoadScene("End");

        Main.isCardShowing = false;
    }

    public void backAction()
    {
        HideCard();

        Main.isCardShowing = false;

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
