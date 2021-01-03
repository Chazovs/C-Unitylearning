using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridService : MonoBehaviour
{

    GameObject[,] gameGrids;
    public GameObject block;

    /*
     * <summary> раскидывает по полю картинки полей </summary>
     */
    public void createGrid(GameObject grid, GameObject block)
    {
        Vector3 startPosition = grid.transform.position;

        float xx = startPosition.x + (Constants.step / 2);
        float yy = startPosition.y + (Constants.step / 2);

        gameGrids = new GameObject[Constants.gridWidth, Constants.gridWidth];

        for (int y = 0; y < Constants.gridWidth; y++)
        {
            for (int x = 0; x < Constants.gridWidth; x++)
            {
                gameGrids[x, y] = Instantiate(block);
                gameGrids[x, y].transform.position = new Vector3(xx, yy, startPosition.z);
                xx = xx + Constants.step;
            }
            xx = startPosition.x + (Constants.step / 2);
            yy = yy + Constants.step;
        }
    }

    public void setGoal(GameObject endPoint)
    {
        float randomX = Random.Range(1, 10);
        float randomY = Random.Range(1, 10);
        endPoint.transform.position = new Vector3(
            (endPoint.transform.position.x + Constants.step / 2) + (Constants.step * randomX),
            (endPoint.transform.position.y + Constants.step / 2) + (Constants.step * randomY),
            endPoint.transform.position.z
            );
    }
}
