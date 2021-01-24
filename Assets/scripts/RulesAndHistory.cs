using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesAndHistory : MonoBehaviour
{
    private ButtonService buttonService;
    public string topic = "rules";
    public int currentSlide = 1;

    public int currentMyBook = 0;
    public int currentNewBook = 0;

    public List<Book> myBooks;
    public List<Book> newBooks;

    void Start()
    {
        new Translator(Constants.defaultLang);
        new RulesAndHistoryObjects();

        BookService bookService = new BookService();

        myBooks = bookService.GetBooks(Constants.myBooksType);
        newBooks = bookService.GetBooks(Constants.newBooksType);

        RulesAndHistoryObjects.startMenu.SetActive(false);

        buttonService = new ButtonService();

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

        //startGameBtn
        RulesAndHistoryObjects.startGameBtn.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.startGameBtnHandler());
    }
}
