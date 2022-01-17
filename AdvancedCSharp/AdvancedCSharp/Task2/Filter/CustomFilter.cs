using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using Task2.Event;
using Task2.Models;
using Task2.Visitor;

namespace Task2.Filter;

public sealed class CustomFilter
{
    public static Folder CustomFilterFunction(string selfPath, string[] folders, string[] files, IFileSystemTraverser traverser)
    {
        var resultingFolder = new Folder();
        resultingFolder.Name = selfPath.Split("\\").LastOrDefault();
        resultingFolder.Path = selfPath;
        resultingFolder.Files = new List<File>();
        resultingFolder.Folders = new List<Folder>();
        
        foreach (var file in files)
        {
            var splits = file.Split('\\');

            var tmpFile = new Models.File
            {
                Name = splits.LastOrDefault(),
                Path = file,
            };

            var eventArgs = new CustomEventArgs(fileName: tmpFile.Name);
            traverser.OnRaiseFileFoundEvent(eventArgs);
            
            if (eventArgs.ShallInclude)
            {
                resultingFolder.Files.Add(tmpFile);
            }

            if (eventArgs.ShallAbort)
            {
                System.Environment.Exit(1);
            }
        }
        
        foreach (var folder in folders)
        {
            var splits = folder.Split('\\');

            var tmpFolder = new Folder
            {
                Name = splits.LastOrDefault(),
                Path = folder,
            };
            
            var eventArgs = new CustomEventArgs(folderName: tmpFolder.Name);
            traverser.OnRaiseDirectoryFoundEvent(eventArgs);
            
            if (eventArgs.ShallInclude)
            {
                resultingFolder.Folders.Add(tmpFolder);
            }

            if (eventArgs.ShallAbort)
            {
                System.Environment.Exit(1);
            }
        }

        return resultingFolder;
    }
}