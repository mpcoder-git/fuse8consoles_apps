using System.Linq;

internal class Program
{
    public static void Main()
    {
        // Считываем из командной строки количество процессоров и задач
        Console.WriteLine("Введите количество процессоров и количество задач (целые числа через пробел): ");
        var firstLine = ReadIntValuesFromLine().ToArray();
        var processorCount = firstLine[0];
        var tasksCount = firstLine[1];

        // Считываем из командной строки мощности процессоров
        Console.WriteLine("Введите мощность процессоров (целые числа через пробел по количеству процессоров): ");
        var processorsPower = ReadIntValuesFromLine().ToArray();

        Console.WriteLine("Введите задачи (два целых числа через пробел в одну строку, количество строк равно количеству задач): ");
        // Считываем из командной строки данные о задачах
        var tasks = new List<(int StartedAt, int Duration)>();
        var taskIndex = 1;
        while (taskIndex <= tasksCount)
        {
            
            var taskData = ReadIntValuesFromLine().ToArray();
            tasks.Add((StartedAt: taskData[0], Duration: taskData[1]));

            taskIndex++;
        }



        //алгоритм

        int[,] planer = new int[processorCount, 4];
        int linep = 0;
        foreach (var ppow in processorsPower)
        {
            planer[linep, 0] = ppow;
            linep++;
        }

        int sum_power = 0; //итоговое потребление энергии

        //цикл эмитация секундного хода - 1 итерация равна 1 секунде (будем выполнять 20 секунд)
        for (int sec = 1; sec < 20; sec++)
        {

            //к запущеным задачам прибавить 1 секунду
            //если прибавление выйдет за период - то обнулить строку

            for (int i = 0; i < planer.GetLength(0); i++)
            {
                //перебор таблицы
                int status = planer[i, 1];
                if (status == 1)
                {
                    //увеличим время работы каждой задачи если не вышло время работы
                    //если на текущем тике время работы вышло - то освобождаем процессор
                    //секунду запуска задания  считаем за время работы. 
                    if (planer[i, 3] + 1 <= planer[i, 2])
                    {
                        planer[i, 3] = planer[i, 3] + 1;
                        sum_power = sum_power + 1 * planer[i, 0];
                    }
                    else
                    {
                        planer[i, 1] = 0;
                        planer[i, 2] = 0;
                        planer[i, 3] = 0;

                    }

                }


            }



            // Поиск кортежа по значению StartedAt
            var task = tasks.FirstOrDefault(t => t.StartedAt == sec);
            
            if (task != default)
            {
                //Console.WriteLine($"Найдено: StartedAt = {task.StartedAt}, Duration = {task.Duration}");

                





                // Переменные для поиска минимального потребления
                int minEnergy = int.MaxValue;
                int minIndex = -1;
                
                // Поиск неиспользуемого процессора с минимальным потреблением
                for (int i = 0; i < planer.GetLength(0); i++)
                {
                    int energy = planer[i, 0];
                    int status = planer[i, 1];

                    if (status == 0) // Если процессор не используется
                    {
                        if (energy < minEnergy)
                        {
                            minEnergy = energy;
                            minIndex = i;
                        }
                    }

                    
                }

                if (minIndex != -1)
                {
                    //Console.WriteLine($"Неиспользуемый процессор с минимальным потреблением энергии: {minEnergy}");
                    planer[minIndex, 1] = 1;
                    planer[minIndex, 2] = task.Duration;
                    planer[minIndex, 3] = 1; //секунда начала будет считаться секундой в работе

                }
                else
                {
                    //Console.WriteLine("Все процессоры используются.");
                }



            }
            else
            {
                continue;
            }

        }



        // TODO: Реализовать вычисление результата
        //var result = 105;
        Console.WriteLine("Суммарное потребление энергии после выполнения всех заданий равно "+sum_power);
    }

    private static IEnumerable<int> ReadIntValuesFromLine()
        => Console.ReadLine()!.Split(" ").Select(int.Parse);
}