using System.Collections.Generic;

public class Shuffler
{
    /**
     * перемешивать карточки игровые
     */
    public static List<Card> listShuffler(List<Card> arr)
    {
        System.Random rand = new System.Random();

        for (int i = arr.Count - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            Card tmp = arr[j];
            arr[j] = arr[i];
            arr[i] = tmp;
        }

        return arr;
    }

    /**
     * перемешивать массивы объектов
     */
    public static List<Position> PositionsShuffler(List<Position> arr)
    {
        System.Random rand = new System.Random();

        for (int i = arr.Count - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            Position tmp = arr[j];
            arr[j] = arr[i];
            arr[i] = tmp;
        }

        return arr;
    }
}
