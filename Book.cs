using System;

public class Book
{
    public string Title { get; }
    public string Author { get; }
    public int PublicationYear { get; }
    public bool IsOnLoan { get; set; }


    public Book (string title, string author, int publicationYear)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("An item must have a title.");
        }

        Title = title;
        Author = author;
        PublicationYear = publicationYear;
    }

    public Book(string title, string author)
        : this(title, author, DateTime.Now.Year)
    {
    }

}
