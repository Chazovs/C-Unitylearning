using UnityEngine;

class RulesAndHistoryObjects
{
    public GameObject rightButton;
    public GameObject leftButton;
    public GameObject skipButton;
    public GameObject rulesButton;
    public GameObject historyButton;
    public GameObject rulHisImage;
    public GameObject magicBookUrl;
    public GameObject inputVersion;
    public GameObject checkVersionButton;
    public GameObject magicBookTitle;
    public GameObject versionNumberText;
    public GameObject myBooksBtn;
    public GameObject newBooksBtn;

    public GameObject mainCanva;
    public GameObject startMenu;
    public GameObject myBooks;
    public GameObject newBooks;

    public RulesAndHistoryObjects()
    {
        rightButton = GameObject.Find("rightButton");
        leftButton = GameObject.Find("leftButton");
        skipButton = GameObject.Find("skipButton");
        rulesButton = GameObject.Find("rulesButton");
        historyButton = GameObject.Find("historyButton");
        rulHisImage = GameObject.Find("rulHisImage");
        magicBookUrl = GameObject.Find("magicBookUrl");
        inputVersion = GameObject.Find("inputVersion");
        checkVersionButton = GameObject.Find("checkVersionButton");
        magicBookTitle = GameObject.Find("magicBookTitle");
        versionNumberText = GameObject.Find("versionNumberText");
        newBooksBtn = GameObject.Find("newBooksBtn");
        myBooksBtn = GameObject.Find("myBooksBtn");

        mainCanva = GameObject.Find("mainCanva");
        startMenu = GameObject.Find("startMenu");
        myBooks = GameObject.Find("myBooks");
        newBooks = GameObject.Find("newBooks");
    }
}
