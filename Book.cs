public class Book : LibraryItem, IReservable
{
    public string Author { get; }
    public string Isbn { get; }

    public Book(
        string title,
        string author,
        string isbn,
        int publicationYear
    ) : base(title, publicationYear, new StandardFinePolicy(0.50m))
    {
        Author = author;
        Isbn = isbn;
    }

    public override int LoanPeriodDays => 21;

    public override string ItemType => "Book";

    public override string Describe()
    {
        return $"{base.Describe()} by {Author} — ISBN {Isbn}";
    }
}