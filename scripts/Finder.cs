using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(transform.position.x == _goal.transform.position.x
            && transform.position.y == _goal.transform.position.y)
        {
            SceneManager.LoadScene("End");
        }
    }
}
