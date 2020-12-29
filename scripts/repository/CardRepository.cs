
using System;
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
        string query = $"SELECT * FROM cards WHERE  is_safe= {safeToInt};";

        openConnection();

        IDataReader cardsDataReader = Execute(query);
        int i = 0;

        while (cardsDataReader.Read())
        {
            Card card = new Card();
            
            card.id = (Int64)cardsDataReader[0];
            card.text = cardsDataReader[1].ToString();
            card.imageFileName = cardsDataReader[2].ToString();
            card.isSafe = cardsDataReader[3].ToString() == "1" ? true : false;
            
            cards.Add(card);
            
            i++;
        }

        closeConnection();

        return cards;
    }
}
