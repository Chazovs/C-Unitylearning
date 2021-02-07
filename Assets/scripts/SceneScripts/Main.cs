using UnityEngine;

public class Main : MonoBehaviour
{
   
    private Timer _timer;

    void Start()
    {
        Application.targetFrameRate = Settings.defaultFramRate;

        new GameObjects();

        Langs.SetLangsForMain();

        _timer = new Timer();

        ServiceLocator.GetService<CardService>().HideCard();
        ServiceLocator.GetService<GameFieldService>().SetGoal();
        ServiceLocator.GetService<GameFieldService>().FillGameFields();

        //рантайм тесты
        /*FieldTest.showField();*/ //показать все поле с координатами и карточками
    }

    void Update()
    {
        _timer.UpdateTimer();
        ServiceLocator.GetService<HeroService>().Move();
    }
}
