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
    // Start is called before the first frame update
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
            RulesAndHistoryObjects.startMenuElements.SetActive(false);
            RulesAndHistoryObjects.exceptionMsg.GetComponent<Text>().text
                = Langs.GetMessge("NETWORK_ERROR");

            yield break;
        }
        else
        {
            Main.gameData
                = JsonConvert.DeserializeObject<GetCardResponse>(www.downloadHandler.text);
            
            if (Main.gameData.cards == null)
            {
                RulesAndHistoryObjects.startMenuElements.SetActive(false);
                RulesAndHistoryObjects.exceptionMsg.GetComponent<Text>().text
                    = Langs.GetMessge("NETWORK_ERROR");

                yield break;
            }
            else
            {
                SceneManager.LoadScene("Game");
            }
        }
    }
}
