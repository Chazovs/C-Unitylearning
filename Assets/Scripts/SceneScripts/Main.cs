using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
   
    public static Timer timer;

    void Start()
    {
        Application.targetFrameRate = Settings.defaultFramRate;

        Destroy(GameObject.Find("splashMusic"));

        new GameObjects();

        Langs.SetLangsForMain();

        timer = new Timer();

        GameObjects.toggleMuteBtn.GetComponent<Button>()
            .onClick.AddListener(() => ServiceLocator.GetService<ButtonService>().toggleMuteHandler());

        ServiceLocator.GetService<CardService>().HideCard();
        ServiceLocator.GetService<GameFieldService>().FillGameFields();

        //рантайм тесты
        FieldTest.showField(); //показать все поле с координатами и карточками
    }

    void Update()
    {
        timer.UpdateTimer();
        ServiceLocator.GetService<HeroService>().Move();
    }
}
