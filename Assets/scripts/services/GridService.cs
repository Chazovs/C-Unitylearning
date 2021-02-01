using UnityEngine;

public class GridService : MonoBehaviour
{
    public void SetGoal()
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

        GameObjects.endPoint.transform.position = new Vector3(
            (GameObjects.endPoint.transform.position.x + Constants.step / 2) + (Constants.step * goalPosition.x)-Constants.step,
            (GameObjects.endPoint.transform.position.y + Constants.step / 2) + (Constants.step * goalPosition.y)-Constants.step,
            GameObjects.endPoint.transform.position.z
            );

        Main.goalPosition = goalPosition;
    }
}
