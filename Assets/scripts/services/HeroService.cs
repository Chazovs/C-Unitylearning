using UnityEngine;

public class HeroService : MonoBehaviour
{
    private Vector2 _movementDirection;

    private Vector3 _destination = new Vector3();

    private KeyInput keyInput = new KeyInput();



    public HeroService()
    {
        GameObjects.hero.transform.position = new Vector3(
            GameObjects.hero.transform.position.x + (Constants.step / 2),
            GameObjects.hero.transform.position.y - (Constants.step / 2) + (Constants.step * Constants.fieldSize),
            GameObjects.hero.transform.position.z
            );

        _destination = GameObjects.hero.transform.position;
    }

    public void Move()
    {
        CardService cardService = ServiceLocator.GetService<CardService>();

        cardService.ShowController();

        if (Main.isCardShowing) return;

        keyInput.x = keyInput.y = 0;

        //если герой не движется, то можно принимать значение ввода
        if (!Main.heroPosition.onTheWay) {
            keyInput.x = Input.GetAxisRaw("Horizontal");
            keyInput.y = Input.GetAxisRaw("Vertical");
        };

        if(keyInput.x != 0 || keyInput.y != 0)
        {
            Main.heroPosition.onTheWay = true;

            Main.newPosition = new Position
            {
                x = Main.heroPosition.x + keyInput.x,
                y = Main.heroPosition.y + keyInput.y
            };

            bool canMove = Main.newPosition.x > 0
            && Main.newPosition.x < 11
            && Main.newPosition.y > 0
            && Main.newPosition.y < 11;

            if (!canMove) {
                Main.heroPosition.onTheWay = false;
                return;
            }

            _movementDirection = Vector2.zero;

            _movementDirection.Set(keyInput.x * Constants.step, keyInput.y * Constants.step);

            _destination = GameObjects.hero.transform.position + (Vector3)_movementDirection;
        }

        //если туда можно двигаться и если мы еще не там - идем туда
        if (_destination != GameObjects.hero.transform.position) {
            Main.heroPosition.onTheWay = true;
            GameObjects.hero.transform.position = Vector3.MoveTowards(GameObjects.hero.transform.position, _destination, Constants.speed * Time.deltaTime);

            return;
        }

        //если мы пришли
        if (Main.heroPosition.onTheWay && GameObjects.hero.transform.position == _destination)
        {
            Main.previousPosition = Main.heroPosition;
            Main.heroPosition = Main.newPosition;
            Main.heroPosition.onTheWay = false;

            return;
        }
    }

    public void goBack()
    {
        if (_destination == GameObjects.hero.transform.position)
        {
            _movementDirection.Set(
            (Main.previousPosition.x - Main.heroPosition.x) * Constants.step,
            (Main.previousPosition.y - Main.heroPosition.y) * Constants.step
            );

            _destination = GameObjects.hero.transform.position + (Vector3)_movementDirection;

            GameObjects.hero.transform.position 
                = Vector3.MoveTowards(GameObjects.hero.transform.position,
                _destination,
                Constants.speed * Time.deltaTime
                );
        
            _movementDirection.Set(
            (Main.previousPosition.x - Main.heroPosition.x) * Constants.step,
            (Main.previousPosition.y - Main.heroPosition.y) * Constants.step
            );

            _destination = GameObjects.hero.transform.position + (Vector3)_movementDirection;

            GameObjects.hero.transform.position 
                = Vector3.MoveTowards(GameObjects.hero.transform.position,
                _destination,
                Constants.speed * Time.deltaTime
                );

            Main.heroPosition.onTheWay = false;
            Main.heroPosition.x = Main.previousPosition.x;
            Main.heroPosition.y = Main.previousPosition.y;
        }
        else
        {
            Main.heroPosition.onTheWay = true;
        }
    }
}
