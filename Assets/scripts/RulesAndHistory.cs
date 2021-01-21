using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RulesAndHistory : MonoBehaviour
{
    private RulesAndHistoryObjects rulesAndHistoryObjects;
    private ButtonService buttonService;
    public string topic = "rules";
    public int currentSlide = 1;
    void Start()
    {
        rulesAndHistoryObjects = new RulesAndHistoryObjects();

        rulesAndHistoryObjects.newBooks.SetActive(false);
        rulesAndHistoryObjects.myBooks.SetActive(false);
        rulesAndHistoryObjects.startMenu.SetActive(false);

        buttonService = new ButtonService();
        
        //leftButton
        rulesAndHistoryObjects.leftButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisLeftButtonHandler(topic,
            currentSlide,
            rulesAndHistoryObjects.rulHisImage,
            ref rulesAndHistoryObjects.magicBookUrl)
            );
        
        //rightButton
        rulesAndHistoryObjects.rightButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisRightButtonHandler(topic,
            currentSlide,
            rulesAndHistoryObjects.rulHisImage,
            ref rulesAndHistoryObjects.magicBookUrl)
            );

        //rulesButton
        rulesAndHistoryObjects.rulesButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisRulesButtonHandler(ref rulesAndHistoryObjects.rulHisImage));
        
        //historyButton
        rulesAndHistoryObjects.historyButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisHistoryButtonHandler(ref rulesAndHistoryObjects.rulHisImage));

        //skipButton
        rulesAndHistoryObjects.skipButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisSkipButtonHandler(ref rulesAndHistoryObjects));

        //magicBookUrl
        rulesAndHistoryObjects.magicBookUrl.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.openMagicBookUrl());


        //myBooksBtn
        rulesAndHistoryObjects.myBooksBtn.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.myBooksBtnHandler(
            ref rulesAndHistoryObjects.myBooks,
            ref rulesAndHistoryObjects.newBooks
            )
            );

        //newBooksBtn
        rulesAndHistoryObjects.newBooksBtn.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.newBooksBtnHandler(
            ref rulesAndHistoryObjects.myBooks,
            ref rulesAndHistoryObjects.newBooks
            )
            );

    }
}
