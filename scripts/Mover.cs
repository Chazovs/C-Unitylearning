using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mover : MonoBehaviour
{
    private float speed = 10f;

    Vector2 _movementDirection;
    Vector3 _destination;
    private float step = 16f;

    private Vector3 _startField;
    private Vector3 _endField;
    private float fieldSize = 10;

    private Card[,] gameFields = new Card[10, 10];

    /*позиция героя */
    private Position heroPosition = new Position();

    /*позиция цели */
    private Position goalPosition = new Position();

    private GameObject _goal;
    private GameObject _block;
    private float _horizontVector = 0;
    private float _verticalVector = 0;

    Position previousPosition = new Position();

    CardService cardService;

    void Start()
    {
        cardService = new CardService();
        cardService.hideCard();

        heroPosition.x = previousPosition.x = 1;
        heroPosition.y = previousPosition.y = 10;

        _goal = GameObject.Find("EndPoint");
        _block = GameObject.Find("block");

        goalPosition.x = (float)(Math.Abs(_block.transform.position.x - step * 0.5) + _goal.transform.position.x) / step;
        goalPosition.y = (float)(Math.Abs(_block.transform.position.y - step * 0.5) + _goal.transform.position.y) / step;

        transform.position = new Vector3(
            transform.position.x + (step / 2),
            transform.position.y - (step / 2) + (step * fieldSize),
            transform.position.z
            );

        GameFieldService fieldService = new GameFieldService(fieldSize, heroPosition, goalPosition);
        gameFields =  fieldService.fillGameFields();

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

        _startField = transform.position;
        _endField = new Vector3(
            _startField.x + step * 9,
            _startField.y - step * 9,
            transform.position.z
            );

        _destination = transform.position;
    }

    internal void setCardInField(Card currentCard)
    {
        gameFields[(int)currentCard.position.x, (int)currentCard.position.y] = currentCard;
    }

    void Update()
    {

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (
            (Input.GetAxisRaw("Horizontal") != _horizontVector && Input.GetAxisRaw("Horizontal") != 0)
            || (Input.GetAxisRaw("Vertical") != _verticalVector && Input.GetAxisRaw("Vertical") != 0)
            )
        {
            previousPosition.x = heroPosition.x;
            previousPosition.y = heroPosition.y;

            cardService.showCard(gameFields[(int)heroPosition.x - 1, (int)heroPosition.y - 1]);

            heroPosition.x += input.x;
            heroPosition.y += input.y;

            if (heroPosition.x > 10) heroPosition.x = 10;
            if (heroPosition.x < 1) heroPosition.x = 1;
            if (heroPosition.y > 10) heroPosition.y = 10;
            if (heroPosition.y < 1) heroPosition.y = 1;
        }

       

        _horizontVector = Input.GetAxisRaw("Horizontal");
        _verticalVector = Input.GetAxisRaw("Vertical");

        if (transform.position.x == _goal.transform.position.x
            && transform.position.y == _goal.transform.position.y)
        {
            SceneManager.LoadScene("End");
        }

        if (input.x > 0)
        {
            _movementDirection.Set(step, 0f);
        }
        else if (input.x < 0)
        {
            _movementDirection.Set(-step, 0f);
        }
        else if (input.y > 0)
        {
            _movementDirection.Set(0f, step);
        }
        else if (input.y < 0)
        {
            _movementDirection.Set(0f, -step);
        }
        else
        {
            _movementDirection = Vector2.zero;
        }

        if (_destination == transform.position)
        {
            _destination = transform.position + (Vector3)_movementDirection;
        }

        if (_destination.x >= _startField.x
            && _destination.x <= _endField.x
            && _destination.y <= _startField.y
            && _destination.y >= _endField.y
            )
        {
            transform.position = Vector3.MoveTowards(transform.position, _destination, speed * Time.deltaTime);
        }
        else
        {
            _destination = transform.position;
        }
    }

    public void goBack()
    {
        _movementDirection.Set(
            (previousPosition.x - heroPosition.x) * step,
            (previousPosition.y - heroPosition.y) * step
            );

        _destination = transform.position + (Vector3)_movementDirection;

        transform.position = Vector3.MoveTowards(transform.position, _destination, speed * Time.deltaTime);

        heroPosition.x = previousPosition.x;
        heroPosition.y = previousPosition.y;
    }
}
