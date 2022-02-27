using System;
using System.Collections.Generic;
using Task2.Model;

namespace Task2.StorageServices.FileStorage.Poviders;

public class FileStoragePathProvider
{
    private static FileStoragePathProvider? _instance;
    public static FileStoragePathProvider Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FileStoragePathProvider();
            }
            return _instance;
        }
    }

    public readonly Dictionary<Type, string> Foo = new Dictionary<Type, string>
    {
        { typeof(Book), "../../../FileStorage/Books" },
        { typeof(LocalizedBook), "../../../FileStorage/LocalizedBooks" },
        { typeof(Patent), "../../../FileStorage/Patents" },
        { typeof(Magazine), "../../../FileStorage/Magazines" },
    };

    private FileStoragePathProvider() { }
}
