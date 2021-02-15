using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameFieldService : MonoBehaviour
{

    public GetCardResponse gameData;
    public Position goalPosition = new Position();
    public Card[,] gameFields = new Card[Constants.fieldSize, Constants.fieldSize];
    private int safeFieldsIndex;

    /*
    * <summary>Основной метод - заполняет все поля на игровом поле</summary>
    */
    public void FillGameFields()
    {
        gameData.cards.danger = Shuffler.listShuffler(gameData.cards.danger);
        gameData.cards.safety = Shuffler.listShuffler(gameData.cards.safety);

        SetDangerousFields();
        SetGoalAndStartFields();
        CreateMaze();
        /*SetSafetyCardsInField();*/
    }
    public void SetGoalImg()
    {
        GameObjects.endPoint.transform.position = new Vector3(
            (GameObjects.endPoint.transform.position.x + Constants.step / 2) + (Constants.step * goalPosition.x) - Constants.step,
            (GameObjects.endPoint.transform.position.y + Constants.step / 2) + (Constants.step * goalPosition.y) - Constants.step,
            GameObjects.endPoint.transform.position.z
            );

    }

    private void SetGoalAndStartFields()
    {
        HeroService heroService = ServiceLocator.GetService<HeroService>();

        Card start = new Card() { isStart = true, position = heroService.heroPosition };
        gameFields[(int)heroService.heroPosition.x - 1, (int)heroService.heroPosition.y - 1] = start;
    }

    public void CreateMaze()
    {
        HeroService heroService = ServiceLocator.GetService<HeroService>();
        safeFieldsIndex = 0;
        System.Random rnd = new System.Random(DateTime.Now.Millisecond);
        Stack<Card> way = new Stack<Card>();

        Card current = gameFields[(int)heroService.heroPosition.x - 1, (int)heroService.heroPosition.y - 1];

        way.Push(current);

        int mazeLength = rnd.Next(Settings.mazeLengthMin, Settings.mazeLengthMax);

        while (mazeLength > safeFieldsIndex) {

            List<Card> availableCards = GetAvailableCards(current);

            if (availableCards.Count == 0)
            {
                current = way.Pop();
                continue;
            }

            int rndIndex = rnd.Next(0, availableCards.Count);
            Card directionCard = availableCards[rndIndex];

            gameData.cards.safety[safeFieldsIndex].position = directionCard.position;
            gameFields[(int)directionCard.position.x - 1, (int)directionCard.position.y - 1]
                = gameData.cards.safety[safeFieldsIndex];

            safeFieldsIndex++;

            current = gameFields[(int)directionCard.position.x - 1, (int)directionCard.position.y - 1];

            way.Push(current);
        };

        current.isWin = true;
        goalPosition = current.position;
        SetGoalImg();
    }

    private List<Card> GetAvailableCards(Card current)
    {
        List<Card> availableCards = new List<Card>();

        List<Card> surroundingCards = new List<Card> { 
            GetCard(Constants.leftPosition, current.position),
            GetCard(Constants.rightPosition, current.position),
            GetCard(Constants.upPosition, current.position),
            GetCard(Constants.downPosition, current.position)
        };

        foreach(Card surroundingCard in surroundingCards)
        {
            if (
                surroundingCard != null 
                && surroundingCard.isSafe == false 
                && surroundingCard.isStart != true
                )
            {
                bool isСontact = IssetContactCard(surroundingCard.position, current.position);

                if (!isСontact) availableCards.Add(surroundingCard);
            }
        }

        return availableCards;
    }

    //если касается то true
    private bool IssetContactCard(Position surrounding, Position current)
    {
        foreach(Position check in Constants.adjacentPositions)
        {
            float x = surrounding.x + check.x;
            float y = surrounding.y + check.y;

            if ((current.x == x && current.y == y) || x > 10 || x < 1 || y >10 || y < 1) continue;

            Card checkingCard = gameFields[(int)x - 1, (int)y - 1];

            if (checkingCard.isStart || checkingCard.isSafe) return true;
        }

        return false;
    }

    private Card GetCard(Position distanse, Position current)
    {
        if (current.x + distanse.x > 0
            && current.x + distanse.x < 11
            && current.y + distanse.y < 11
            && current.y + distanse.y > 0
            ) {
            return gameFields[(int)current.x + (int)distanse.x - 1, (int) current.y + (int)distanse.y - 1];
        }

        return null;
    }

    public void setOpenField(Position openedField)
    {
         GameObject instance = Instantiate(GameObjects.openField);
         instance.transform.position = new Vector3(
            instance.transform.position.x + (Constants.step / 2) + (Constants.step * openedField.x ) - Constants.step,
            instance.transform.position.y + (Constants.step / 2) + (Constants.step * openedField.y) - Constants.step,
            Constants.openedFieldZ
            );
    }

    /*
     * <summary>Создает опасные поля? пропуская начало и конец </summary>
     */
    private void SetDangerousFields()
    {
        int dangerousFieldsIndex = 0;

        for (int i = 0; i < Constants.fieldSize; i++)
        {
            for (int j = 0; j < Constants.fieldSize; j++)
            {
                if (gameFields[i, j] == null)
                {
                    gameFields[i, j] 
                        = gameData.cards.danger[dangerousFieldsIndex];
                    gameFields[i, j].position 
                        = new Position() { x = i + 1, y = j + 1 };

                    dangerousFieldsIndex++;

                    dangerousFieldsIndex 
                        = dangerousFieldsIndex > gameData.cards.danger.Count - 1 ? 0 : dangerousFieldsIndex;
                }
            }
        }
    }
}
