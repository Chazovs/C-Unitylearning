using UnityEngine;

public class FieldTest
{
    public static void showField()
    {
        GameFieldService gameFieldService = ServiceLocator.GetService<GameFieldService>();

        string myX = "";
        for (int i = 0; i < Constants.fieldSize; i++) //строки
        {
            for (int j = 0; j < Constants.fieldSize; j++)//столбцы
            {
                if (i == 0 && j ==9)
                {
                    /*myX += "x" + i + "y" + j + "*";*/
                    myX += "*";
                }
                else if (gameFieldService.goalPosition.x == i + 1 && gameFieldService.goalPosition.y == j + 1)
                {
                    /*myX += "x" + i + "y" + j + "O " ;*/
                    myX += "O " ;
                }
                else
                {
                    /*myX += mainComponent.gameFields[i, j].isSafe ? "x" + i + "y" + j + "+ " : "x" + i + "y" + j + "- ";
                    myX += mainComponent.gameFields[i, j].isSafe ? "x" + i + "y" + j + "№"+ mainComponent.gameFields[i, j].id + "+ " : "x" + i + "y" + j + "- ";*/
                    myX += gameFieldService.gameFields[i, j].isSafe ? "+ " : "- ";
                }
            }

            myX += "\n";
        }

        Debug.Log(myX);
        Debug.Log("goalPosition x: " + gameFieldService.goalPosition.x + "y: " + gameFieldService.goalPosition.y);
    }
}
