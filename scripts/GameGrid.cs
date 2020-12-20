using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    int gridWidth = 10;

    public GameObject block;

    GameObject[,] gameGrids;
    private float step = 3;

    void CreateGrid()
    {
        Vector3 startPosition = transform.position;

        float xx = startPosition.x + step;
        float yy = startPosition.y - step;

        gameGrids = new GameObject[gridWidth, gridWidth];

        for (int y = 0; y < gridWidth; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                gameGrids[x, y] = Instantiate(block);
                gameGrids[x, y].GetComponent<GridBlocks>().currentBlockIndex = 0;
                gameGrids[x, y].transform.position = new Vector3(xx, yy, startPosition.z);
                xx++;
            }
            xx = startPosition.x + step;
            yy = yy+step;
        }
    }

   
    private void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
