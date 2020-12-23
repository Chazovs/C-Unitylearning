using System.Collections;
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
    private Position heroPosition;

    /*позиция цели */
    private Position goalPosition;
 
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

        goalPosition.x = (float) (Math.Abs(_block.transform.position.x - step * 0.5) + _goal.transform.position.x) / step;
        goalPosition.y = (float) (Math.Abs(_block.transform.position.y - step * 0.5) + _goal.transform.position.y) / step;

        transform.position = new Vector3(
            transform.position.x + (step / 2),
            transform.position.y - (step/2) + (step * fieldSize),
            transform.position.z
            );

        fillGameFields();

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
            ) {
            transform.position = Vector3.MoveTowards(transform.position, _destination, speed * Time.deltaTime);
        }
        else
        {
            _destination = transform.position;
        }
    }
    /**
     * заполняет поле карточками
     */
    private void fillGameFields()
    {
        Card[] safeFields = getSafeFields();
        Card[] dangerousFields = getDangerousFields();

        setSafePath(safeFields);
        setDangerousFields(dangerousFields);
    }

    private void setDangerousFields(Card[] dangerousFields)
    {
        throw new NotImplementedException();
    }

    private void setSafePath(Card[] safeFields)
    {
        throw new NotImplementedException();
    }

    private Card[] getDangerousFields()
    {
        CardRepository repository = new CardRepository();

        return repository.getCardsBySafety(false);
    }

    private Card[] getSafeFields()
    {
        CardRepository repository = new CardRepository();

        return repository.getCardsBySafety(true);
    }
}