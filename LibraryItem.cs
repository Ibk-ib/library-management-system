public abstract class LibraryItem
{
    private static int _nextId = 1;

    public int Id { get; }
    public string Title { get; }
    public int PublicationYear { get; }
    public bool IsOnLoan { get; private set; }

    protected LibraryItem(string title, int publicationYear)
    {
        Id = _nextId++;
        Title = title;
        PublicationYear = publicationYear;
        IsOnLoan = false;
    }

    public abstract int LoanPeriodDays { get; }

    public abstract string ItemType { get; }

    public virtual decimal DailyFine => 0.50m;

    public decimal CalculateFine(int daysLate)
    {
        if (daysLate <= 0)
        {
            return 0m;
        }

        return daysLate * DailyFine;
    }

    public virtual string Describe()
    {
        return $"[{Id}] {ItemType}: \"{Title}\" ({PublicationYear})";
    }

    public void MarkAsBorrowed()
    {
        if (IsOnLoan)
        {
            throw new InvalidOperationException($"\"{Title}\" is already on loan.");
        }

        IsOnLoan = true;
    }

    public void MarkAsReturned()
    {
        if (!IsOnLoan)
        {
            throw new InvalidOperationException($"\"{Title}\" is not currently on loan.");
        }

        IsOnLoan = false;
    }
}