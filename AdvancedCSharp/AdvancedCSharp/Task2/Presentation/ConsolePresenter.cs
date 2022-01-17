using System;
using Task2.Models;

namespace Task2.Presentation;

public class ConsolePresenter : IPresenter
{
    private static ConsolePresenter _instance = null;

    private ConsolePresenter()
    {
    }

    public static ConsolePresenter Instance
    {
        get
        {
            if (_instance is null)
            {
                _instance = new ConsolePresenter();
            }

            return _instance;
        }
    }

    public void Present(Folder rootFolder, int indent = 0)
    {
        Indent(indent);
        Console.WriteLine($"{rootFolder.Name}:");

        foreach (var file in rootFolder.Files)
        {
            Indent(indent + 2);
            Console.WriteLine($"{file.Name}");
        }

        foreach (var folder in rootFolder.Folders)
        {
            Present(folder, indent + 2);
        }
    }

    private void Indent(int indent)
    {
        Console.Write("".PadRight(indent));
    }
}