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

        /*var result  =  UnityWebRequest.Get(Constants.serverUrl + "books/" + book.code);*/
        var www = UnityWebRequest.Get(Constants.serverUrl + "cards");

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
            GetCardResponse response 
                =  JsonUtility.FromJson<GetCardResponse>(www.downloadHandler.text);

            if (response.cards.safety.Count == 0)
            {
                RulesAndHistoryObjects.startMenuElements.SetActive(false);
                RulesAndHistoryObjects.exceptionMsg.GetComponent<Text>().text
                    = Langs.GetMessge("NETWORK_ERROR");

                yield break;
            }
            else
            {
                Main.safetyCards = response.cards.safety;
                Main.dangerousCards = response.cards.dangerous;
                Main.book = response.book;

                SceneManager.LoadScene("Game");
            }
        }
    }
}
