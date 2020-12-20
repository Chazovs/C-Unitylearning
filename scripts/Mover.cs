using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mover : MonoBehaviour
{
    private float speed = 10f;

    Vector2 _movementDirection;
    Vector3 _destination;
    private float step = 16f;
    private float offsetX;
    private float offsetY;
    private Vector3 _startField;
    private Vector3 _endField;
    private float fieldSize = 10;
    void Start()
    {
        transform.position = new Vector3(
            transform.position.x + (step / 2),
            transform.position.y - (step/2) + (step * fieldSize),
            transform.position.z
            );

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
}