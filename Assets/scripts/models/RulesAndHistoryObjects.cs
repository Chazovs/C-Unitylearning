using UnityEngine;

class RulesAndHistoryObjects
{
    public static GameObject rightButton;
    public static GameObject leftButton;
    public static GameObject skipButton;
    public static GameObject rulesButton;
    public static GameObject historyButton;
    public static GameObject rulHisImage;
    public static GameObject magicBookUrl;
    public static GameObject magicBookTitle;
    public static GameObject startGameBtn;
    public static GameObject bookDropdown;
    public static GameObject qrCodeImage;
    public static GameObject mainCamera;
    public static GameObject newBooksBtn;
    public static GameObject startMenuElements;
    public static GameObject exceptionMsg;
    public static GameObject myBooksBtn;
    public static GameObject apiController;
    public static GameObject cardText;

    public static GameObject mainCanva;
    public static GameObject startMenu;
    public static GameObject bookSectionTitle;
    public static GameObject rulHisTitle;

    public RulesAndHistoryObjects()
    {
        rightButton = GameObject.Find("rightButton");
        leftButton = GameObject.Find("leftButton");
        skipButton = GameObject.Find("skipButton");
        rulesButton = GameObject.Find("rulesButton");
        historyButton = GameObject.Find("historyButton");
        rulHisImage = GameObject.Find("rulHisImage");
        magicBookUrl = GameObject.Find("magicBookUrl");
        magicBookTitle = GameObject.Find("magicBookTitle");
        startGameBtn = GameObject.Find("startGameBtn");
        bookDropdown = GameObject.Find("bookDropdown");
        qrCodeImage = GameObject.Find("qrCodeImage");
        newBooksBtn = GameObject.Find("newBooksBtn");
        myBooksBtn = GameObject.Find("myBooksBtn");
        apiController = GameObject.Find("apiController");
        cardText = GameObject.Find("cardText");
        rulHisTitle = GameObject.Find("rulHisTitle");

        mainCamera = GameObject.Find("mainCamera");
        startMenuElements = GameObject.Find("startMenuElements");

        mainCanva = GameObject.Find("mainCanva");
        startMenu = GameObject.Find("startMenu");
        bookSectionTitle = GameObject.Find("bookSectionTitle");
        exceptionMsg = GameObject.Find("exceptionMsg");
    }
}
