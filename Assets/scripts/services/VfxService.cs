using UnityEngine;

public class VfxService
{
    private static GameObject mainLogo;

    internal static void changeLogo()
    {
        mainLogo = GameObject.Find("mainLogo");

        if (First.goChangeLogo)
        {
            mainLogo.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, (float)First.logoTransparency / 100);

            if (First.showLogoVector == 1 && First.logoTransparency >= 0)
                First.logoTransparency -= 5;

            if (First.logoTransparency == 0 && First.showLogoVector == 1)
            {
                First.showLogoVector = -1;
                mainLogo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("img/logo/" + Settings.logoLang);
            }

            if (First.showLogoVector == -1)
                First.logoTransparency += 5;

            if (First.logoTransparency == 100 && First.showLogoVector == -1)
            {
                First.goChangeLogo = false;
                First.showLogoVector = 1;
            }
        }
    }
}
