using System;
using System.Collections.Generic;

namespace Task2.Models;

public class Folder
{
    public string Name { get; set; }
    public string Path { get; set; }
    public List<Folder> Folders { get; set; }
    public List<File> Files { get; set; }
}