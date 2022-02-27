using System;

namespace Task1.Model;

public class LocalizedBook : Document
{
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int NumberOfPages { get; set; }
    public string OriginalPublisher { get; set; }
    public string CountryOfLocalization { get; set; }
    public string LocalPublisher { get; set; }
    public DateTime DatePublished { get; set; }

    public LocalizedBook() : this (String.Empty, String.Empty, String.Empty, -1, String.Empty, String.Empty, String.Empty, DateTime.Now) { }

    public LocalizedBook(string isbn, string title, string author, int numberOfPages, string originalPublisher, string countryOfLocalization, string localPublisher, DateTime datePublished)
    {
        Isbn = isbn;
        Title = title;
        Author = author;
        NumberOfPages = numberOfPages;
        OriginalPublisher = originalPublisher;
        CountryOfLocalization = countryOfLocalization;
        LocalPublisher = localPublisher;
        DatePublished = datePublished;
    }
}
