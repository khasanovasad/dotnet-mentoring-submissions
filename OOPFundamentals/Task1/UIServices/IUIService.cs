using System;
using Task1.Model;

namespace Task1.UIServices;

public interface IUIService
{
    void StartView();
    void ExitProgram();
    void SearchAndDisplayDocument(Type typeOfDocument);
    void DisplayDocument(Document document);
    void ShowErrorMessage(string message);
}
