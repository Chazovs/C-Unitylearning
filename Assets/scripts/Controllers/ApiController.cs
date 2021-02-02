using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApiController : MonoBehaviour
{
    internal void loadCardsAction(Book book)
    {
        StartCoroutine(loadCards(book));
    }
    
    IEnumerator loadCards(Book book)
    {
        var www = UnityWebRequest.Get(Constants.serverUrl 
            + "cards?book=" 
            + book.code 
            + "&lang=" 
            + Settings.currentLang 
            + "&key="
            + Settings.apiKey
            );

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            MenuObjects.startMenuElements.SetActive(false);
            MenuObjects.exceptionMsg.GetComponent<Text>().text
                = Langs.GetMessge("NETWORK_ERROR");

            yield break;
        }
        else
        {
            Main.gameData
                = JsonConvert.DeserializeObject<GetCardResponse>(www.downloadHandler.text);
            
            if (Main.gameData.cards == null)
            {
                MenuObjects.startMenuElements.SetActive(false);
                MenuObjects.exceptionMsg.GetComponent<Text>().text
                    = Langs.GetMessge("NETWORK_ERROR");

                yield break;
            }
            else
            {
                SceneManager.LoadScene("Game");
            }
        }
    }

    internal void loadCardImageAction(string imageName)
    {
        StartCoroutine(loadCardsImage(imageName));
    }
    IEnumerator loadCardsImage(string imageName)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(Constants.serverUrl
            + "res/img/cards/"
            + imageName
            + ".jpg"
            );

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            yield break;
        }
        else
        {
            GameObjects.cardImage.GetComponent<RawImage>().texture 
                = ((DownloadHandlerTexture)www.downloadHandler).texture;
            yield break;
        }
    }
}
