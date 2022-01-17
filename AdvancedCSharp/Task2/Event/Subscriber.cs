using System;
using Task2.Visitor;

namespace Task2.Event;

public class Subscriber
{
    private readonly string _id;

    public Subscriber(string id, IFileSystemTraverser publisher)
    {
        _id = id;

        publisher.RaiseStartEvent += HandleStartEvent;
        publisher.RaiseFinishEvent += HandleFinishEvent;
        publisher.RaiseFileFoundEvent += HandleFileFoundEvent;
        publisher.RaiseDirectoryFoundEvent += HandleDirectoryFoundEvent;
        publisher.RaiseFilteredFileFoundEvent += HandleFilteredFileFoundEvent;
        publisher.RaiseFilteredDirectoryFoundEvent += HandleFilteredDirectoryFoundEvent;
    }

    void HandleStartEvent(object sender, CustomEventArgs e)
    {
        Console.WriteLine($"SEARCH STARTED.");
        Console.ReadKey();
    }
    
    void HandleFinishEvent(object sender, CustomEventArgs e)
    {
        Console.WriteLine($"SEARCH FINISHED.");
        Console.ReadKey();
    }
    
    void HandleFileFoundEvent(object sender, CustomEventArgs e)
    {
        string answer;
        
        Console.WriteLine($"FILE FOUND WITH NAME: {e.FileName}");
        
        Console.Write("Shall include this file (Y/n)? ");
        answer = Console.ReadLine();
        e.ShallInclude = answer.Equals("Y") || String.IsNullOrEmpty(answer);
        
        Console.Write("Shall abort the program (y/N)? ");
        answer = Console.ReadLine();
        e.ShallAbort = answer.Equals("y") || answer.Equals("Y");
    }
    
    void HandleDirectoryFoundEvent(object sender, CustomEventArgs e)
    {
        string answer;
        
        Console.WriteLine($"FOLDER FOUND WITH NAME: {e.FolderName}");
        
        Console.Write("Shall include this FOLDER (Y/n)? ");
        answer = Console.ReadLine();
        e.ShallInclude = answer.Equals("Y") || String.IsNullOrEmpty(answer);
        
        Console.Write("Shall abort the program (y/N)? ");
        answer = Console.ReadLine();
        e.ShallAbort = answer.Equals("y") || answer.Equals("Y");
    }
    
    void HandleFilteredFileFoundEvent(object sender, CustomEventArgs e)
    {
        Console.WriteLine($"FILTERED FILE FOUND WITH NAME: {e.FileName}");
        Console.ReadKey();
    }
    
    void HandleFilteredDirectoryFoundEvent(object sender, CustomEventArgs e)
    {
        Console.WriteLine($"FILTERED FOLDER FOUND WITH NAME: {e.FolderName}");
        Console.ReadKey();
    }
}