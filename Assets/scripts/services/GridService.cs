using UnityEngine;

public class GridService : MonoBehaviour
{

    private GameObject main;
    private Main mainComponent;
    private GameObjects gameObjects;

    public GridService(ref GameObjects _gameObjects)
    {
        gameObjects = _gameObjects;
        main = _gameObjects.main;
        mainComponent = main.GetComponent<Main>();
    }

    public void setGoal()
    {
        Position goalPosition = new Position();

        float goalZone = Random.Range(0, 2);
        
        if(goalZone == 0) {
            goalPosition.x = Random.Range(5, 11);
            goalPosition.y = Random.Range(1, 11);
        }

        if(goalZone == 1) {
            goalPosition.x = Random.Range(1, 11);
            goalPosition.y = Random.Range(1, 7);
        }

        gameObjects.endPoint.transform.position = new Vector3(
            (gameObjects.endPoint.transform.position.x + Constants.step / 2) + (Constants.step * goalPosition.x)-Constants.step,
            (gameObjects.endPoint.transform.position.y + Constants.step / 2) + (Constants.step * goalPosition.y)-Constants.step,
            gameObjects.endPoint.transform.position.z
            );
        
        mainComponent.goalPosition = goalPosition;
    }
}
