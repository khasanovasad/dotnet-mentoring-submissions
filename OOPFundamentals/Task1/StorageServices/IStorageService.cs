using System;
using Task1.Model;

namespace Task1.StorageServices;

public interface IStorageService
{
    Document? SearchDocument(string id, Type typeOfDocument);
}
