using System;
using System.IO;
using Task2.Event;
using Task2.Filter;
using Task2.Presentation;
using Task2.Visitor;

namespace Task2;

public class Program
{
    public static void Main(string[] args)
    {
        const string predefinedFolderPath = @"..\..\..\..\PredefinedFolder";
        // const string predefinedFolderPath = @"C:\Users\Asadbek_Khasanov\Desktop";
        if (!Directory.Exists(predefinedFolderPath))
        {
            throw new Exception($"Folder at {Directory.GetCurrentDirectory()} {predefinedFolderPath} does not exist.");
        }

        FilterFunction filter = CustomFilter.CustomFilterFunction;
        var fileSystemTraverser = new FileSystemTraverser(filter);
        
        var _ = new Subscriber(Guid.NewGuid().ToString(), fileSystemTraverser);
        
        fileSystemTraverser.OnRaiseStartEvent(new CustomEventArgs());
        
        var result = fileSystemTraverser.Traverse(predefinedFolderPath);
        
        fileSystemTraverser.OnRaiseFinishEvent(new CustomEventArgs());
        
        IPresenter consolePresenter = ConsolePresenter.Instance;
        consolePresenter.Present(result);
    }
}