using UnityEngine;

public class GridService : MonoBehaviour
{

    GameObject[,] gameGrids;
    public GameObject block;
    private GameObject main;
    private Main mainComponent;
    private GameObjects gameObjects;

    public GridService(ref GameObjects _gameObjects)
    {
        gameObjects = _gameObjects;
        block = gameObjects.block;
        main = _gameObjects.main;
        mainComponent = main.GetComponent<Main>();
    }

        /*
         * <summary> раскидывает по полю картинки полей </summary>
         */
        public void createGrid()
    {
        Vector3 startPosition = gameObjects.grid.transform.position;

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

    public void setGoal()
    {
        Position goalPosition = new Position();

        goalPosition.x = Random.Range(1, 10);
        goalPosition.y = Random.Range(1, 10);
        gameObjects.endPoint.transform.position = new Vector3(
            (gameObjects.endPoint.transform.position.x + Constants.step / 2) + (Constants.step * goalPosition.x)-Constants.step,
            (gameObjects.endPoint.transform.position.y + Constants.step / 2) + (Constants.step * goalPosition.y)-Constants.step,
            gameObjects.endPoint.transform.position.z
            );
        
        mainComponent.goalPosition = goalPosition;
    }
}
