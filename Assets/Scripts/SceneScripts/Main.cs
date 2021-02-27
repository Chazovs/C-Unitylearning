using UnityEngine;

public class Main : MonoBehaviour
{
   
    private Timer _timer;

    void Start()
    {
        Application.targetFrameRate = Settings.defaultFramRate;

        Destroy(GameObject.Find("splashMusic"));

        new GameObjects();

        Langs.SetLangsForMain();

        _timer = new Timer();
       
        ServiceLocator.GetService<HeroService>().SetHeroPosition();
        ServiceLocator.GetService<CardService>().HideCard();
        ServiceLocator.GetService<GameFieldService>().FillGameFields();

        //рантайм тесты
        FieldTest.showField(); //показать все поле с координатами и карточками
    }

    void Update()
    {
        _timer.UpdateTimer();
        ServiceLocator.GetService<HeroService>().Move();
    }
}
