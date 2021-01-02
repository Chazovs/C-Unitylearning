using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridService : MonoBehaviour
{

    GameObject[,] gameGrids;
    public GameObject block;

    public void CreateGrid(Transform transform)
    {
        block = GameObject.Find("block");

        Vector3 startPosition = transform.position;

        float xx = startPosition.x + (Constants.step / 2);
        float yy = startPosition.y + (Constants.step / 2);

        gameGrids = new GameObject[Constants.gridWidth, Constants.gridWidth];

        for (int y = 0; y < Constants.gridWidth; y++)
        {
            for (int x = 0; x < Constants.gridWidth; x++)
            {
                gameGrids[x, y] = Instantiate(block);
                gameGrids[x, y].GetComponent<GridBlocks>().currentBlockIndex = 0;
                gameGrids[x, y].transform.position = new Vector3(xx, yy, startPosition.z);
                xx = xx + Constants.step;
            }
            xx = startPosition.x + (Constants.step / 2);
            yy = yy + Constants.step;
        }
    }
}
