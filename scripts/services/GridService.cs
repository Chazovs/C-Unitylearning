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

    public Position setGoal(GameObject endPoint)
    {
        Position goalPosition = new Position();

        goalPosition.x = Random.Range(1, 10);
        goalPosition.y = Random.Range(1, 10);
        endPoint.transform.position = new Vector3(
            (endPoint.transform.position.x + Constants.step / 2) + (Constants.step * goalPosition.x),
            (endPoint.transform.position.y + Constants.step / 2) + (Constants.step * goalPosition.y),
            endPoint.transform.position.z
            );

        return goalPosition;
    }
}
