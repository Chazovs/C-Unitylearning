using System;
using UnityEngine;
using UnityEngine.UI;

public class Langs
{
    internal static string GetMessge(string key)
    {
        string value = "";

        if (Translator.Fields.ContainsKey(key))
        {
            value = Translator.Fields[key];
        }

        return value;
    }

    public static void SetLangsForMenu()
    {
        MenuObjects.rulesButton.GetComponentInChildren<Text>().text
            = GetMessge("RULES_BTN");
        MenuObjects.skipButton.GetComponentInChildren<Text>().text
            = GetMessge("BOOKS_BTN");
        MenuObjects.historyButton.GetComponentInChildren<Text>().text
            = GetMessge("HISTORY_BTN");
        MenuObjects.startGameBtn.GetComponentInChildren<Text>().text
            = GetMessge("START_BTN");
        MenuObjects.bookSectionTitle.GetComponentInChildren<Text>().text
            = GetMessge("YOUR_BOOKS_TITLE");
        MenuObjects.rulHisTitle.GetComponentInChildren<Text>().text
            = GetMessge("RULES_TITLE");
    }

    internal static void SetLangsForMain()
    {
        GameObjects.backCardButton.GetComponentInChildren<Text>().text
             = GetMessge("BACK");
        GameObjects.goCardButton.GetComponentInChildren<Text>().text
             = GetMessge("GO");
    }

    public static void SetLangsForEnd()
    {
        GameObject.Find("english").GetComponentInChildren<Text>().text
           = GetMessge("RESTART");
    }
}
