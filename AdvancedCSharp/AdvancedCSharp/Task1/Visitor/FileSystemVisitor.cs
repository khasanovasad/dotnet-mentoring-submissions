using System.IO;
using Task2.Models;

namespace Task2.Visitor;

public class FileSystemVisitor : IFileSystemVisitor
{
    private readonly FilterFunction _filterFunction;
        
    public FileSystemVisitor(FilterFunction filterFunction)
    {
        _filterFunction = filterFunction;
    }
    
    public Folder Traverse(string dirPath)
    {
        var subFiles = Directory.GetFiles(dirPath, "*");
        var subFolders = Directory.GetDirectories(dirPath, "*", SearchOption.TopDirectoryOnly);

        var rootFolder = _filterFunction(dirPath, subFolders, subFiles);
        for (int i = 0; i < rootFolder.Folders.Count; ++i)
        {
            rootFolder.Folders[i] = Traverse(rootFolder.Folders[i].Path);
        }
        
        return rootFolder;
    }
}