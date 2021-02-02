using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public string topic = "rules";
    public int currentSlide = 1;

    public static int currentMyBook = 0;
    public static int currentNewBook = 0;

    public static List<Book> myBooks;
    public static List<Book> newBooks;

    private BookService bookService;
    private ButtonService buttonService;

    private void Start()
    {
        Application.targetFrameRate = Settings.defaultFramRate;

        new MenuObjects();

        Langs.SetLangsForRules();

        MenuObjects.cardText.GetComponent<Text>().text = Langs.GetMessge("RULES_1");

        bookService = ServiceLocator.GetService<BookService>();
        buttonService = ServiceLocator.GetService<ButtonService>();

        myBooks = bookService.GetBooks(Constants.myBooksType);
        newBooks = bookService.GetBooks(Constants.newBooksType);

        MenuObjects.startMenu.SetActive(false);

        if (Settings.endType != "")
        {
            MenuObjects.rightButton.SetActive(false);
            MenuObjects.leftButton.SetActive(false);
            MenuObjects.rulHisTitle.SetActive(false);

            MenuObjects.cardText.GetComponent<Text>().text
                = Langs.GetMessge(Settings.endType);
            MenuObjects.rulHisImage.GetComponent<Image>().sprite
                = Resources.Load<Sprite>("img/endimg/" + Settings.endType);
        }

        //leftButton
        MenuObjects.leftButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisLeftButtonHandler(topic, currentSlide));

        //rightButton
        MenuObjects.rightButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisRightButtonHandler(topic, currentSlide));

        //rulesButton
        MenuObjects.rulesButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisRulesButtonHandler());

        //historyButton
        MenuObjects.historyButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisHistoryButtonHandler());

        //skipButton
        MenuObjects.skipButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisSkipButtonHandler());

        //myBooksBtn
        MenuObjects.myBooksBtn.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.myBooksBtnHandler());

        //newBooksBtn
        MenuObjects.newBooksBtn.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.newBooksBtnHandler());
    }
}