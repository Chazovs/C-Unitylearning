using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mover : MonoBehaviour
{
    private float speed = 10f;

    Vector2 _movementDirection;
    Vector3 _destination;
    private float step;
    private float offsetX;
    private float offsetY;
    private Vector3 _startField;
    private Vector3 _endField;

    void Start()
    {
        step = 15;
        _startField = transform.position;
        _endField = new Vector3();
        _endField.x = _startField.x + step * 10;
        _endField.y = _startField.y - step * 10;
        _destination = transform.position;
    }
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


        if (input.x > 0)
        {
            _movementDirection.Set(input.x+step, 0f);
        }
         else if (input.x < 0)
        {
            _movementDirection.Set(input.x-step, 0f);
        }
        else if (input.y > 0)
        {
            _movementDirection.Set(0f, input.y+step);
        }
        else if (input.y < 0)
        {
            _movementDirection.Set(0f, input.y-step);
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
}