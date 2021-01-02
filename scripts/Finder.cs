using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finder : MonoBehaviour
{
    private GameObject _goal;
    private GameObject _block;
    private float xField;
    private float yField;


    // Start is called before the first frame update
    void Start()
    {

        _goal = GameObject.Find("EndPoint");
        _block = GameObject.Find("block");

        xField = (Math.Abs(_block.transform.position.x + Constants.step / 2) + _goal.transform.position.x) / Constants.step;
        yField = (Math.Abs(_block.transform.position.y + Constants.step / 2) + _goal.transform.position.y) / Constants.step;
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
