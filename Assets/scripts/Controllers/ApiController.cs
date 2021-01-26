using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiController : MonoBehaviour
{

    internal List<Card> getCardsAction(Book book)
    {
        StartCoroutine(getCards(book));

        return new List<Card>();
    }
    // Start is called before the first frame update
    IEnumerator getCards(Book book)
    {

        /*var result  =  UnityWebRequest.Get(Constants.serverUrl + "books/" + book.code);*/
        var www = UnityWebRequest.Get(Constants.serverUrl + "cards");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
        }
    }

    
}
