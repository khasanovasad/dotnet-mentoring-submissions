using Task2.Models;

namespace Task2.Visitor;

public interface IFileSystemVisitor
{
    Folder Traverse(string dirPath);
}