using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mover : MonoBehaviour
{
    Vector2 _movementDirection;
    Vector3 _destination;

    private Vector3 _startField;
    private Vector3 _endField;

    private Card[,] gameFields;

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

    public bool isInputBlocked = false;

/*    void Start()
    {
        gameFields = new Card[(int) Constants.fieldSize, (int)Constants.fieldSize];

        cardService = new CardService();
        cardService.hideCard();

        heroPosition.x = previousPosition.x = 1;
        heroPosition.y = previousPosition.y = Constants.fieldSize;

        _goal = GameObject.Find("EndPoint");
        _block = GameObject.Find("block");

        goalPosition.x = (float)(Math.Abs(_block.transform.position.x - Constants.step * 0.5) + _goal.transform.position.x) / Constants.step;
        goalPosition.y = (float)(Math.Abs(_block.transform.position.y - Constants.step * 0.5) + _goal.transform.position.y) / Constants.step;

        transform.position = new Vector3(
            transform.position.x + (Constants.step / 2),
            transform.position.y - (Constants.step / 2) + (Constants.step * Constants.fieldSize),
            transform.position.z
            );

        GameFieldService fieldService = new GameFieldService();
        gameFields =  fieldService.fillGameFields(heroPosition, goalPosition);

*//*        string myX = "";
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
        Debug.Log("goalPosition x: " + goalPosition.x + "y: " + goalPosition.y);*//*

        _startField = transform.position;
        _endField = new Vector3(
            _startField.x + Constants.step * (Constants.fieldSize - 1),
            _startField.y - Constants.step * (Constants.fieldSize - 1),
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
           
                Card currentCard = gameFields[(int)heroPosition.x - 1, (int)heroPosition.y - 1];
                if (currentCard.isOpen == false)
                {
                    isInputBlocked = true;
                    cardService.showCard(gameFields[(int)heroPosition.x - 1, (int)heroPosition.y - 1]);
                }

            if (heroPosition.x > Constants.fieldSize) heroPosition.x = Constants.fieldSize;
            if (heroPosition.x < 1) heroPosition.x = 1;
            if (heroPosition.y > Constants.fieldSize) heroPosition.y = Constants.fieldSize;
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
            transform.position = Vector3.MoveTowards(transform.position, _destination, Constants.speed * Time.deltaTime);
        }
        else
        {
            _destination = transform.position;
        }
        }
    }

    public void goBack()
    {
        _movementDirection.Set(
            (previousPosition.x - heroPosition.x) * Constants.step,
            (previousPosition.y - heroPosition.y) * Constants.step
            );

        _destination = transform.position + (Vector3)_movementDirection;

        transform.position = Vector3.MoveTowards(transform.position, _destination, Constants.speed * Time.deltaTime);

        heroPosition.x = previousPosition.x;
        heroPosition.y = previousPosition.y;
    }*/
}
