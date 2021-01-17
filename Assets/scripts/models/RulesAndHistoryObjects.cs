using UnityEngine;

class RulesAndHistoryObjects
{
    public GameObject rightButton;
    public GameObject leftButton;
    public GameObject skipButton;
    public GameObject rulesButton;
    public GameObject historyButton;
    public GameObject rulHisImage;
    public RulesAndHistoryObjects()
    {
        rightButton = GameObject.Find("rightButton");
        leftButton = GameObject.Find("leftButton");
        skipButton = GameObject.Find("skipButton");
        rulesButton = GameObject.Find("rulesButton");
        historyButton = GameObject.Find("historyButton");
        rulHisImage = GameObject.Find("rulHisImage");
    }
}
