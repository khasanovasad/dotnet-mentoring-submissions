using System.Collections.Generic;
using Task2.Models;

namespace Task2.Presentation;

public interface IPresenter
{
    void Present(Folder rootFolder, int indent = 0);
}