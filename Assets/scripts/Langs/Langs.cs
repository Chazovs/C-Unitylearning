using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
