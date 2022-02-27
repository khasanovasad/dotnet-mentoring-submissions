using System;
using Task2.Model;

namespace Task2.UIServices;

public interface IUIService
{
    void StartView();
    void ExitProgram();
    void SearchAndDisplayDocument(Type typeOfDocument);
    void DisplayDocument(Document document);
    void ShowErrorMessage(string message);
}
