using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonService : MonoBehaviour, IPointerEnterHandler
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    internal void rulHisLeftButtonHandler(string topic, int currentSlide)
    {
        int nextSlide = currentSlide -1;
        string nextSlideText = "";

        if (topic == "rules") {
            nextSlide = nextSlide == 0 ? Constants.lastRulesSlide : nextSlide;
            nextSlideText = "RULES_" + nextSlide.ToString();
        }
        
        if(topic == "history") {
            nextSlide = nextSlide == 0 ? Constants.lastHistorySlide : nextSlide;
            nextSlideText = "HISTORY_" + nextSlide.ToString();
        }

        GameObject.Find("mainCamera").GetComponent<RulesAndHistory>().currentSlide = nextSlide;
        RulesAndHistoryObjects.rulHisImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/" +topic + "/" + nextSlide.ToString());

        RulesAndHistoryObjects.cardText.GetComponent<Text>().text = Langs.GetMessge(nextSlideText);
    }

    internal void setLangHandler(string lang)
    {
        Settings.currentLang = lang;

        new Translator(lang);
        SceneManager.LoadScene("Rules");
    }

    internal void rulHisRightButtonHandler(string topic, int currentSlide)
    {
        int nextSlide = currentSlide + 1;
        string nextSlideText = "";

        if (topic == "rules")
        {
            nextSlide = nextSlide > Constants.lastRulesSlide ? 1 : nextSlide;
            nextSlideText = "RULES_" + nextSlide.ToString();
        }

        if (topic == "history")
        {
            nextSlide = nextSlide > Constants.lastHistorySlide ? 1 : nextSlide;
            nextSlideText = "HISTORY_" + nextSlide.ToString();
        }

        RulesAndHistoryObjects.mainCamera.GetComponent<RulesAndHistory>().currentSlide = nextSlide;
        RulesAndHistoryObjects.rulHisImage.GetComponent<Image>().sprite 
            = Resources.Load<Sprite>("img/" + topic + "/" + nextSlide.ToString());

        RulesAndHistoryObjects.cardText.GetComponent<Text>().text = Langs.GetMessge(nextSlideText);
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
        RulesAndHistoryObjects.rulHisImage.GetComponent<Image>().sprite 
            = Resources.Load<Sprite>("img/rules/1");
        RulesAndHistoryObjects.cardText.GetComponent<Text>().text = Langs.GetMessge("RULES_1");
        RulesAndHistoryObjects.rulHisTitle.GetComponent<Text>().text = Langs.GetMessge("RULES_TITLE");
    }

    public static void startGameBtnHandler()
    {
        int index = RulesAndHistory.currentMyBook;
        Book book = RulesAndHistory.myBooks[index];

        RulesAndHistoryObjects
            .apiController
            .GetComponent<ApiController>()
            .loadCardsAction(Constants.defaultBook);
    }

    internal void myBooksBtnHandler()
    {
        if (RulesAndHistory.myBooks.Count == 0)
        {
            RulesAndHistoryObjects.startMenuElements.SetActive(false);
            RulesAndHistoryObjects.exceptionMsg.GetComponent<Text>().text 
                = Langs.GetMessge("NO_MY_BOOKS");
            RulesAndHistoryObjects.exceptionMsg.SetActive(true);
        }

        if (RulesAndHistory.myBooks.Count > 0)
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
        RulesAndHistoryObjects.rulHisImage.GetComponent<Image>().sprite 
            = Resources.Load<Sprite>("img/history/1");
        RulesAndHistoryObjects.cardText.GetComponent<Text>().text = Langs.GetMessge("HISTORY_1");
        RulesAndHistoryObjects.rulHisTitle.GetComponent<Text>().text = Langs.GetMessge("HISTORY_TITLE");
    }

    internal void newBooksBtnHandler()
    {
        if (RulesAndHistory.newBooks.Count == 0)
        {
            RulesAndHistoryObjects.startMenuElements.SetActive(false);

            RulesAndHistoryObjects.exceptionMsg.GetComponent<Text>().text 
                = Langs.GetMessge("NO_NEW_BOOKS");

            RulesAndHistoryObjects.exceptionMsg.SetActive(true);
        }

        if (RulesAndHistory.newBooks.Count > 0)
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Settings.logoLang == "ru" && name == "english")
        {
            Settings.logoLang = "en";
            PreviewScreen.goChangeLogo = true;
        }

        if (Settings.logoLang == "en" && name == "russian")
        {
            Settings.logoLang = "ru";
            PreviewScreen.goChangeLogo = true;
        }
    }
}
