using System;
using Task2.Event;
using Task2.Models;

namespace Task2.Visitor;

public interface IFileSystemTraverser
{
    public event EventHandler<CustomEventArgs> RaiseStartEvent;
    public event EventHandler<CustomEventArgs> RaiseFinishEvent;
    public event EventHandler<CustomEventArgs> RaiseFileFoundEvent;
    public event EventHandler<CustomEventArgs> RaiseDirectoryFoundEvent;
    public event EventHandler<CustomEventArgs> RaiseFilteredFileFoundEvent;
    public event EventHandler<CustomEventArgs> RaiseFilteredDirectoryFoundEvent;
    
    void OnRaiseStartEvent(CustomEventArgs e); 
    void OnRaiseFinishEvent(CustomEventArgs e); 
    void OnRaiseFileFoundEvent(CustomEventArgs e); 
    void OnRaiseDirectoryFoundEvent(CustomEventArgs e); 
    void OnRaiseFilteredFileFoundEvent(CustomEventArgs e); 
    void OnRaiseFilteredDirectoryFoundEvent(CustomEventArgs e);
    
    Folder Traverse(string dirPath);
}