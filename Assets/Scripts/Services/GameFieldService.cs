using System.Collections.Generic;
using UnityEngine;
using Rand = UnityEngine.Random;

public class GameFieldService
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
        gameFields = SetDangerousFields(gameData.cards.danger);
        CreateMaze();
    }

    public void SetGoalImg()
    {
        GameObjects.endPoint.transform.position = new Vector3(
            (GameObjects.endPoint.transform.position.x + Constants.step / 2) + (Constants.step * goalPosition.x) - Constants.step,
            (GameObjects.endPoint.transform.position.y + Constants.step / 2) + (Constants.step * goalPosition.y) - Constants.step,
            GameObjects.endPoint.transform.position.z
            );
    }

    private Card SetStartFields(Position heroPosition)
    {
        return gameFields[heroPosition.arX(), heroPosition.arY()] 
            = new Card() { isStart = true, position = heroPosition };
    }

    public void CreateMaze()
    {
        Position heroPosition = ServiceLocator.GetService<HeroService>().SetHeroPosition();
        Debug.Log("Респаун X:" + heroPosition.x + " Y:" + heroPosition.y);

        Card current = SetStartFields(heroPosition);
        Stack<Card> way = new Stack<Card>();

        way.Push(current);
        
        int mazeLength = Rand.Range(Settings.mazeLengthMin, Settings.mazeLengthMax);

        safeFieldsIndex = 0;

        while (mazeLength > safeFieldsIndex)
        {
            List<Card> availableCards = GetAvailableCards(current);
            Debug.Log("availableCards.Count" + availableCards.Count);

            if (availableCards.Count == 0)
            {
                current = way.Pop();
                continue;
            }

            Card directionCard = availableCards[Rand.Range(0, availableCards.Count)];

            gameData.cards.safety[safeFieldsIndex].position = directionCard.position;

            current 
                = gameFields[directionCard.position.arX(), directionCard.position.arY()]
                = gameData.cards.safety[safeFieldsIndex];

            safeFieldsIndex++;

            Debug.Log("X:" + current.position.x + " Y:" + current.position.y);
            way.Push(current);
        };

        current.isWin = true;
        goalPosition = current.position;
        SetGoalImg();
    }

    private List<Card> GetAvailableCards(Card current)
    {
        List<Card> availableCards = new List<Card>();

        foreach (Card surroundingCard in GetSurroundingCards(current.position))
        {
            if (IssetContactCard(surroundingCard.position)) continue;
            
            Debug.Log("sX:" + surroundingCard.position.x + " sY:" + surroundingCard.position.y);
            availableCards.Add(surroundingCard);
        }

        return availableCards;
    }

    private bool IssetContactCard(Position cardPosition)
    {
        if (GetEmptyFieldsCount(cardPosition) == 3) return false; // не касается

        return true;
    }

    private int GetEmptyFieldsCount(Position cardPosition)
    {
        return GetSurroundingCards(cardPosition).Count + GetOverSideCount(cardPosition);
    }

    private int GetOverSideCount(Position cardPosition)
    {
        int OverSideCount = 0;

        if (cardPosition.GetLeft() == null) OverSideCount++;
        if (cardPosition.GetRight() == null) OverSideCount++;
        if (cardPosition.GetUp() == null) OverSideCount++;
        if (cardPosition.GetDown() == null) OverSideCount++;

        return OverSideCount;
    }

    private List<Card> GetSurroundingCards(Position current)
    {
        List<Card> surroundingCards = new List<Card>();

        Position left = current.GetLeft();
        Position rigth = current.GetRight();
        Position up = current.GetUp();
        Position down = current.GetDown();

        if (left != null) surroundingCards.Add(gameFields[left.arX(), left.arY()]);
        if (rigth != null) surroundingCards.Add(gameFields[rigth.arX(), rigth.arY()]);
        if (up != null) surroundingCards.Add(gameFields[up.arX(), up.arY()]);
        if (down != null) surroundingCards.Add(gameFields[down.arX(), down.arY()]);

        return FilteringAvailableCards(surroundingCards);
    }

    private List<Card> FilteringAvailableCards(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].isStart || cards[i].isSafe)
            {
                cards.RemoveAt(i);
                i--;
            }
        }

        return cards;
    }

    public void setOpenField(Position openedField)
    {
         GameObject instance = UnityEngine.Object.Instantiate(GameObjects.openField);
         instance.transform.position = new Vector3(
            instance.transform.position.x + (Constants.step / 2) + (Constants.step * openedField.x ) - Constants.step,
            instance.transform.position.y + (Constants.step / 2) + (Constants.step * openedField.y) - Constants.step,
            Constants.openedFieldZ
            );
    }

    /*
     * <summary>Создает опасные поля? пропуская начало и конец </summary>
     */
    private Card[,] SetDangerousFields( List<Card> dangerCards)
    {
        Card[,] dangerFields = new Card[Constants.fieldSize, Constants.fieldSize];

        int dangerousFieldsIndex = 0;

        for (int i = 1; i <= Constants.fieldSize; i++)
        {
            for (int j = 1; j <= Constants.fieldSize; j++)
            {
                Position cardPosition = new Position() { x = i, y = j };

                    Card dangerCard = new Card();
                    dangerCard.id = dangerCards[dangerousFieldsIndex].id;
                    dangerCard.imageName = dangerCards[dangerousFieldsIndex].imageName;
                    dangerCard.text = dangerCards[dangerousFieldsIndex].text;
                    dangerCard.isOpen = false;
                    dangerCard.isSafe = false;
                    dangerCard.isStart = false;
                    dangerCard.isWin = false;
                    dangerCard.position = cardPosition;

                    dangerFields[cardPosition.arX(), cardPosition.arY()] = dangerCard;

                    dangerousFieldsIndex++;

                    dangerousFieldsIndex 
                        = dangerousFieldsIndex > dangerCards.Count - 1 ? 0 : dangerousFieldsIndex;
            }
        }

        return dangerFields;
    }
}
