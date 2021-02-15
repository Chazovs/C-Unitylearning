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

        GameObject.Find("mainCamera").GetComponent<Menu>().currentSlide = nextSlide;
        MenuObjects.rulHisImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/" +topic + "/" + nextSlide.ToString());

        MenuObjects.cardText.GetComponent<Text>().text = Langs.GetMessge(nextSlideText);
    }

    internal void restartHandler()
    {
        SceneManager.LoadScene("Game");
    }

    internal void setLangHandler(string lang)
    {
        Settings.currentLang = lang;

        new Translator(lang);
        SceneManager.LoadScene("Menu");
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

        MenuObjects.mainCamera.GetComponent<Menu>().currentSlide = nextSlide;
        MenuObjects.rulHisImage.GetComponent<Image>().sprite 
            = Resources.Load<Sprite>("img/" + topic + "/" + nextSlide.ToString());

        MenuObjects.cardText.GetComponent<Text>().text = Langs.GetMessge(nextSlideText);
    }

    internal static void openMagicBookUrl(string url)
    {
        Application.OpenURL(url);
    }

    internal void rulHisRulesButtonHandler()
    {
        MenuObjects.mainCanva.SetActive(true);
        MenuObjects.startMenu.SetActive(false);
        MenuObjects.rightButton.SetActive(true);
        MenuObjects.leftButton.SetActive(true);
        MenuObjects.rulHisTitle.SetActive(true);

        Menu component = GameObject.Find("mainCamera").GetComponent<Menu>();
        component.currentSlide = 1;
        component.topic = "rules";
        MenuObjects.rulHisImage.GetComponent<Image>().sprite 
            = Resources.Load<Sprite>("img/rules/1");
        MenuObjects.cardText.GetComponent<Text>().text = Langs.GetMessge("RULES_1");
        MenuObjects.rulHisTitle.GetComponent<Text>().text = Langs.GetMessge("RULES_TITLE");
    }

    public static void startGameBtnHandler()
    {
        int index = Menu.currentMyBook;
        Book book = Menu.myBooks[index];

        MenuObjects
            .apiController
            .GetComponent<ApiController>()
            .loadCardsAction(Constants.defaultBook);
    }

    internal void myBooksBtnHandler()
    {
        if (Menu.myBooks.Count == 0)
        {
            MenuObjects.startMenuElements.SetActive(false);
            MenuObjects.exceptionMsg.GetComponent<Text>().text 
                = Langs.GetMessge("NO_MY_BOOKS");
            MenuObjects.exceptionMsg.SetActive(true);
        }

        if (Menu.myBooks.Count > 0)
        {
            MenuObjects.startMenuElements.SetActive(true);
            MenuObjects.exceptionMsg.SetActive(false);
        }

        BookService bookService = new BookService();
        bookService.setCurrentMyBook();
    }

    internal void rulHisHistoryButtonHandler()
    {
        MenuObjects.mainCanva.SetActive(true);
        MenuObjects.startMenu.SetActive(false);
        MenuObjects.rightButton.SetActive(true);
        MenuObjects.leftButton.SetActive(true);
        MenuObjects.rulHisTitle.SetActive(true);

        Menu component = GameObject.Find("mainCamera").GetComponent<Menu>();
        component.currentSlide = 1;
        component.topic = "history";
        MenuObjects.rulHisImage.GetComponent<Image>().sprite 
            = Resources.Load<Sprite>("img/history/1");
        MenuObjects.cardText.GetComponent<Text>().text = Langs.GetMessge("HISTORY_1");
        MenuObjects.rulHisTitle.GetComponent<Text>().text = Langs.GetMessge("HISTORY_TITLE");
    }

    internal void newBooksBtnHandler()
    {
        if (Menu.newBooks.Count == 0)
        {
            MenuObjects.startMenuElements.SetActive(false);

            MenuObjects.exceptionMsg.GetComponent<Text>().text 
                = Langs.GetMessge("NO_NEW_BOOKS");

            MenuObjects.exceptionMsg.SetActive(true);
        }

        if (Menu.newBooks.Count > 0)
        {
            MenuObjects.startMenuElements.SetActive(true);
            MenuObjects.exceptionMsg.SetActive(false);
        }
    }

    internal void rulHisSkipButtonHandler()
    {
        BookService bookService = ServiceLocator.GetService<BookService>();

        bookService.setCurrentMyBook();
        bookService.SetMyBooksDropDown();

        MenuObjects.mainCanva.SetActive(false);
        MenuObjects.startMenu.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Settings.logoLang == "ru" && name == "english")
        {
            Settings.logoLang = "en";
            First.goChangeLogo = true;
        }

        if (Settings.logoLang == "en" && name == "russian")
        {
            Settings.logoLang = "ru";
            First.goChangeLogo = true;
        }
    }
}
