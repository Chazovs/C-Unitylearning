using UnityEngine;

public class HeroService : MonoBehaviour
{
    private Vector2 _movementDirection;

    private Vector3 _destination = new Vector3();
    private Vector3 _startField;
    private Vector3 _endField;

    private GameObject hero;
    private float _horizontVector = 0;
    private float _verticalVector = 0;
    private Main mainComponent;
    private KeyInput keyInput = new KeyInput();

    public bool isInputBlocked = false;
    Vector3 nullPosition;

    public HeroService(ref GameObjects gameObjects)
    {
        hero = gameObjects.hero;
        mainComponent = gameObjects.main.GetComponent<Main>();

        hero.transform.position = new Vector3(
            hero.transform.position.x + (Constants.step / 2),
            hero.transform.position.y - (Constants.step / 2) + (Constants.step * Constants.fieldSize),
            hero.transform.position.z
            );

        _startField = hero.transform.position;
        nullPosition = new Vector3() { x = Constants.fieldSize / 2 * Constants.step, y = Constants.fieldSize / 2 * Constants.step };
        _endField = new Vector3(
            _startField.x + Constants.step * (Constants.fieldSize - 1),
            _startField.y - Constants.step * (Constants.fieldSize - 1),
            hero.transform.position.z
            );

        _destination = hero.transform.position;
    }


    public void Move()
    {
        mainComponent.serviceLocator.cardService.ShowController();

        if (mainComponent.isCardShowing) return;

        keyInput.x = keyInput.y = 0;

        //если герой не движется, то можно принимать значение ввода
        if (!mainComponent.heroPosition.onTheWay) {
            keyInput.x = Input.GetAxisRaw("Horizontal");
            keyInput.y = Input.GetAxisRaw("Vertical");
        };

        if(keyInput.x != 0 || keyInput.y != 0)
        {
            mainComponent.heroPosition.onTheWay = true;

            mainComponent.newPosition = new Position
            {
                x = mainComponent.heroPosition.x + keyInput.x,
                y = mainComponent.heroPosition.y + keyInput.y
            };

            bool canMove = mainComponent.newPosition.x > 0
            && mainComponent.newPosition.x < 11
            && mainComponent.newPosition.y > 0
            && mainComponent.newPosition.y < 11;

            if (!canMove) {
                mainComponent.heroPosition.onTheWay = false;
                return;
            }

            _movementDirection = Vector2.zero;

            _movementDirection.Set(keyInput.x * Constants.step, keyInput.y * Constants.step);

            _destination = hero.transform.position + (Vector3)_movementDirection;
        }

        //если туда можно двигаться и если мы еще не там - идем туда
        if (_destination != hero.transform.position) {
            mainComponent.heroPosition.onTheWay = true;
            hero.transform.position = Vector3.MoveTowards(hero.transform.position, _destination, Constants.speed * Time.deltaTime);

            return;
        }

        //если мы пришли
        if (mainComponent.heroPosition.onTheWay && hero.transform.position == _destination)
        {
            mainComponent.previousPosition = mainComponent.heroPosition;
            mainComponent.heroPosition = mainComponent.newPosition;
            mainComponent.heroPosition.onTheWay = false;

            return;
        }
    }

    public void goBack()
    {
        /*        Debug.Log(previousPosition.x);
                Debug.Log(previousPosition.y);
                Debug.Log("ssssssssssssssssssss");
                Debug.Log(heroPosition.x);
                Debug.Log(heroPosition.y);*/

        if (_destination == hero.transform.position)
        {

            _movementDirection.Set(
            (mainComponent.previousPosition.x - mainComponent.heroPosition.x) * Constants.step,
            (mainComponent.previousPosition.y - mainComponent.heroPosition.y) * Constants.step
            );

        _destination = hero.transform.position + (Vector3)_movementDirection;

        hero.transform.position = Vector3.MoveTowards(hero.transform.position, _destination, Constants.speed * Time.deltaTime);
        
            _movementDirection.Set(
            (mainComponent.previousPosition.x - mainComponent.heroPosition.x) * Constants.step,
            (mainComponent.previousPosition.y - mainComponent.heroPosition.y) * Constants.step
            );

            _destination = hero.transform.position + (Vector3)_movementDirection;

            hero.transform.position = Vector3.MoveTowards(hero.transform.position, _destination, Constants.speed * Time.deltaTime);

            mainComponent.heroPosition.onTheWay = false;
            mainComponent.heroPosition.x = mainComponent.previousPosition.x;
            mainComponent.heroPosition.y = mainComponent.previousPosition.y;
    }
        else
        {
            mainComponent.heroPosition.onTheWay = true;
        }
    }
}
