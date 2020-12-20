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

    void Start()
    {
        _destination = transform.position;
        step = 15;
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

        transform.position = Vector3.MoveTowards(transform.position, _destination, speed * Time.deltaTime);

    }
}