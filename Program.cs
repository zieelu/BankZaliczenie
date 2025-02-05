using System;
using System.Collections.Generic;

class Program
{
    class Client
    {
        public string FullName { get; set; }
        public decimal Balance { get; set; }

        public Client(string fullName, decimal balance)
        {
            FullName = fullName;
            Balance = balance;
        }
    }

    static void Main(string[] args)
    {
        List<Client> clients = new List<Client>
        {
            new Client("Jan Motylski", 1500m),
            new Client("Anna Nowak", 3000m),
            new Client("Piotr Wiśniewski", 1500m)
        };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Bank Zaliczenie ===");
            Console.WriteLine("1. Pokaż listę klientów");
            Console.WriteLine("2. Zrób przelew");
            Console.WriteLine("3. Wyjście");
            Console.Write("Wybierz jedną z opcji: ");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ShowClients(clients);
                    break;
                case "2":
                    MakeTransfer(clients);
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Nieprawidłowy wybór. Naciśnij dowolny klawisz aby wrócić do menu głównego.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void ShowClients(List<Client> clients)
    {
        Console.WriteLine("\n=== Klienci ===");
        for (int i = 0; i < clients.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {clients[i].FullName} - Stan konta: {clients[i].Balance:C}");
        }
        Console.WriteLine("\nNaciśnij dowolny klawisz aby wrócić do menu głównego.");
        Console.ReadKey();
    }

    static void MakeTransfer(List<Client> clients)
    {
        ShowClients(clients);

        Console.Write("\nWprowadź numer nadawcy: ");
        if (!int.TryParse(Console.ReadLine(), out int senderIndex) || senderIndex < 1 || senderIndex > clients.Count)
        {
            Console.WriteLine("Nieprawidłowy wybór. Naciśnij dowolny klawisz aby wrócić do menu głównego.");
            Console.ReadKey();
            return;
        }

        Console.Write("Wprwoadź numer odbiorcy:");
        if (!int.TryParse(Console.ReadLine(), out int receiverIndex) || receiverIndex < 1 || receiverIndex > clients.Count || receiverIndex == senderIndex)
        {
            Console.WriteLine("Nieprawidłowy numer klienta. Naciśnij dowolny klawisz aby wrócić do menu głównego.");
            Console.ReadKey();
            return;
        }

        Console.Write("Wprowadz kwotę przelewu: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
        {
            Console.WriteLine("Nieprawidłowa kwota.Naciśnij dowolny klawisz aby wrócić do menu głównego. ");
            Console.ReadKey();
            return;
        }

        Client sender = clients[senderIndex - 1];
        Client receiver = clients[receiverIndex - 1];

        if (sender.Balance < amount)
        {
            Console.WriteLine("Niewystarczające fundusze. Aby powrócić do menu, naciśnij dowolny klawisz.");
            Console.ReadKey();
            return;
        }

        sender.Balance -= amount;
        receiver.Balance += amount;

        Console.WriteLine($"Przelew udany! {sender.FullName} przelano {amount:C} do {receiver.FullName}.");
        Console.WriteLine("Naciśnij dowolny klawisz aby wrócić do menu głównego.");
        Console.ReadKey();
    }
}
