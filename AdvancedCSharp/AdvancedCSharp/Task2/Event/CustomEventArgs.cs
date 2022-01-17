using System;

namespace Task2.Event;

public class CustomEventArgs : EventArgs
{
    public string FileName { get; set; }
    public string FolderName { get; set; }
    public bool ShallInclude { get; set; } = true;
    public bool ShallAbort { get; set; } = false;
    
    public CustomEventArgs(string fileName = null, string folderName = null)
    {
        FileName = fileName;
        FolderName = folderName;
    }
}