using UnityEngine;
using UnityEngine.UI;

public class First : MonoBehaviour
{

   public static bool goChangeLogo = false;
   public static int logoTransparency = 100;
   public static int showLogoVector = 1;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = Settings.defaultFramRate;

        ButtonService buttonService = ServiceLocator.GetService<ButtonService>();

        GameObject.Find("russian").GetComponent<Button>()
           .onClick.AddListener(() => buttonService.setLangHandler("ru"));

        GameObject.Find("english").GetComponent<Button>()
           .onClick.AddListener(() => buttonService.setLangHandler("en"));
    }

    void Update()
    {
        VfxService.changeLogo();
    }
}
