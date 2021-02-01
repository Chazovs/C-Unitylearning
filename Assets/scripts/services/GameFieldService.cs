using System;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldService : MonoBehaviour
{
    private int safeFieldsIndex;
    
    /*
    * <summary>Основной метод - заполняет все поля на игровом поле</summary>
    */
    public void FillGameFields()
    {
        Main.gameData.cards.danger
           = Shuffler.listShuffler(Main.gameData.cards.danger);

        Main.gameData.cards.safety
           = Shuffler.listShuffler(Main.gameData.cards.safety);

        SetGoalAndStartFields();
        SetDangerousFields();
        SetSafetyCardsInField();
    }

    private void SetGoalAndStartFields()
    {
        Card goal = new Card() { isWin = true, position = Main.goalPosition };
        Main.gameFields[(int)Main.goalPosition.x-1, (int)Main.goalPosition.y-1] = goal;

        Card start = new Card() { isStart = true, position = Main.heroPosition };
        Main.gameFields[(int)Main.heroPosition.x-1, (int)Main.heroPosition.y-1] = start;
    }

    public void SetSafetyCardsInField()
    {
        safeFieldsIndex = 0;

        System.Random rnd = new System.Random(DateTime.Now.Millisecond);

        Stack<Card> way = new Stack<Card>();

        Card current = Main.gameFields[(int)Main.goalPosition.x - 1, (int)Main.goalPosition.y - 1];

        way.Push(current);

        do {
            List<Card> availableCards = GetAvailableCards(current);

            if (availableCards.Count == 0) 
            {
                current = way.Pop();
                continue;
            }
           
            int rndIndex = rnd.Next(0, availableCards.Count);
            Card directionCard = availableCards[rndIndex];

            if(directionCard.position.x == Constants.startPosition.x 
                && directionCard.position.y == Constants.startPosition.y
                )
            {
                break;
            }

            Main.gameData.cards.safety[safeFieldsIndex].position = directionCard.position;
            Main.gameFields[(int)directionCard.position.x - 1, (int)directionCard.position.y - 1] 
                = Main.gameData.cards.safety[safeFieldsIndex];
            
            safeFieldsIndex++;

            current = Main.gameFields[(int)directionCard.position.x - 1, (int)directionCard.position.y - 1];

            way.Push(current);

        } while (way.Count > 0);
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
            if (surroundingCard != null && surroundingCard.isSafe == false)
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

            Card checkingCard = Main.gameFields[(int)x - 1, (int)y - 1];

            if (checkingCard.isWin || checkingCard.isSafe) return true;
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
            return Main.gameFields[(int)current.x + (int)distanse.x - 1, (int) current.y + (int)distanse.y - 1];
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
                if (Main.gameFields[i, j] == null)
                {
                    if (Main.gameFields[i, j] != null 
                        && (Main.gameFields[i, j].isWin 
                        || Main.gameFields[i, j].isStart)) continue;

                    Main.gameFields[i, j] 
                        = Main.gameData.cards.danger[dangerousFieldsIndex];
                    Main.gameFields[i, j].position 
                        = new Position() { x = i + 1, y = j + 1 };

                    dangerousFieldsIndex++;

                    dangerousFieldsIndex 
                        = dangerousFieldsIndex > Main.gameData.cards.danger.Count - 1 ? 0 : dangerousFieldsIndex;
                }
            }
        }
    }
}
