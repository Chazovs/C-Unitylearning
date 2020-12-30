using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardService
{
    SpriteRenderer cardObjectComponent;
    SpriteRenderer cardTextComponent;

    private GameObject cardObject;
    private GameObject cardText;
    private GameObject cardImage;
    private Texture2D cardImageFile;

    public CardService()
    {
        cardObject = GameObject.Find("card");
        cardText = GameObject.Find("cardText");
        cardImage = GameObject.Find("cardImage");

        cardObjectComponent = cardObject.GetComponent<SpriteRenderer>();
       /* cardTextComponent = cardText.GetComponent<SpriteRenderer>();*/

    }

    public void showCard(Card card)
    {
        cardObjectComponent.enabled = true;
        /*cardTextComponent.enabled = true;*/

        cardText.GetComponent<Text>().text = card.text;

        string imagePath = "img/cards/" + card.imageFileName;
        /*Texture2D othericon = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(imagePath, typeof(Texture2D));*/
        Debug.Log(imagePath);
        cardImageFile = Resources.Load(imagePath) as Texture2D;
        cardImage.GetComponent<RawImage>().texture = cardImageFile;

    }

    public void hideCard(Card card)
    {
        cardObjectComponent.enabled = false;
        cardTextComponent.enabled = false;
    }
}
