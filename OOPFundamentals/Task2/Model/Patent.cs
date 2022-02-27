using System;
using System.Linq;

namespace Task2.Model;

public class Patent : Document
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string[] Authors { get; set; }
    public DateTime DatePublished { get; set; }
    public DateTime ExpirationDate { get; set; }

    public Patent() : this(String.Empty, String.Empty, Array.Empty<string>(), DateTime.Now, DateTime.Now) { }

    public Patent(string id, string title, string[] authors, DateTime datePublished, DateTime expirationDate)
    {
        Id = id;
        Title = title;
        Authors = authors;
        DatePublished = datePublished;
        ExpirationDate = expirationDate;
    }
}
