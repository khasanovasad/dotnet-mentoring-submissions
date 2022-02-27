using System;

namespace Task1.Model;

public class Book : Document
{
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int NumberOfPages { get; set; }
    public string Publisher { get; set; }
    public DateTime DatePublished { get; set; }

    public Book() : this(String.Empty, String.Empty, String.Empty, 0, String.Empty, DateTime.Now) { }

    public Book(string isbn, string title, string author, int numberOfPages, string publisher, DateTime datePublished)
    {
        Isbn = isbn;
        Title = title;
        Author = author;
        NumberOfPages = numberOfPages;
        Publisher = publisher;
        DatePublished = datePublished;
    }
}
