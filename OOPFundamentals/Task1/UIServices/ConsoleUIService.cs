using System;
using Task1.Model;
using Task1.StorageServices;

namespace Task1.UIServices;

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
        Console.Write($"\nEnter the ID/ISBN number of the {typeOfDocument.Name}: ");
        var id = Console.ReadLine();

        var document = _storageService.SearchDocument(id!, typeOfDocument);
        if (document is not null)
        {
            DisplayDocument(document);
            return;
        }

        ShowErrorMessage($"{typeOfDocument.Name} with ID/ISBN {id} does not exist.");
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
            Console.WriteLine("4. Quit.");
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
                    ExitProgram();
                    break;
            };
            Console.ReadKey();
        } while (true);
    }
}
