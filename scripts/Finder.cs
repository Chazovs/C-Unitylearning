using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finder : MonoBehaviour
{
    int[,] gameFields = new int[10, 10];
    int[] startField = new int[2];
    int[] goalField = new int[2];
    private float step = 16;

    private GameObject _goal;
    private GameObject _block;
    private float xField;
    private float yField;

    // Start is called before the first frame update
    void Start()
    {
         startField[0] = 1;
         startField[1] = 10;
        _goal = GameObject.Find("EndPoint");
        _block = GameObject.Find("block");

        xField = (Math.Abs(_block.transform.position.x + step / 2) + _goal.transform.position.x) / step;
        yField = (Math.Abs(_block.transform.position.y + step / 2) + _goal.transform.position.y) / step;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x == _goal.transform.position.x
            && transform.position.y == _goal.transform.position.y)
        {
            SceneManager.LoadScene("End");
        }
    }
}
