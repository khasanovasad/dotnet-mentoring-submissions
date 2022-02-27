using System;

namespace Task2.Model;

public class Magazine : Document
{
    public string ReleaseNumber { get; set; }
    public string Title { get; set; }
    public string Publisher { get; set; }
    public DateTime PublishDate { get; set; }

    public Magazine() : this(String.Empty, String.Empty, String.Empty, DateTime.Now) { }

    public Magazine(string releaseNumber, string title, string publisher, DateTime publishDate)
    {
        ReleaseNumber = releaseNumber;
        Title = title;
        Publisher = publisher;
        PublishDate = publishDate;
    }
}
