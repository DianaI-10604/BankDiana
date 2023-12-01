using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace БанкиДиана
{ 
    class Bank
    {
        private int id;
        private string FIO;
        private int accountNumber; //номер счета
        private double money;

        public void userInfo(int id, string FIO, int accountNumber, double money) //закинем из Main сюда
        {
            this.id = id;
            this.FIO = FIO;
            this.accountNumber = accountNumber;
            this.money = money;
        }

        public void accountToChoose(Bank[] accounts, int j, int choice)//Вывод всех счетов
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (i == j) //пропускаем того чела, с которого зашли на аккаунт
                {
                    continue;
                }
                else//Выводим номер счёта
                {
                    Console.WriteLine($"{accounts[i].id}. {accounts[i].FIO}, {accounts[i].accountNumber}");
                }
            }
        }

        public void Actions(Bank[] accounts, int actionChoice, int accountIndex, int choice)
        {
            switch (actionChoice)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Вы выбрали положить деньги на счёт.");
                    accounts[accountIndex].dob(accounts, accountIndex);
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("Вы выбрали снять деньги со счёта.");
                    accounts[accountIndex].umen(accounts, accountIndex);
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("Вы выбрали обнулить счёт.");
                    accounts[accountIndex].obnul(accounts, accountIndex);
                    break;

                case 4:
                    Console.Clear();
                    Console.WriteLine("Вы выбрали перевести сумму с одного счёта на другой");
                    Console.WriteLine("Выберите счет для перевода: ");
                    accountToChoose(accounts, accountIndex, choice);//Вывод всех счетов

                    int chooseToSend = Convert.ToInt32(Console.ReadLine()); //это айди того, кому переводим
                    int destinationIndex = chooseToSend - 1;                //это индекс массива

                    accounts[accountIndex].sendMoney(accounts, actionChoice, accountIndex, destinationIndex);
                    break;

                case 5:
                    Console.WriteLine("Вы выбрали выйти");
                    Console.Clear();
                    return;

                default:
                    Console.Clear();
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
        }

        private void sendMoney(Bank[] accounts, int actionChoice, int accountIndex, int destinationIndex) //перевести деньги с одного счета на другой
        {
            Console.Write("Укажите, сколько хотите перевести: ");
            double sendMoney = Convert.ToDouble(Console.ReadLine());

            while (sendMoney < 0 || accounts[accountIndex].money < sendMoney)
            {
                Console.WriteLine("На вашем счетe не хватает денег. Попробуйте снова:");
                sendMoney = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine($"Отправлено {sendMoney} руб");

            Console.WriteLine();

            Console.Write("Нажмите любую клавишу для вывода информации о счетах: ");
            Console.ReadKey();
            Console.Clear();

            accounts[accountIndex].money -= sendMoney;       //перевели деньги
            accounts[destinationIndex].money += sendMoney;   //получили деньги

            Console.WriteLine("Информация о счете, на который переводились деньги: ");
            accounts[destinationIndex].accountInfo();

            Console.ReadKey();
            Console.WriteLine();

            Console.WriteLine("Информация о счете, с которого переводились деньги: ");
            accounts[accountIndex].accountInfo();

            Console.WriteLine();
        }

        private void obnul(Bank[] accounts, int accountIndex) //обнулить счет
        {
            accounts[accountIndex].money = 0;
            Console.WriteLine($"теперь на вашем счете {accounts[accountIndex].money} руб");

            Console.WriteLine();
        }

        private void dob(Bank[] accounts, int accountIndex) //пооложить деньги на счет
        {
            Console.Write("Укажите сумму для пополнения: ");
            double dob = Convert.ToDouble(Console.ReadLine());

            while (dob <= 0) //проверка на дурака
            {
                Console.WriteLine("Нельзя перевести отрицательное количество денег или равное 0. Попробуйте снова:");
                dob = Convert.ToDouble(Console.ReadLine());
            }

            accounts[accountIndex].money += dob; //записали значение
            Console.WriteLine($"Теперь на счетe {accounts[accountIndex].money} руб");

            Console.WriteLine();
        }

        public void umen(Bank[] accounts, int accountIndex)  //снять деньги со счета
        {
            Console.Clear();
            Console.Write("Сколько денег вы хотите вывести со счёта?: ");
            double minus = Convert.ToDouble(Console.ReadLine());

            while (minus > accounts[accountIndex].money)
            {
                Console.WriteLine("У вас не хватает денег для вывода. Попробуйте выбрать сумму поменьше.");
                minus = Convert.ToDouble(Console.ReadLine());
            }

            accounts[accountIndex].money -= minus;

            Console.WriteLine($"Со счета выведено {minus} руб");
            Console.WriteLine($"Осталось {accounts[accountIndex].money} руб");

            Console.WriteLine();
        }

        public void accountInfo() //информация о свете
        {
            Console.WriteLine($"ID: {id}");
            Console.WriteLine($"ФИО: {FIO}");
            Console.WriteLine($"Номер счета: {accountNumber}");
            Console.WriteLine($"Средств на счете: {money} руб");
        }
    }
}
