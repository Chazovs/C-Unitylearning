using UnityEngine;

public class VfxService
{
    private static GameObject mainLogo;

    internal static void changeLogo()
    {
        mainLogo = GameObject.Find("mainLogo");

        if (PreviewScreen.goChangeLogo)
        {
            mainLogo.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, (float)PreviewScreen.logoTransparency / 100);

            if (PreviewScreen.showLogoVector == 1 && PreviewScreen.logoTransparency >= 0)
                PreviewScreen.logoTransparency -= 5;

            if (PreviewScreen.logoTransparency == 0 && PreviewScreen.showLogoVector == 1)
            {
                PreviewScreen.showLogoVector = -1;
                mainLogo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("img/logo/" + Settings.logoLang);
            }

            if (PreviewScreen.showLogoVector == -1)
                PreviewScreen.logoTransparency += 5;

            if (PreviewScreen.logoTransparency == 100 && PreviewScreen.showLogoVector == -1)
            {
                PreviewScreen.goChangeLogo = false;
                PreviewScreen.showLogoVector = 1;
            }
        }
    }
}
