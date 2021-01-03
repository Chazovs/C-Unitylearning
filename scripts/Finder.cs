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
