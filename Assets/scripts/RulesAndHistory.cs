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
        buttonService = new ButtonService();

        rulesAndHistoryObjects.leftButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisLeftButtonHandler(topic, currentSlide, rulesAndHistoryObjects.rulHisImage));
        rulesAndHistoryObjects.rightButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisRightButtonHandler(topic, currentSlide, rulesAndHistoryObjects.rulHisImage));
        rulesAndHistoryObjects.rulesButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisRulesButtonHandler());
        rulesAndHistoryObjects.historyButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisHistoryButtonHandler());
        rulesAndHistoryObjects.skipButton.GetComponent<Button>()
            .onClick.AddListener(() => buttonService.rulHisSkipButtonHandler());
    }
}
