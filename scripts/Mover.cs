using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mover : MonoBehaviour
{
    public float speed = 150f;

    Vector2 _movementDirection;
    Vector3 _destination;
    private float step;

    void Start()
    {
        _destination = transform.position;
        step = 150;
    }
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.x != 0) {

            float offsetX = input.x >= 0 ? input.x + step : input.x - step;

            _movementDirection.Set(offsetX, 0f);
        }
        else if (input.y != 0)
        {
            float offsetY = input.y >= 0 ? input.y + step : input.y - step;

            _movementDirection.Set(0f, offsetY);
        }
        else
        {
            _movementDirection = Vector2.zero;
        }

        if (_destination == transform.position) {
            _destination = transform.position + (Vector3)_movementDirection;
        }

        transform.position = Vector3.MoveTowards(transform.position, _destination, speed * Time.deltaTime);
    }
}