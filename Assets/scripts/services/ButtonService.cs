using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonService : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    internal void rulHisLeftButtonHandler(string topic, int currentSlide)
    {
        int nextSlide = currentSlide -1;

        if (topic == "rules") {
            nextSlide = nextSlide == 0 ? Constants.lastRulesSlide : nextSlide;
            if (nextSlide == 6)
            {
                RulesAndHistoryObjects.magicBookUrl.SetActive(true);
            }
            else
            {
                RulesAndHistoryObjects.magicBookUrl.SetActive(false);
            }
        }
        
        if(topic == "history") {
            nextSlide = nextSlide == 0 ? Constants.lastHistorySlide : nextSlide;
        }
        GameObject.Find("mainCamera").GetComponent<RulesAndHistory>().currentSlide = nextSlide;
        RulesAndHistoryObjects.rulHisImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/" +topic + "/" + nextSlide.ToString());
    }

    internal void rulHisRightButtonHandler(string topic, int currentSlide)
    {
        int nextSlide = currentSlide + 1;

        if (topic == "rules")
        {
            nextSlide = nextSlide > Constants.lastRulesSlide ? 1 : nextSlide;
            if (nextSlide == 6)
            {
                RulesAndHistoryObjects.magicBookUrl.SetActive(true);
            }
            else
            {
                RulesAndHistoryObjects.magicBookUrl.SetActive(false);
            }
        }

        if (topic == "history")
        {
            nextSlide = nextSlide > Constants.lastHistorySlide ? 1 : nextSlide;
        }

        GameObject.Find("mainCamera").GetComponent<RulesAndHistory>().currentSlide = nextSlide;
        RulesAndHistoryObjects.rulHisImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/" + topic + "/" + nextSlide.ToString());
    }

    internal static void openMagicBookUrl(string url)
    {
        Application.OpenURL(url);
    }

    internal void rulHisRulesButtonHandler()
    {
        RulesAndHistory component = GameObject.Find("mainCamera").GetComponent<RulesAndHistory>();
        component.currentSlide = 1;
        component.topic = "rules";
        RulesAndHistoryObjects.rulHisImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/rules/1");
    }

    internal void startGameBtnHandler()
    {
        SceneManager.LoadScene("Game");
    }

    internal void myBooksBtnHandler()
    {
        RulesAndHistory rulesAndHistory
          = GameObject.Find("mainCamera").GetComponent<RulesAndHistory>();

        if (rulesAndHistory.myBooks.Count == 0)
        {
            RulesAndHistoryObjects.startMenuElements.SetActive(false);
            RulesAndHistoryObjects.exceptionMsg.GetComponent<Text>().text = Langs.GetMessge("NO_MY_BOOKS");
            RulesAndHistoryObjects.exceptionMsg.SetActive(true);
        }

        if (rulesAndHistory.myBooks.Count > 0)
        {
            RulesAndHistoryObjects.startMenuElements.SetActive(true);
            RulesAndHistoryObjects.exceptionMsg.SetActive(false);
        }

        BookService bookService = new BookService();
        bookService.setCurrentMyBook();
    }

    internal void rulHisHistoryButtonHandler()
    {
        RulesAndHistory component = GameObject.Find("mainCamera").GetComponent<RulesAndHistory>();
        component.currentSlide = 1;
        component.topic = "history";
        RulesAndHistoryObjects.rulHisImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/history/1");
    }

    internal void newBooksBtnHandler()
    {
        RulesAndHistory rulesAndHistory 
            = GameObject.Find("mainCamera").GetComponent<RulesAndHistory>();

        if (rulesAndHistory.newBooks.Count == 0)
        {
            RulesAndHistoryObjects.startMenuElements.SetActive(false);
            RulesAndHistoryObjects.exceptionMsg.GetComponent<Text>().text = Langs.GetMessge("NO_NEW_BOOKS");
            RulesAndHistoryObjects.exceptionMsg.SetActive(true);
        }

        if (rulesAndHistory.newBooks.Count > 0)
        {
            RulesAndHistoryObjects.startMenuElements.SetActive(true);
            RulesAndHistoryObjects.exceptionMsg.SetActive(false);
        }
    }

    internal void rulHisSkipButtonHandler()
    {
        BookService bookService = new BookService();
        bookService.setCurrentMyBook();
        bookService.SetMyBooksDropDown();

        RulesAndHistoryObjects.mainCanva.SetActive(false);
        RulesAndHistoryObjects.startMenu.SetActive(true);
    }
}
