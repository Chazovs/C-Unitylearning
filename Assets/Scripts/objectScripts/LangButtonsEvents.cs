using UnityEngine;
using UnityEngine.EventSystems;

public class LangButtonsEvents : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Settings.logoLang == "ru" && name == "english")
        {
            Settings.logoLang = "en";
            First.goChangeLogo = true;
        }

        if (Settings.logoLang == "en" && name == "russian")
        {
            Settings.logoLang = "ru";
            First.goChangeLogo = true;
        }
    }
}
