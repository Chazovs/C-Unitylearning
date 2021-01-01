using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class CardService
{
    SpriteRenderer cardObjectComponent;
    SpriteRenderer cardTextComponent;

    private GameObject cardObject;
    private GameObject cardText;
    private GameObject cardImage;
    private GameObject goCardButton;
    private GameObject backCardButton;
    private Texture2D cardImageFile;

    public CardService()
    {
        cardObject = GameObject.Find("card");
        cardText = GameObject.Find("cardText");
        cardImage = GameObject.Find("cardImage");
        goCardButton = GameObject.Find("goCardButton");
        backCardButton = GameObject.Find("backCardButton");

        cardObjectComponent = cardObject.GetComponent<SpriteRenderer>();
       /* cardTextComponent = cardText.GetComponent<SpriteRenderer>();*/

    }

    public void showCard(Card card)
    {
        cardObjectComponent.enabled = true;
        /*cardTextComponent.enabled = true;*/

        cardText.GetComponent<Text>().text = card.text;

        string imagePath = "img/cards/" + card.imageFileName;
        cardImageFile = Resources.Load(imagePath) as Texture2D;
        cardImage.GetComponent<RawImage>().texture = cardImageFile;


        Button goCardButtonComponent = goCardButton.GetComponent<Button>();
        Button backCardButtonComponent = backCardButton.GetComponent<Button>();

        UnityEngine.Events.UnityAction action = card.isSafe ? safetyAction : dangerousAction;
        goCardButtonComponent.onClick.AddListener(action);
        backCardButtonComponent.onClick.AddListener(backAction);
    }

    private void safetyAction()
    {
        Debug.Log("безопасное действие");
    }

    private void dangerousAction()
    {
        SceneManager.LoadScene("End");
    }

    private void backAction()
    {
        Debug.Log("ход назад");
    }

    public void hideCard(Card card)
    {
        cardObjectComponent.enabled = false;
        cardTextComponent.enabled = false;
    }
}
