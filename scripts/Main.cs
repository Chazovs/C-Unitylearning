using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject block = GameObject.Find("block");
        GameObject grid = GameObject.Find("grid");
        GameObject endPoint = GameObject.Find("EndPoint");

        GridService gridService = new GridService();

        gridService.createGrid(grid, block);
        gridService.setGoal(endPoint);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
