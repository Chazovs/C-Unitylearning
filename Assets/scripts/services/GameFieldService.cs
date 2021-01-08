using System;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldService : MonoBehaviour
{
    /*позиция героя */
    private Position heroPosition = new Position();

    /*позиция цели */
    private Position goalPosition = new Position();
    private Main mainComponent;
    private GameObjects _gameObjects;

    public GameFieldService(ref GameObjects gameObjects)
    {
        _gameObjects = gameObjects;
    }
    
    /*
* <summary>Основной метод - заполняет все поля на игровом поле</summary>
*/
    public void fillGameFields()
    {
        goalPosition.x = (float)(Math.Abs(_gameObjects.block.transform.position.x - (Constants.step * 0.5)) + _gameObjects.endPoint.transform.position.x) / Constants.step;
        goalPosition.y = (float)(Math.Abs(_gameObjects.block.transform.position.y - (Constants.step * 0.5)) + _gameObjects.endPoint.transform.position.y) / Constants.step;

        heroPosition.x = 1;
        heroPosition.y = Constants.fieldSize;

        mainComponent = _gameObjects.main.GetComponent<Main>();

        List<Card> safeFields = getSafeFields();
        List<Card> dangerousFields = getDangerousFields();

        setSafePathVectors(safeFields);
        setDangerousFields(dangerousFields);
    }

    public void setOpenField(Position openedField)
    {
         GameObject instance = Instantiate(_gameObjects.openField);
         instance.transform.position = new Vector3(
            instance.transform.position.x + (Constants.step / 2) + (Constants.step * openedField.x ),
            instance.transform.position.y + (Constants.step / 2) + (Constants.step * openedField.y),
            Constants.openedFieldZ
            );
    }
  
    /*
* <summary>Получает опасные поля из БД</summary>
*/
    private List<Card> getDangerousFields()
    {
        CardRepository repository = new CardRepository();

        return repository.getCardsBySafety(false);
    }

    /*
* <summary>Получает безопасные поля из БД</summary>
*/
    private List<Card> getSafeFields()
    {
        CardRepository repository = new CardRepository();

        return repository.getCardsBySafety(true);
    }


    /*
 * <summary>Создает безопасные поля </summary>
 */
    private void setSafePathVectors(List<Card> safeFields)
    {
        safeFields = Shuffler.listShuffler(safeFields);

        Position cursor = new Position
        {
            x = heroPosition.x,
            y = heroPosition.y
        };

        System.Random rnd = new System.Random(DateTime.Now.Millisecond);

        int directionKey;
        int safeFieldsIndex = 0;

        while (cursor.x != goalPosition.x
            || cursor.y != goalPosition.y)
        {
            //вычисляем расстояние до края поля в каждом направлении
            float upDistance = 10 - cursor.y;
            float downDistance = cursor.y - 1;
            float rightDistance = 10 - cursor.x;
            float leftDistance = cursor.x - 1;

            float[] distanceArr = { upDistance, downDistance, rightDistance, leftDistance };

            //ишем подходящее направление пока не найдем
            while (true)
            {
                directionKey = rnd.Next(0, 4);

                if (distanceArr[directionKey] > 0)
                {
                    break;
                }
            }

            //решаем, как далеко мы пойдем в этом направлении
            int distance = rnd.Next(0, (int)distanceArr[directionKey] + 1);

            Position endPoint = new Position();

            switch (directionKey)
            {
                case 0:
                    endPoint.x = cursor.x;
                    endPoint.y = cursor.y + distance;
                    break;
                case 1:
                    endPoint.x = cursor.x;
                    endPoint.y = cursor.y - distance;
                    break;
                case 2:
                    endPoint.x = cursor.x + distance;
                    endPoint.y = cursor.y;
                    break;
                case 3:
                    endPoint.x = cursor.x - distance;
                    endPoint.y = cursor.y;
                    break;
            }

            //идем в этом направлении пока не придем
            while (cursor.x != endPoint.x || cursor.y != endPoint.y)
            {
                switch (directionKey)
                {
                    //вверх
                    case 0:
                        cursor.y++;
                        break;
                    //вниз
                    case 1:
                        cursor.y--;
                        break;
                    //вправо
                    case 2:
                        cursor.x++;
                        break;
                    //влево
                    case 3:
                        cursor.x--;
                        break;
                }

                if (
                    (cursor.x != heroPosition.x || cursor.y != heroPosition.y)
                    && mainComponent.gameFields[(int)cursor.x - 1, (int)cursor.y - 1] == null
                    )
                {
                    Position arrayPosition = new Position();
                    arrayPosition.x = cursor.x - 1;
                    arrayPosition.y = cursor.y - 1;

                    safeFields[safeFieldsIndex].position = arrayPosition;

                    mainComponent.gameFields[(int)arrayPosition.x, (int)arrayPosition.y] = safeFields[safeFieldsIndex];
                    
                    safeFieldsIndex++;

                    safeFieldsIndex = safeFieldsIndex > safeFields.Count - 1 ? 0 : safeFieldsIndex;
                }
            }

            if ((cursor.x == goalPosition.x && cursor.y == goalPosition.y))
            {
                break;
            }
        }
    }

    /*
     * <summary>Создает опасные поля </summary>
     */
    private void setDangerousFields(List<Card> dangerousFields)
    {
        dangerousFields = Shuffler.listShuffler(dangerousFields);

        int dangerousFieldsIndex = 0;

        for (int i = 0; i < Constants.fieldSize; i++)
        {
            for (int j = 0; j < Constants.fieldSize; j++)
            {
                if (mainComponent.gameFields[i, j] == null)
                {
                    mainComponent.gameFields[i, j] = dangerousFields[dangerousFieldsIndex];

                    dangerousFieldsIndex++;

                    dangerousFieldsIndex = dangerousFieldsIndex > dangerousFields.Count - 1 ? 0 : dangerousFieldsIndex;
                }
            }
        }
    }
}
