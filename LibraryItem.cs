public abstract class LibraryItem
{
    private static int _nextId = 1;
    private readonly IFinePolicy _finePolicy;

    public int Id { get; }
    public string Title { get; }
    public int PublicationYear { get; }
    public bool IsOnLoan { get; private set; }

    protected LibraryItem(string title, int publicationYear, IFinePolicy finePolicy)
    {

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("An item must have a title.", nameof(title));

        if (publicationYear < 1450 || publicationYear > DateTime.Now.Year + 1)
            throw new ArgumentOutOfRangeException(
                nameof(publicationYear),
                "Publication year is outside the plausible range."
            );
        Id = _nextId++;
        Title = title;
        PublicationYear = publicationYear;
        IsOnLoan = false;
        _finePolicy = finePolicy;
    }

    public abstract int LoanPeriodDays { get; }

    public abstract string ItemType { get; }

    public virtual decimal DailyFine => 0.50m;

    public decimal CalculateFine(int daysLate)
    {
        return _finePolicy.Calculate(daysLate);
    }

    public virtual string Describe()
    {
        return $"[{Id}] {ItemType}: \"{Title}\" ({PublicationYear})";
    }

    public virtual void MarkAsBorrowed()
    {
        if (IsOnLoan)
        {
            throw new InvalidOperationException($"\"{Title}\" is already on loan.");
        }

        IsOnLoan = true;
    }

    public virtual void MarkAsReturned()
    {
        if (!IsOnLoan)
        {
            throw new InvalidOperationException($"\"{Title}\" is not currently on loan.");
        }

        IsOnLoan = false;
    }
}