using System.Collections.Generic;

public class Shuffler
{
    /**
     * перемешивать массивов
     */
    public static List<Card> listShuffler(List<Card> arr)
    {
        // создаем экземпляр класса Random для генерирования случайных чисел
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
}
