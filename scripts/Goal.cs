using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    private float randomX;
    private float randomY;

    // Start is called before the first frame update
    void Start()
    {
        randomX = Random.Range(1, 10);
        randomY = Random.Range(1, 10);
        transform.position = new Vector3(
            (transform.position.x + Constants.step / 2) + (Constants.step * randomX),
            (transform.position.y + Constants.step / 2) + (Constants.step * randomY),
            transform.position.z
            );
    }
}
