using UnityEngine;
using UnityEngine.UI;

public class PreviewScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ButtonService buttonService = ServiceLocator.GetService<ButtonService>();

        GameObject.Find("russian").GetComponent<Button>()
           .onClick.AddListener(() => buttonService.setLangHandler("ru"));

        GameObject.Find("english").GetComponent<Button>()
           .onClick.AddListener(() => buttonService.setLangHandler("en"));
    }
}
