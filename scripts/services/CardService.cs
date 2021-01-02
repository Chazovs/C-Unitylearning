using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class CardService
{
    SpriteRenderer cardObjectComponent;
    Text cardTextComponent;
    RawImage cardImageComponent;
    Button goCardButtonComponent;
    Button backCardButtonComponent;

    private GameObject cardObject;
    private GameObject cardText;
    private GameObject cardImage;
    private GameObject goCardButton;
    private GameObject backCardButton;
    private GameObject hero;

    private Texture2D cardImageFile;

    Mover mover;

    Card currentCard;

    public CardService()
    {
        cardObject = GameObject.Find("card");
        cardText = GameObject.Find("cardText");
        cardImage = GameObject.Find("cardImage");
        goCardButton = GameObject.Find("goCardButton");
        backCardButton = GameObject.Find("backCardButton");
        hero = GameObject.Find("Hero");

        cardObjectComponent = cardObject.GetComponent<SpriteRenderer>();
        cardTextComponent = cardText.GetComponent<Text>();
        cardImageComponent = cardImage.GetComponent<RawImage>();
        goCardButtonComponent = goCardButton.GetComponent<Button>();
        backCardButtonComponent = backCardButton.GetComponent<Button>();

        mover = hero.GetComponent<Mover>();
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
        mover.setCardInField(currentCard);
        mover.isInputBlocked = false;

        hideCard();
    }

    private void dangerousAction()
    {
        SceneManager.LoadScene("End");
    }

    private void backAction()
    {
        hideCard();
        mover.goBack();
        mover.isInputBlocked = false;
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
