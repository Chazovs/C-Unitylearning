using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    private float step = 16f;
    private float randomX;
    private float randomY;
    private float fieldSize;

    // Start is called before the first frame update
    void Start()
    {
        randomX = Random.Range(1, 10);
        randomY = Random.Range(1, 10);
        transform.position = new Vector3(
            (transform.position.x + step / 2) + (step* randomX),
            (transform.position.y + step / 2) + (step * randomY),
            transform.position.z
            );
    }
}
