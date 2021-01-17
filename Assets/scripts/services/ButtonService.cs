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

    internal void rulHisLeftButtonHandler(string topic, int currentSlide, GameObject img)
    {
        int nextSlide = currentSlide -1;

        if (topic == "rules") {
            nextSlide = nextSlide == 0 ? Constants.lastRulesSlide : nextSlide;
        }
        
        if(topic == "history") {
            nextSlide = nextSlide == 0 ? Constants.lastHistorySlide : nextSlide;
        }
        GameObject.Find("Main Camera").GetComponent<RulesAndHistory>().currentSlide = nextSlide;
        img.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/" +topic + "/" + nextSlide.ToString());
    }

    internal void rulHisRightButtonHandler(string topic, int currentSlide, GameObject img)
    {
        int nextSlide = currentSlide + 1;

        if (topic == "rules")
        {
            nextSlide = nextSlide > Constants.lastRulesSlide ? 1 : nextSlide;
        }

        if (topic == "history")
        {
            nextSlide = nextSlide > Constants.lastHistorySlide ? 1 : nextSlide;
        }
        GameObject.Find("Main Camera").GetComponent<RulesAndHistory>().currentSlide = nextSlide;
        img.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/" + topic + "/" + nextSlide.ToString());
    }

    internal void rulHisRulesButtonHandler()
    {
        throw new NotImplementedException();
    }

    internal void rulHisHistoryButtonHandler()
    {
        throw new NotImplementedException();
    }

    internal void rulHisSkipButtonHandler()
    {
        Debug.Log("я здесь");
        SceneManager.LoadScene("Main_Menu");
    }
}
