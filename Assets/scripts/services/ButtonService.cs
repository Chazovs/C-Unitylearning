using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ButtonService : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    internal void rulHisLeftButtonHandler(string topic, int currentSlide, GameObject img, ref GameObject gameURL)
    {
        int nextSlide = currentSlide -1;

        if (topic == "rules") {
            nextSlide = nextSlide == 0 ? Constants.lastRulesSlide : nextSlide;
            if (nextSlide == 6)
            {
                gameURL.SetActive(true);
            }
            else
            {
                gameURL.SetActive(false);
            }
        }
        
        if(topic == "history") {
            nextSlide = nextSlide == 0 ? Constants.lastHistorySlide : nextSlide;
        }
        GameObject.Find("Main Camera").GetComponent<RulesAndHistory>().currentSlide = nextSlide;
        img.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/" +topic + "/" + nextSlide.ToString());
    }

    internal void rulHisRightButtonHandler(string topic, int currentSlide, GameObject img, ref GameObject gameURL)
    {
        int nextSlide = currentSlide + 1;

        if (topic == "rules")
        {
            nextSlide = nextSlide > Constants.lastRulesSlide ? 1 : nextSlide;
            if (nextSlide == 6)
            {
                gameURL.SetActive(true);
            }
            else
            {
                gameURL.SetActive(false);
            }
        }

        if (topic == "history")
        {
            nextSlide = nextSlide > Constants.lastHistorySlide ? 1 : nextSlide;
        }
        GameObject.Find("Main Camera").GetComponent<RulesAndHistory>().currentSlide = nextSlide;
        img.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/" + topic + "/" + nextSlide.ToString());
    }

    internal void openMagicBookUrl()
    {
        Application.OpenURL(Constants.serverUrl);
    }

    internal void rulHisRulesButtonHandler(ref GameObject img)
    {
        RulesAndHistory component = GameObject.Find("Main Camera").GetComponent<RulesAndHistory>();
        component.currentSlide = 1;
        component.topic = "rules";
        img.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/rules/1");
    }

    internal void myBooksBtnHandler(ref GameObject myBooks, ref GameObject newBooks)
    {
        myBooks.SetActive(true);
        newBooks.SetActive(false);
    }

    internal void rulHisHistoryButtonHandler(ref GameObject img)
    {
        RulesAndHistory component = GameObject.Find("Main Camera").GetComponent<RulesAndHistory>();
        component.currentSlide = 1;
        component.topic = "history";
        img.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/history/1");
    }

    internal void newBooksBtnHandler(ref GameObject myBooks, ref GameObject newBooks)
    {
        myBooks.SetActive(false);
        newBooks.SetActive(true);
    }

    internal void rulHisSkipButtonHandler(ref RulesAndHistoryObjects rulesAndHistoryObjects)
    {
        rulesAndHistoryObjects.mainCanva.SetActive(false);
        rulesAndHistoryObjects.startMenu.SetActive(true);
        rulesAndHistoryObjects.myBooks.SetActive(true);
    }
}
