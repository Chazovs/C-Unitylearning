using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finder : MonoBehaviour
{
    private GameObject _goal;

    // Start is called before the first frame update
    void Start()
    {
        _goal = GameObject.Find("EndPoint");
    }

    // Update is called once per frame
    void Update()
    {
        /*_goal.transform.position.x;
        _goal.transform.position.y;*/
    }
}
