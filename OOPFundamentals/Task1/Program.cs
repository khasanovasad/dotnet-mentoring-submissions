using System;
using Task1.StorageServices;
using Task1.StorageServices.FileStorage;
using Task1.UIServices;

namespace Task1;

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