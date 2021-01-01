using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameFieldService
{
    /*позиция героя */
    private Position heroPosition = new Position();

    /*позиция цели */
    private Position goalPosition = new Position();

    private float fieldSize;

    private Card[,] gameFields;

    //Конструктор класса
    public GameFieldService(float fieldS, Position hero, Position goal)
    {
        goalPosition = goal;
        heroPosition = hero;
        fieldSize = fieldS;
        gameFields = new Card[(int) fieldSize, (int) fieldSize];
    }

    public Card[,] fillGameFields()
    {
        List<Card> safeFields = getSafeFields();
        List<Card> dangerousFields = getDangerousFields();

        setSafePathVectors(safeFields);
        setDangerousFields(dangerousFields);

        return gameFields;
    }

    private List<Card> getDangerousFields()
    {
        CardRepository repository = new CardRepository();

        return repository.getCardsBySafety(false);
    }

    private List<Card> getSafeFields()
    {
        CardRepository repository = new CardRepository();

        return repository.getCardsBySafety(true);
    }

    private void setSafePathVectors(List<Card> safeFields)
    {
        safeFields = Shuffler.listShuffler(safeFields);

        Position cursor = new Position();
        cursor.x = heroPosition.x;
        cursor.y = heroPosition.y;

        System.Random rnd = new System.Random(DateTime.Now.Millisecond);

        int directionKey;
        int safeFieldsIndex = 0;

        while (cursor.x != goalPosition.x || cursor.y != goalPosition.y)
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

                //если мы добрались до цели
                if ((cursor.x == goalPosition.x && cursor.y == goalPosition.y))
                {
                    Card winCard = new Card();
                    winCard.isWin = true;
                    gameFields[(int)cursor.x - 1, (int)cursor.y - 1] = winCard;
                }

                if (
                    (cursor.x != heroPosition.x || cursor.y != heroPosition.y)
                    && gameFields[(int)cursor.x - 1, (int)cursor.y - 1] == null
                    )
                {
                    Position arrayPosition = new Position();
                    arrayPosition.x = cursor.x - 1;
                    arrayPosition.y = cursor.y - 1;

                    safeFields[safeFieldsIndex].position = arrayPosition;

                    gameFields[(int)arrayPosition.x, (int)arrayPosition.y] = safeFields[safeFieldsIndex];
                }
            }

            if ((cursor.x == goalPosition.x && cursor.y == goalPosition.y))
            {
                break;
            }

            safeFieldsIndex++;

            safeFieldsIndex = safeFieldsIndex > safeFields.Count - 1 ? 0 : safeFieldsIndex;
        }
    }

    private void setDangerousFields(List<Card> dangerousFields)
    {
        dangerousFields = Shuffler.listShuffler(dangerousFields);

        int dangerousFieldsIndex = 0;

        for (int i = 0; i < fieldSize; i++)
        {
            for (int j = 0; j < fieldSize; j++)
            {
                if (gameFields[i, j] == null)
                {
                    gameFields[i, j] = dangerousFields[dangerousFieldsIndex];

                    dangerousFieldsIndex++;

                    dangerousFieldsIndex = dangerousFieldsIndex > dangerousFields.Count - 1 ? 0 : dangerousFieldsIndex;
                }
            }
        }
    }
}
