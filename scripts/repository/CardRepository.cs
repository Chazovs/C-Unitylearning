
using System.Data;
using UnityEngine;

public class CardRepository : AbstactRepository
{

    private Card[] cards;
    /**
    * <summary>Получить все безопасные или все небезопасные карточки</summary>
    */
    public Card[] getCardsBySafety(bool isSafe)
    {
        int safeToInt = isSafe ? 1 : 0;
        DataTable arrCards = GetTable($"SELECT * FROM cards ORDER  WHERE id_player = {safeToInt};");
        Debug.Log(arrCards.Rows[1][1]);
/*        int i = 0;
        foreach (object[] card in arrCards.Rows)
        {
            cards[i] = new Card();
            cards[i].isSafe = isSafe;
            cards[i].text = card[text_ru];
            cards[i].imageFileName = card[image_path];
            i++;
        }*/

        return cards;//TODO заглушка
    }
}
