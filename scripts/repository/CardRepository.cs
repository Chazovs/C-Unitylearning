
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CardRepository : AbstactRepository
{
    private List<Card> cards = new List<Card>();
    /**
    * <summary>Получить все безопасные или все небезопасные карточки</summary>
    */
    public List<Card> getCardsBySafety(bool isSafe)
    {
        int safeToInt = isSafe ? 1 : 0;
        string query = "SELECT * FROM cards";//$"SELECT * FROM cards WHERE  is_safe= {safeToInt};";
        openConnection();

        IDataReader cardsDataReader = Execute(query);
        int i = 0;

        while (cardsDataReader.Read())
        {
            cards.Add(new Card());
            Debug.Log("id: " + cardsDataReader[0].ToString());
            Debug.Log("val: " + cardsDataReader[1].ToString());
            Debug.Log("val: " + cardsDataReader[2].ToString());
            Debug.Log("val: " + cardsDataReader[3].ToString());
            Debug.Log("zzzzzzzzzzzzzzzzzz");
            i++;
        }
        closeConnection();
        return cards;
    }
}

