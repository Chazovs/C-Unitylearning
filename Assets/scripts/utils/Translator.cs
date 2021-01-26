/*
 * Internationalization 
 * 
 * Author: Daniel Erdmann
 */

using System;
using System.Collections.Generic;
using UnityEngine;

class Translator
{
    public static Dictionary<String, String> Fields { get; private set; }

    public Translator(string lang)
    {
        LoadLanguage(lang);
    }

    private static void LoadLanguage(string lang)
    {
        if (Fields == null)
        {
            Fields = new Dictionary<string, string>();
        }

        Fields.Clear();
        
        var textAsset = Resources.Load(@"lang/" + lang);
        string allTexts = "";
        if (textAsset == null)
            textAsset = Resources.Load(@"lang/ru") as TextAsset;
        if (textAsset == null)
            Debug.LogError("File not found for lang: Assets/Resources/lang/" + lang + ".txt");
        allTexts = (textAsset as TextAsset).text;
        string[] lines = allTexts.Split(new string[] { "\r\n", "\n" },
            StringSplitOptions.None);
        string key, value;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].IndexOf("=") >= 0 && !lines[i].StartsWith("#"))
            {
                key = lines[i].Substring(0, lines[i].IndexOf("="));
                value = lines[i].Substring(lines[i].IndexOf("=") + 1,
                        lines[i].Length - lines[i].IndexOf("=") - 1).Replace("\\n", Environment.NewLine);
                Fields.Add(key, value);
            }
        }
    }
}
