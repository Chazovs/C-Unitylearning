using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HeroService
{
    public Position heroPosition = new Position();
    public Position newPosition = new Position();
    public Position previousPosition = new Position();
    private Vector2 _movementDirection;
    private Vector3 _destination = new Vector3();
    private KeyInput keyInput = new KeyInput();

    public void SetHeroPosition()
    {
        heroPosition.x = Random.Range(1, 11);
        heroPosition.y = Random.Range(1, 11);
        heroPosition.onTheWay = false;

        GameObjects.hero.transform.position = new Vector3(
            GameObjects.hero.transform.position.x + (Constants.step / 2) + (Constants.step * (heroPosition.x-1)),
            GameObjects.hero.transform.position.y + (Constants.step / 2) + (Constants.step * (heroPosition.y-1)),
            GameObjects.hero.transform.position.z
            );

        _destination = GameObjects.hero.transform.position;
    }

    public void Move()
    {
        CardService cardService = ServiceLocator.GetService<CardService>();

        cardService.ShowController();

        if (cardService.isCardShowing) return;

        keyInput.x = keyInput.y = 0;

        //если герой не движется, то можно принимать значение ввода
        if (!heroPosition.onTheWay) {
            keyInput.x = Input.GetAxisRaw("Horizontal");
            keyInput.y = Input.GetAxisRaw("Vertical");
        };

        if(keyInput.x != 0 || keyInput.y != 0)
        {
            heroPosition.onTheWay = true;

            newPosition = new Position
            {
                x = heroPosition.x + keyInput.x,
                y = heroPosition.y + keyInput.y
            };

            bool canMove = newPosition.x > 0
            && newPosition.x < 11
            && newPosition.y > 0
            && newPosition.y < 11;

            if (!canMove) {
                heroPosition.onTheWay = false;
                return;
            }

            _movementDirection = Vector2.zero;

            _movementDirection.Set(keyInput.x * Constants.step, keyInput.y * Constants.step);

            _destination = GameObjects.hero.transform.position + (Vector3)_movementDirection;
        }

        //если туда можно двигаться и если мы еще не там - идем туда
        if (_destination != GameObjects.hero.transform.position) {
            heroPosition.onTheWay = true;
            GameObjects.hero.transform.position = Vector3.MoveTowards(GameObjects.hero.transform.position, _destination, Constants.speed * Time.deltaTime);

            return;
        }

        //если мы пришли
        if (heroPosition.onTheWay && GameObjects.hero.transform.position == _destination)
        {
            previousPosition =heroPosition;
            heroPosition = newPosition;
            heroPosition.onTheWay = false;

            return;
        }
    }

    public void goBack()
    {
        if (_destination == GameObjects.hero.transform.position)
        {
            _movementDirection.Set(
            (previousPosition.x - heroPosition.x) * Constants.step,
            (previousPosition.y - heroPosition.y) * Constants.step
            );

            _destination = GameObjects.hero.transform.position + (Vector3)_movementDirection;

            GameObjects.hero.transform.position 
                = Vector3.MoveTowards(GameObjects.hero.transform.position,
                _destination,
                Constants.speed * Time.deltaTime
                );
        
            _movementDirection.Set(
            (previousPosition.x - heroPosition.x) * Constants.step,
            (previousPosition.y - heroPosition.y) * Constants.step
            );

            _destination = GameObjects.hero.transform.position + (Vector3)_movementDirection;

            GameObjects.hero.transform.position 
                = Vector3.MoveTowards(GameObjects.hero.transform.position,
                _destination,
                Constants.speed * Time.deltaTime
                );

            heroPosition.onTheWay = false;
            heroPosition.x = previousPosition.x;
            heroPosition.y = previousPosition.y;
        }
        else
        {
            heroPosition.onTheWay = true;
        }
    }
}
