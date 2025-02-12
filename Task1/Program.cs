using System;


internal class Program
{
    public static void Main()
    {
        // Считываем из командной строки количество наборов входных данных
        Console.WriteLine("Введите количество корзин (одно целое число): ");
        var dataCount = ReadIntValue();

        // Считываем из командной строки информацию о каждой корзине
        var baskets = new List<(int Count, int[] Prices)>();
        for (var i = 0; i < dataCount; i++)
        {
            Console.WriteLine("Введите количество покупок в корзине " + (i+1) + " (одно целое число): ");
            var count = ReadIntValue();
            Console.WriteLine("Введите список цен в корзине " + (i+1) + " (целые числа через пробел в одной строке, количество цен должно быть равно "+ count + "): ");
            var prices = ReadIntValuesFromLine().ToArray();
            if (prices.Count() == count)
            {
                baskets.Add((count, prices));
            }
            else
            {
                Console.WriteLine("Количество цен не равно количеству покупок! Повторите попытку ввода");
                return;
            }
            
        }


        //обработка корзин
        // Словарь для хранения уникальных чисел и их количества
        //Dictionary<int, int> uniqueCount = new Dictionary<int, int>();

        List<int> resultbasket = new List<int>();

        foreach (var basket in baskets)
        {
            Dictionary<int, int> uniqueCount = new Dictionary<int, int>();
            //Console.WriteLine(basket.Prices);

            // Подсчет уникальных чисел
            foreach (int number in basket.Prices)
            {
                if (uniqueCount.ContainsKey(number))
                {
                    uniqueCount[number]++;
                }
                else
                {
                    uniqueCount[number] = 1;
                }
            }

            //посчитаем суммы по каждой корзине
            double kc = 0;
            int ostatok = 0;
            int polkol = 0;
            int basketitsum = 0; //для рассчета общей суммы по всем покупкам в корзине
            foreach (var kvp in uniqueCount)
            {
                //int basketitsum = 0; //если нужно каждую покупку считать
                //Console.WriteLine($"Key:[{kvp.Key}] Value:[{kvp.Value}]");
                double n = Convert.ToDouble(kvp.Value) / 3;
                kc = Math.Truncate(n);      //количество троек
                
                if (n > 1)
                {
                    ostatok = kvp.Value - Convert.ToInt32(kc)*3;
                    polkol = Convert.ToInt32(kc) * 2 + ostatok;
                } else
                {
                    if (n == 1)
                    {
                        
                        polkol = Convert.ToInt32(kc) * 2;
                    }

                    if (n < 1)
                    {
                        
                        polkol = kvp.Value;
                    }
                        
                }

                basketitsum = basketitsum + polkol * kvp.Key;

                //resultbasket.Add(basketitsum); // для сумм по каждому товару

            }
            resultbasket.Add(basketitsum); //для общей суммы корзины


        }





        // TODO: Реализовать вычисление результатов
        //var results = new[] { 22, 22, 10000, 12, 40000, 1100 };
        //foreach (var result in results)
        Console.WriteLine("Результирующий список цен всех корзин: ");
        foreach (var result in resultbasket)
        {
            Console.WriteLine(result);
        }
    }
    private static IEnumerable<int> ReadIntValuesFromLine()
            => Console.ReadLine()!.Split(" ").Select(int.Parse);

    private static int ReadIntValue()
            => int.Parse(Console.ReadLine()!);

}