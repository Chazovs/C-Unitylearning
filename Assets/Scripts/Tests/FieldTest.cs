using UnityEngine;

public class FieldTest
{
    public static void showField()
    {
        GameFieldService gameFieldService = ServiceLocator.GetService<GameFieldService>();

        string myX = "Y \n";

        for (int j = Constants.fieldSize-1; j >= 0; j--) //столбцы
        {
            int realj = j + 1;
            realj = realj == 10 ? 0 : realj;
            myX += + realj + " ";
            
            for (int i = 0; i < Constants.fieldSize; i++)//строки
            {
                if (gameFieldService.gameFields[i,j].isStart)
                {
                    /*myX += "x" + i + "y" + j + "*";*/
                    myX += "* ";
                }
                else if (gameFieldService.gameFields[i, j].isWin)
                {
                    /*myX += "x" + i + "y" + j + "O " ;*/
                    myX += "! " ;
                }
                else if (i == 0 && j == 0)
                {
                    /*myX += "x" + i + "y" + j + "O " ;*/
                    myX += "Z " ;
                }
                else
                {
                    /*myX += mainComponent.gameFields[i, j].isSafe ? "x" + i + "y" + j + "+ " : "x" + i + "y" + j + "- ";
                    myX += mainComponent.gameFields[i, j].isSafe ? "x" + i + "y" + j + "№"+ mainComponent.gameFields[i, j].id + "+ " : "x" + i + "y" + j + "- ";*/
                    myX += gameFieldService.gameFields[i, j].isSafe ? "+ " : "- ";
                    /*if (gameFieldService.gameFields[i, j].isSafe)
                    {
                        int ii = i + 1;
                        int jj = j + 1;
                        Debug.Log("_X:" + ii + " _Y:" + jj);
                    }*/
                }
            }

            myX += "\n";
        }
        myX += " 1 2 3 4 5 6 7 8 9 0 X\n";
        Debug.Log(myX);
        Debug.Log("goalPosition x: " + gameFieldService.goalPosition.x + "y: " + gameFieldService.goalPosition.y);
    }
}
