using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ButtonService : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    internal void rulHisLeftButtonHandler(string topic, int currentSlide, GameObject img, ref GameObject gameURL)
    {
        int nextSlide = currentSlide -1;

        if (topic == "rules") {
            nextSlide = nextSlide == 0 ? Constants.lastRulesSlide : nextSlide;
            if (nextSlide == 6)
            {
                gameURL.SetActive(true);
            }
            else
            {
                gameURL.SetActive(false);
            }
        }
        
        if(topic == "history") {
            nextSlide = nextSlide == 0 ? Constants.lastHistorySlide : nextSlide;
        }
        GameObject.Find("Main Camera").GetComponent<RulesAndHistory>().currentSlide = nextSlide;
        img.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/" +topic + "/" + nextSlide.ToString());
    }

    internal void rulHisRightButtonHandler(string topic, int currentSlide, GameObject img, ref GameObject gameURL)
    {
        int nextSlide = currentSlide + 1;

        if (topic == "rules")
        {
            nextSlide = nextSlide > Constants.lastRulesSlide ? 1 : nextSlide;
            if (nextSlide == 6)
            {
                gameURL.SetActive(true);
            }
            else
            {
                gameURL.SetActive(false);
            }
        }

        if (topic == "history")
        {
            nextSlide = nextSlide > Constants.lastHistorySlide ? 1 : nextSlide;
        }
        GameObject.Find("Main Camera").GetComponent<RulesAndHistory>().currentSlide = nextSlide;
        img.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/" + topic + "/" + nextSlide.ToString());
    }

    internal void openMagicBookUrl()
    {
        Application.OpenURL(Constants.magicBookUrl);
    }

    internal void rulHisRulesButtonHandler()
    {
        throw new NotImplementedException();
    }

    internal void rulHisHistoryButtonHandler()
    {
        throw new NotImplementedException();
    }

    internal void rulHisSkipButtonHandler(ref RulesAndHistoryObjects rulesAndHistoryObjects)
    {
        rulesAndHistoryObjects.leftButton.SetActive(false);
        rulesAndHistoryObjects.rightButton.SetActive(false);
        rulesAndHistoryObjects.rulesButton.SetActive(false);
        rulesAndHistoryObjects.historyButton.SetActive(false);
        rulesAndHistoryObjects.skipButton.SetActive(false);
        rulesAndHistoryObjects.magicBookUrl.SetActive(true);
        rulesAndHistoryObjects.rulHisImage.GetComponent<Image>().enabled = false;
        
        rulesAndHistoryObjects.checkVersionButton.SetActive(true);
        rulesAndHistoryObjects.magicBookTitle.SetActive(true);
        rulesAndHistoryObjects.versionNumberText.SetActive(true);
        rulesAndHistoryObjects.inputVersion.SetActive(true);
    }
}
