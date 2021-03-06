using System;
using System.IO;
using System.Text.Json;
using Task1.Model;
using Task1.StorageServices.FileStorage.Poviders;

namespace Task1.StorageServices.FileStorage;

public class FileStorageService : IStorageService
{
    public Document? SearchDocument(string id, Type typeOfDocument)
    {
        var dataStorePath = FileStoragePathProvider.Instance.Foo[typeOfDocument];
        var fullPath = $"{dataStorePath}/{typeOfDocument.Name.ToLower()}_[{id}].json";

        if (File.Exists(fullPath))
        {
            string jsonString = File.ReadAllText(fullPath);
            var document = JsonSerializer.Deserialize(jsonString, typeOfDocument) as Document;
            return document!;
        }

        return null;
    }
}
