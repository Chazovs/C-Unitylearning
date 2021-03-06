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

    private void Start()
    {
        ServiceLocator.Reset();

        Application.targetFrameRate = Settings.defaultFramRate;

        new MenuObjects();

        Langs.SetLangsForMenu();

        MenuObjects.cardText.GetComponent<Text>().text = Langs.GetMessge("RULES_1");

        ButtonService buttonService = ServiceLocator.GetService<ButtonService>();
        BookService bookService = ServiceLocator.GetService<BookService>();

        myBooks = bookService.GetBooks(Constants.myBooksType);
        newBooks = bookService.GetBooks(Constants.newBooksType);

        MenuObjects.startMenu.SetActive(false);
        MenuObjects.happyEnd.SetActive(false);
        
        if (Settings.endType != "")
        {
            MenuObjects.rightButton.SetActive(false);
            MenuObjects.leftButton.SetActive(false);
            MenuObjects.rulHisTitle.SetActive(false);

            if (Settings.endType == "sadEnd")
            {
                MenuObjects.cardText.GetComponent<Text>().text
               = Langs.GetMessge(Settings.endType);
                MenuObjects.rulHisImage.GetComponent<Image>().sprite
                    = Resources.Load<Sprite>("img/endimg/" + Settings.endType);
                MenuObjects.losingSound.GetComponent<AudioSource>().Play();
            }

            if (Settings.endType == "happyEnd")
            {
                MenuObjects.cardText.SetActive(false);
                MenuObjects.rulHisImage.SetActive(false);
                MenuObjects.cardImage.SetActive(false);

                MenuObjects.happyEndText.GetComponent<Text>().text
              = Langs.GetMessge(Settings.endType);

                MenuObjects.happyEnd.SetActive(true);
                MenuObjects.winSound.GetComponent<AudioSource>().Play();
            }
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