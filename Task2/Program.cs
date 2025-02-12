using System.Text.RegularExpressions;
using System.Collections.Generic;

internal class Program
{
    public static void Main()
    {
        // Считываем из командной строки количество наборов входных данных
        Console.WriteLine("Введите количество наборов (одно целое число): ");
        var dataCount = ReadIntValue();

        // Считываем из командной строки информацию о попытках регистрации
        var userData = new List<(int AttemptCount, List<string> Logins)>();
        for (var i = 0; i < dataCount; i++)
        {
            Console.WriteLine("Введите количество вводов логинов (одно целое число): ");
            var attemptCount = ReadIntValue();
            var logins = new List<string>(attemptCount);
            Console.WriteLine("Ввод логинов: ");
            for (int j = 0; j < attemptCount; j++)
            {
                
                var login = Console.ReadLine()!;
                logins.Add(login);
            }

            userData.Add((attemptCount, logins));
        }




        //блок обработки введенной информации
        List<List<string>> resultList = new List<List<string>>();

        foreach (var user in userData)
        {

            HashSet<string> uniqueLogins = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            List<string> userItemList = new List<string>();

            
            foreach (var login in user.Logins)
            {
                // Проверка, был ли логин ранее
                
                if (uniqueLogins.Contains(login) || !IsValidLogin(login)) //
                {
                    userItemList.Add("NO");
                    
                }
                else
                {
                    userItemList.Add("YES");
                    
                    uniqueLogins.Add(login); // Добавляем логин в набор уникальных логинов
                }
            }

            resultList.Add(userItemList);


        }





        // TODO: Реализовать вычисление результатов
        /*var results = new[]
        {
            new[] { "NO", "YES", "YES", "YES", "NO", "YES", "NO", "YES", "NO", "NO" },
            new[] { "NO", "YES", "YES", "NO", "YES", "YES", "NO", "YES" },
            new[] { "YES", "NO", "NO", "NO", "YES" },
        };*/

        Console.WriteLine("Вывод результатов: ");
        foreach (var result in resultList)
        {
            foreach (var answer in result)
            {
                Console.WriteLine(answer);
            }

            Console.WriteLine();
        }
    }

    //процедура валидации логина
    private static bool IsValidLogin(string login)
    {
        // Проверка длины логина
        if (login.Length < 2 || login.Length > 24)
        {
            return false;
        }

        // Проверка первого символа
        if (login[0] == '-')
        {
            return false;
        }

        // Регулярное выражение для проверки допустимых символов
        string pattern = @"^[a-zA-Z0-9_-]+$";
        return Regex.IsMatch(login, pattern);
    }

    private static int ReadIntValue()
        => int.Parse(Console.ReadLine()!);

    
}