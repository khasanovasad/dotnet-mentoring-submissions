using System;
using Task2.Model;
using Task2.StorageServices;

namespace Task2.UIServices;

public class ConsoleUIService : IUIService
{
    private readonly IStorageService _storageService;

    public ConsoleUIService(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public void DisplayDocument(Document document)
    {
        Console.WriteLine(document);
    }

    public void ExitProgram()
    {
        Environment.Exit(0);
    }

    public void SearchAndDisplayDocument(Type typeOfDocument)
    {
        Console.Write($"\nEnter the UID (ID, ISBN, Release Number) number of the {typeOfDocument.Name}: ");
        var id = Console.ReadLine();

        var document = _storageService.SearchDocument(id!, typeOfDocument);
        if (document is not null)
        {
            DisplayDocument(document);
            return;
        }

        ShowErrorMessage($"{typeOfDocument.Name} with UID {id} does not exist.");
    }

    public void ShowErrorMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void StartView()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Choose one below:");
            Console.WriteLine("1. Search books.");
            Console.WriteLine("2. Search localized books.");
            Console.WriteLine("3. Search patents.");
            Console.WriteLine("4. Search magazines.");
            Console.WriteLine("5. Quit.");
            Console.Write("? ");

            var response = Console.ReadLine();
            switch (response)
            {
                case "1":
                    SearchAndDisplayDocument(typeof(Book));
                    break;
                case "2":
                    SearchAndDisplayDocument(typeof(LocalizedBook));
                    break;
                case "3":
                    SearchAndDisplayDocument(typeof(Patent));
                    break;
                case "4":
                    SearchAndDisplayDocument(typeof(Magazine));
                    break;
                case "5":
                    ExitProgram();
                    break;
            };
            Console.ReadKey();
        } while (true);
    }
}
