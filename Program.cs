using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace БанкиДиана
{
    class Program
    {
        static void Main(string[] args)
        {
            int id;
            string FIO;
            int accountNumber; //номер счета
            double money;

            Console.Write("Укажите количество клиентов банка: ");
            int accountsNumber = Convert.ToInt32(Console.ReadLine());

            Random rnd = new Random();

            Bank[] accounts = new Bank[accountsNumber];
            Bank bank = new Bank();

            Console.WriteLine();
            for (int i = 0; i < accounts.Length; i++)
            {
                Console.WriteLine($"Клиент {i + 1}");
                accounts[i] = new Bank();

                id = i + 1; //записываем айдишник

                Console.Write("Введите ваше ФИО: ");
                FIO = Console.ReadLine();

                accountNumber = rnd.Next(123455600, 999927839);

                Console.Write("Укажите, сколько денег хотите положить на счет: ");
                money = Convert.ToDouble(Console.ReadLine());

                accounts[i].userInfo(id, FIO, accountNumber, money); //закидываем значения отсюда в класс

                Console.WriteLine("\nИнформация о счете: ");
                accounts[i].accountInfo(); //выводим информацию об аккаунте

                Console.ReadKey();
                Console.Clear();
            }

            while (true)
            {
                Console.WriteLine("Выберите счёт, на который хотите зайти (Для выхода укажите 0): ");
                bank.accountToChoose(accounts, accounts.Length, accounts.Length); //закидываем длину массива чтобы не сработало условие if

                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 0)
                {
                    Console.WriteLine("Конец программы");
                    break;
                }
                else
                {
                    int accountIndex = choice - 1;

                    while (choice > accounts.Length || accountIndex < 0)     //проверка на дурака
                    {
                        Console.WriteLine("Такого счёта не существует. Попробуйте ввести снова: ");
                        choice = Convert.ToInt32(Console.ReadLine());    //Выбираем номер счета
                        accountIndex = choice - 1;                       //Представили номер счёта как индекс в массиве
                    }

                    accounts[accountIndex].accountInfo(); //выводим информацию о выбранном аккаунте

                    Console.WriteLine("\nВыберите действие:\n1.Положить деньги на счёт\n2.Снять деньги со счёта\n3.Обнулить счёт\n4.Перевести сумму с одного счёта на другой.\n5.Выход");
                    int actionChoice = Convert.ToInt32(Console.ReadLine());

                    accounts[accountIndex].Actions(accounts, actionChoice, accountIndex, choice);
                }
            }
        }

        static void output(Bank[] accounts) //выводим информацию о счетах
        {
            for (int i = 0; i < accounts.Length; i++) //Вывод каждого счёте
            {
                Console.WriteLine($"Счёт {i + 1}");
                accounts[i].accountInfo();

                Console.WriteLine();
            }
        }
    }
}
