using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApiController : MonoBehaviour
{
    [System.Obsolete]
    internal void loadCardsAction(Book book)
    {
        StartCoroutine(loadCards(book));
    }

    [System.Obsolete]
    IEnumerator loadCards(Book book)
    {
        var www = UnityWebRequest.Get(Constants.serverUrl 
            + "api/cards?book=" 
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
            GameFieldService gameFieldService = ServiceLocator.GetService<GameFieldService>();

            gameFieldService.gameData
                = JsonConvert.DeserializeObject<GetCardResponse>(www.downloadHandler.text);
            
            if (gameFieldService.gameData.cards == null)
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
            + "images/cards/"
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
