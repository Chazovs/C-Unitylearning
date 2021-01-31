using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesAndHistory : MonoBehaviour
{
    public string topic = "rules";
    public int currentSlide = 1;

    public static int currentMyBook = 0;
    public static int currentNewBook = 0;

    public static List<Book> myBooks;
    public static List<Book> newBooks;
    
    private BookService bookService;
    private ButtonService buttonService;

    void Start()
    {
        Application.targetFrameRate = Settings.defaultFramRate;

        new RulesAndHistoryObjects();

        Langs.SetLangsForRules();

        
        RulesAndHistoryObjects.cardText.GetComponent<Text>().text = Langs.GetMessge("RULES_1");

        bookService = ServiceLocator.GetService<BookService>();
        buttonService = ServiceLocator.GetService<ButtonService>();

        myBooks = bookService.GetBooks(Constants.myBooksType);
        newBooks = bookService.GetBooks(Constants.newBooksType);

        RulesAndHistoryObjects.startMenu.SetActive(false);

        //leftButton
        RulesAndHistoryObjects.leftButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisLeftButtonHandler(topic, currentSlide));

        //rightButton
        RulesAndHistoryObjects.rightButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisRightButtonHandler(topic, currentSlide));

        //rulesButton
        RulesAndHistoryObjects.rulesButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisRulesButtonHandler());

        //historyButton
        RulesAndHistoryObjects.historyButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisHistoryButtonHandler());

        //skipButton
        RulesAndHistoryObjects.skipButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisSkipButtonHandler());

        //myBooksBtn
        RulesAndHistoryObjects.myBooksBtn.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.myBooksBtnHandler());

        //newBooksBtn
        RulesAndHistoryObjects.newBooksBtn.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.newBooksBtnHandler());
    }
}
