using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroService
{
    private Vector2 _movementDirection;

    private Vector3 _destination;
    private Vector3 _startField;
    private Vector3 _endField;

    private Card[,] gameFields;
    private Main mainComponent;
    private ServiceLocator serviceLocator;

    /*позиция героя */
    private Position heroPosition = new Position();

    /*позиция цели */
    private Position goalPosition = new Position();

    private GameObject _goal;
    private GameObject _block;
    private GameObject hero;
    private GameObject main;
    private float _horizontVector = 0;
    private float _verticalVector = 0;

    Position previousPosition = new Position();

    CardService cardService;

    public bool isInputBlocked = false;

    public HeroService(GameObjects gameObjects)
    {
        _goal = gameObjects.endPoint;
        _block = gameObjects.block;
        hero = gameObjects.hero;
        main = gameObjects.main;

        mainComponent = main.GetComponent<Main>();
        serviceLocator = mainComponent.serviceLocator;

        heroPosition.x = previousPosition.x = 1;
        heroPosition.y = previousPosition.y = Constants.fieldSize;

        hero.transform.position = new Vector3(
            hero.transform.position.x + (Constants.step / 2),
            hero.transform.position.y - (Constants.step / 2) + (Constants.step * Constants.fieldSize),
            hero.transform.position.z
            );

        /*        string myX = "";
                for (int i = 0; i < fieldSize; i++) //строки
                {
                    for (int j = 0; j < fieldSize; j++)//столбцы
                    {
                        if (heroPosition.x == i+1 && heroPosition.y == j+1)
                        {
                            myX += "x" + i + "y" + j + "*";
                        } else if (goalPosition.x == i+1 && goalPosition.y == j+1) {
                            string str = gameFields[i, j].isWin ? "V " : "! ";
                            myX += "x" + i + "y" + j + "O" + str;
                        }
                        else
                        {
                            myX += gameFields[i, j].isSafe ? "x"+i+ "y" + j + "+ " : "x" + i + "y" + j + "- ";
                        }
                    }
                    myX += "#";

                }
                Debug.Log(myX);
                Debug.Log("goalPosition x: " + goalPosition.x + "y: " + goalPosition.y);*/

        _startField = hero.transform.position;
        _endField = new Vector3(
            _startField.x + Constants.step * (Constants.fieldSize - 1),
            _startField.y - Constants.step * (Constants.fieldSize - 1),
            hero.transform.position.z
            );

        _destination = hero.transform.position;

    }

    public void move()
    {
        {
            if (!isInputBlocked)
            {

                Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

                if (
                    (Input.GetAxisRaw("Horizontal") != _horizontVector && Input.GetAxisRaw("Horizontal") != 0)
                    || (Input.GetAxisRaw("Vertical") != _verticalVector && Input.GetAxisRaw("Vertical") != 0)
                    )
                {
                    previousPosition.x = heroPosition.x;
                    previousPosition.y = heroPosition.y;

                    heroPosition.x += input.x;
                    heroPosition.y += input.y;

                    Card currentCard = mainComponent.gameFields[(int)heroPosition.x - 1, (int)heroPosition.y - 1];

                    if (currentCard.isOpen == false)
                    {
                        isInputBlocked = true;
                        serviceLocator.cardService.showCard(mainComponent.gameFields[(int)heroPosition.x - 1, (int)heroPosition.y - 1]);
                    }

                    if (heroPosition.x > Constants.fieldSize) heroPosition.x = Constants.fieldSize;
                    if (heroPosition.x < 1) heroPosition.x = 1;
                    if (heroPosition.y > Constants.fieldSize) heroPosition.y = Constants.fieldSize;
                    if (heroPosition.y < 1) heroPosition.y = 1;
                }

                _horizontVector = Input.GetAxisRaw("Horizontal");
                _verticalVector = Input.GetAxisRaw("Vertical");

                if (hero.transform.position.x == _goal.transform.position.x
                    && hero.transform.position.y == _goal.transform.position.y)
                {
                    SceneManager.LoadScene("End");
                }

                if (input.x > 0)
                {
                    _movementDirection.Set(Constants.step, 0f);
                }
                else if (input.x < 0)
                {
                    _movementDirection.Set(-Constants.step, 0f);
                }
                else if (input.y > 0)
                {
                    _movementDirection.Set(0f, Constants.step);
                }
                else if (input.y < 0)
                {
                    _movementDirection.Set(0f, -Constants.step);
                }
                else
                {
                    _movementDirection = Vector2.zero;
                }

                if (_destination == hero.transform.position)
                {
                    _destination = hero.transform.position + (Vector3)_movementDirection;
                }

                if (_destination.x >= _startField.x
                    && _destination.x <= _endField.x
                    && _destination.y <= _startField.y
                    && _destination.y >= _endField.y
                    )
                {
                    hero.transform.position = Vector3.MoveTowards(hero.transform.position, _destination, Constants.speed * Time.deltaTime);
                }
                else
                {
                    _destination = hero.transform.position;
                }
            }
        }
    }

    public void goBack()
    {
        _movementDirection.Set(
            (previousPosition.x - heroPosition.x) * Constants.step,
            (previousPosition.y - heroPosition.y) * Constants.step
            );

        _destination = hero.transform.position + (Vector3)_movementDirection;

        hero.transform.position = Vector3.MoveTowards(hero.transform.position, _destination, Constants.speed * Time.deltaTime);

        heroPosition.x = previousPosition.x;
        heroPosition.y = previousPosition.y;
    }

    internal void setCardInField(Card currentCard)
    {
        mainComponent.gameFields[(int)currentCard.position.x, (int)currentCard.position.y] = currentCard;
    }
}
