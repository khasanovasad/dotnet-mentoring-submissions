using System;
using System.IO;
using Task2.Event;
using Task2.Models;

namespace Task2.Visitor;

public delegate Folder FilterFunction(string selfPath, string[] folders, string[] files, IFileSystemTraverser traverser);

public class FileSystemTraverser : IFileSystemTraverser
{
    public event EventHandler<CustomEventArgs> RaiseStartEvent;
    public event EventHandler<CustomEventArgs> RaiseFinishEvent;
    public event EventHandler<CustomEventArgs> RaiseFileFoundEvent;
    public event EventHandler<CustomEventArgs> RaiseDirectoryFoundEvent;
    public event EventHandler<CustomEventArgs> RaiseFilteredFileFoundEvent;
    public event EventHandler<CustomEventArgs> RaiseFilteredDirectoryFoundEvent;

    public void OnRaiseStartEvent(CustomEventArgs e)
    {
        EventHandler<CustomEventArgs> raiseEvent = RaiseStartEvent;
        raiseEvent?.Invoke(this, e);
    }
    
    public void OnRaiseFinishEvent(CustomEventArgs e)
    {
        EventHandler<CustomEventArgs> raiseEvent = RaiseFinishEvent;
        raiseEvent?.Invoke(this, e);
    }
    
    public void OnRaiseFileFoundEvent(CustomEventArgs e)
    {
        EventHandler<CustomEventArgs> raiseEvent = RaiseFileFoundEvent;
        raiseEvent?.Invoke(this, e);
    }
    
    public void OnRaiseDirectoryFoundEvent(CustomEventArgs e)
    {
        EventHandler<CustomEventArgs> raiseEvent = RaiseDirectoryFoundEvent;
        raiseEvent?.Invoke(this, e);
    }
    
    public void OnRaiseFilteredFileFoundEvent(CustomEventArgs e)
    {
        EventHandler<CustomEventArgs> raiseEvent = RaiseFilteredFileFoundEvent;
        raiseEvent?.Invoke(this, e);
    }
    
    public void OnRaiseFilteredDirectoryFoundEvent(CustomEventArgs e)
    {
        EventHandler<CustomEventArgs> raiseEvent = RaiseFilteredDirectoryFoundEvent;
        raiseEvent?.Invoke(this, e);
    }
    
    private readonly FilterFunction _filterFunction;
        
    public FileSystemTraverser(FilterFunction filterFunction)
    {
        _filterFunction = filterFunction;
    }

    public Folder Traverse(string dirPath)
    {
        var subFiles = Directory.GetFiles(dirPath, "*");
        var subFolders = Directory.GetDirectories(dirPath, "*", SearchOption.TopDirectoryOnly);

        var rootFolder = _filterFunction(dirPath, subFolders, subFiles, this);
        for (int i = 0; i < rootFolder.Folders.Count; ++i)
        {
            rootFolder.Folders[i] = Traverse(rootFolder.Folders[i].Path);
        }
        
        return rootFolder;
    }
}