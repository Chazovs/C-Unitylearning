using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Langs
{
     internal static string GetMessge(string key)
    {
        string value = "";

        if (Translator.Fields.ContainsKey(key)) {
            value = Translator.Fields[key];
        }

        return value;
    }


    public static void SetLangsForRules()
    {
        RulesAndHistoryObjects.rulesButton.GetComponentInChildren<Text>().text 
            = GetMessge("RULES_BTN");
        RulesAndHistoryObjects.skipButton.GetComponentInChildren<Text>().text 
            = GetMessge("BOOKS_BTN");
        RulesAndHistoryObjects.historyButton.GetComponentInChildren<Text>().text 
            = GetMessge("HISTORY_BTN");
        RulesAndHistoryObjects.startGameBtn.GetComponentInChildren<Text>().text 
            = GetMessge("START_BTN");
        RulesAndHistoryObjects.myBooksBtn.GetComponentInChildren<Text>().text 
            = GetMessge("MY_BOOKS_BTN");
        RulesAndHistoryObjects.newBooksBtn.GetComponentInChildren<Text>().text 
            = GetMessge("NEW_BOOKS_BTN");
        RulesAndHistoryObjects.bookSectionTitle.GetComponentInChildren<Text>().text 
            = GetMessge("YOUR_BOOKS_TITLE");
    }

}
