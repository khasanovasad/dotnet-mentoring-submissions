using System;
using Task2.Model;

namespace Task2.StorageServices;

public interface IStorageService
{
    Document? SearchDocument(string id, Type typeOfDocument);
}
