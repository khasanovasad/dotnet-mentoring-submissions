using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task2.Models;
using Task2.Presentation;
using Task2.Visitor;
using File = Task2.Models.File;

namespace Task2;

public delegate Folder FilterFunction(string selfPath, string[] folders, string[] files);

public class Program
{
    public static void Main(string[] args)
    {
        // const string predefinedFolderPath = @"..\..\..\..\PredefinedFolder";
        const string predefinedFolderPath = @"C:\Users\Asadbek_Khasanov\Desktop";
        if (!Directory.Exists(predefinedFolderPath))
        {
            throw new Exception($"Folder at {Directory.GetCurrentDirectory()} {predefinedFolderPath} does not exist.");
        }

        FilterFunction filter = CustomFilterFunction;
        var fileSystemVisitor = new FileSystemVisitor(filter);
        var result = fileSystemVisitor.Traverse(predefinedFolderPath);
        
        IPresenter consolePresenter = ConsolePresenter.Instance;
        consolePresenter.Present(result);
    }

    private static Folder CustomFilterFunction(string selfPath, string[] folders, string[] files)
    {
        var resultingFolder = new Folder();
        resultingFolder.Name = selfPath.Split("\\").LastOrDefault();
        resultingFolder.Path = selfPath;
        resultingFolder.Files = new List<Models.File>();
        resultingFolder.Folders = new List<Folder>();
        
        foreach (var file in files)
        {
            var splits = file.Split('\\');

            var tmpFile = new Models.File
            {
                Name = splits.LastOrDefault(),
                Path = file,
            };
            resultingFolder.Files.Add(tmpFile);
        }
        
        foreach (var folder in folders)
        {
            var splits = folder.Split('\\');

            var tmpFolder = new Folder
            {
                Name = splits.LastOrDefault(),
                Path = folder,
            };
            resultingFolder.Folders.Add(tmpFolder);
        }

        return resultingFolder;
    }
}