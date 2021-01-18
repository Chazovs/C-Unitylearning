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

        rulesAndHistoryObjects.magicBookUrl.SetActive(false);
        rulesAndHistoryObjects.inputVersion.SetActive(false);
        rulesAndHistoryObjects.checkVersionButton.SetActive(false);
        rulesAndHistoryObjects.magicBookTitle.SetActive(false);
        rulesAndHistoryObjects.versionNumberText.SetActive(false);


        buttonService = new ButtonService();

        rulesAndHistoryObjects.leftButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisLeftButtonHandler(topic,
            currentSlide,
            rulesAndHistoryObjects.rulHisImage,
            ref rulesAndHistoryObjects.magicBookUrl)
            );
        rulesAndHistoryObjects.rightButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisRightButtonHandler(topic,
            currentSlide,
            rulesAndHistoryObjects.rulHisImage,
            ref rulesAndHistoryObjects.magicBookUrl)
            );
        rulesAndHistoryObjects.rulesButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisRulesButtonHandler());
        rulesAndHistoryObjects.historyButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisHistoryButtonHandler());
        rulesAndHistoryObjects.skipButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisSkipButtonHandler(ref rulesAndHistoryObjects));
        rulesAndHistoryObjects.magicBookUrl.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.openMagicBookUrl());
    }
}
