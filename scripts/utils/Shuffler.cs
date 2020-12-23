public class Shuffler
{
    /**
     * перемешивать массивов
     */
    public static int[] arrayShuffler(int[] arr)
    {
        // создаем экземпляр класса Random для генерирования случайных чисел
        System.Random rand = new System.Random();

        for (int i = arr.Length - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            int tmp = arr[j];
            arr[j] = arr[i];
            arr[i] = tmp;
        }

        return arr;
    }
}
