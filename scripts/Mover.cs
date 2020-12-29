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


    void Start()
    {
        heroPosition.x = 1;
        heroPosition.y = 10;

        _goal = GameObject.Find("EndPoint");
        _block = GameObject.Find("block");

        goalPosition.x = (float)(Math.Abs(_block.transform.position.x - step * 0.5) + _goal.transform.position.x) / step;
        goalPosition.y = (float)(Math.Abs(_block.transform.position.y - step * 0.5) + _goal.transform.position.y) / step;

        transform.position = new Vector3(
            transform.position.x + (step / 2),
            transform.position.y - (step / 2) + (step * fieldSize),
            transform.position.z
            );

        fillGameFields();

        string myX = "";
        for (int i = 0; i < fieldSize; i++) //строки
        {
            for (int j = 0; j < fieldSize; j++)//столбцы
            {
                if (heroPosition.x == i+1 && heroPosition.y == j+1)
                {
                    myX += "x" + i + "y" + j + "*";
                } else if (goalPosition.x == i+1 && goalPosition.y == j+1) {
                    myX += "x" + i + "y" + j + "O ";
                }
                else
                {
                    myX += gameFields[i, j].isSafe ? "x"+i+ "y" + j + "+ " : "x" + i + "y" + j + "- ";
                }
            }
            myX += "#";
            
        }
        Debug.Log(myX);
        Debug.Log("goalPosition x: " + goalPosition.x + "y: " + goalPosition.y);

        _startField = transform.position;
        _endField = new Vector3(
            _startField.x + step * 9,
            _startField.y - step * 9,
            transform.position.z
            );

        _destination = transform.position;
    }
    void Update()
    {

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetAxisRaw("Horizontal") != _horizontVector || Input.GetAxisRaw("Vertical") != _verticalVector)
        {
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
    /**
     * <summary>заполняет поле карточками</summary>
     */
    private void fillGameFields()
    {
        List<Card> safeFields = getSafeFields();
        List<Card> dangerousFields = getDangerousFields();

        /*setSafePath(safeFields);*/
        setSafePathVectors(safeFields);
        setDangerousFields(dangerousFields);
    }

    private void setSafePathVectors(List<Card> safeFields)
    {
        safeFields = Shuffler.listShuffler(safeFields);

        Position cursor = new Position();
        cursor.x = heroPosition.x;
        cursor.y = heroPosition.y;

        System.Random rnd = new System.Random(DateTime.Now.Millisecond);

        int directionKey;
        int safeFieldsIndex = 0;

        while (cursor.x != goalPosition.x || cursor.y != goalPosition.y) {

            //вычисляем расстояние до края поля в каждом направлении
            float upDistance = 10 - cursor.y;
            float downDistance = cursor.y - 1;
            float rightDistance = 10 - cursor.x;
            float leftDistance = cursor.x - 1;

            float[] distanceArr = {upDistance, downDistance, rightDistance, leftDistance };

            //ишем подходящее направление пока не найдем
            while (true)
        {
           directionKey = rnd.Next(0, 4);

            if (distanceArr[directionKey] > 0)
            {
                break;
            }
        }
        
        //решаем, как далеко мы пойдем в этом направлении
        int distance = rnd.Next(0, (int)distanceArr[directionKey] + 1);

        Position endPoint = new Position();

        switch (directionKey)
        {
            case 0:
                endPoint.x = cursor.x;
                endPoint.y = cursor.y+distance;
                break;
            case 1:
                endPoint.x = cursor.x;
                endPoint.y = cursor.y - distance;
                break;
            case 2:
                endPoint.x = cursor.x + distance;
                endPoint.y = cursor.y ;
                break;
            case 3:
                endPoint.x = cursor.x - distance;
                endPoint.y = cursor.y;
                break;
        }

        //идем в этом направлении пока не придем
        while (cursor.x != endPoint.x || cursor.y != endPoint.y)
        {
            switch (directionKey)
            {
                //вверх
                case 0:
                    cursor.y++;
                    break;
                //вниз
                case 1:
                    cursor.y--;
                    break;
                //вправо
                case 2:
                    cursor.x++;
                    break;
                //влево
                case 3:
                    cursor.x--;
                    break;
            }

            if((cursor.x == goalPosition.x && cursor.y == goalPosition.y)) {
                    break;
            }

            if (
                (cursor.x != heroPosition.x || cursor.y != heroPosition.y)
                && gameFields[(int) cursor.x - 1, (int) cursor.y - 1] == null
                )
            {
                gameFields[(int) cursor.x - 1, (int) cursor.y - 1] = safeFields[safeFieldsIndex];
            }
        }

            if ((cursor.x == goalPosition.x && cursor.y == goalPosition.y))
            {
                break;
            }

            safeFieldsIndex++;

            safeFieldsIndex = safeFieldsIndex > safeFields.Count - 1 ? 0 : safeFieldsIndex;
        }
    }

    private void setDangerousFields(List<Card> dangerousFields)
    {
        dangerousFields = Shuffler.listShuffler(dangerousFields);

        int dangerousFieldsIndex = 0;

        for (int i = 0; i < fieldSize; i++)
        {
            for (int j = 0; j < fieldSize; j++)
            {
                if (gameFields[i, j] == null)
                {
                    gameFields[i, j] = dangerousFields[dangerousFieldsIndex];

                    dangerousFieldsIndex++;

                    dangerousFieldsIndex = dangerousFieldsIndex > dangerousFields.Count-1 ? 0 : dangerousFieldsIndex;
                }
            }
        }
    }

    /**
     * <summary>Прокладывает дорогу к цели</summary>
     */
    private void setSafePath(List<Card> safeFields)
    {
        safeFields = Shuffler.listShuffler(safeFields);

        Position cursor = new Position();
            cursor.x = heroPosition.x;
            cursor.y = heroPosition.y;

        int safeFieldsIndex = 0;
        int direction;
        Position newPosition = new Position();

        System.Random rnd = new System.Random(DateTime.Now.Millisecond);

        do
        { direction = rnd.Next(1, 5);
            newPosition.x = cursor.x;
            newPosition.y = cursor.y;

            switch (direction)
            {
                case 1:
                    newPosition.x++;
                    break;
                case 2:
                    newPosition.y++;
                    break;
                case 3:
                    newPosition.x--;
                    break;
                case 4:
                    newPosition.y--;
                    break;
            }

            if (
                newPosition.x > 0
                && newPosition.x < 11
                && newPosition.y > 0
                && newPosition.y < 11
                && (newPosition.x != heroPosition.x || newPosition.y != heroPosition.y)
                )
            {
                cursor.x = newPosition.x;
                cursor.y = newPosition.y;

                if (gameFields[(int)cursor.x - 1, (int)cursor.y - 1] is null 
                    && (cursor.x != heroPosition.x  || cursor.y != heroPosition.y)
                    )
                {
                    gameFields[(int)cursor.x - 1, (int)cursor.y - 1] = safeFields[safeFieldsIndex];

                    safeFieldsIndex++;

                    safeFieldsIndex = safeFieldsIndex > safeFields.Count-1 ? 0 : safeFieldsIndex;
                }
            }
        } while (cursor.x != goalPosition.x || cursor.y != goalPosition.y);
    }

    private List<Card> getDangerousFields()
    {
        CardRepository repository = new CardRepository();

        return repository.getCardsBySafety(false);
    }

    private List<Card> getSafeFields()
    {
        CardRepository repository = new CardRepository();

        return repository.getCardsBySafety(true);
    }
}
