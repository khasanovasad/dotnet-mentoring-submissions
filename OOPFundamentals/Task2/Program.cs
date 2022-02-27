using System;
using Task2.StorageServices;
using Task2.StorageServices.FileStorage;
using Task2.UIServices;

namespace Task2;

public class Program
{
    public static void Main(string[] args)
    {
        ApplicationStart();
    }

    public static void ApplicationStart()
    {
        IStorageService storageService = new FileStorageService();
        IUIService uiService = new ConsoleUIService(storageService);
        uiService.StartView();
    }
}