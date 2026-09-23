public class Magazine : LibraryItem
{
    public int IssueNumber { get; }

    public Magazine(
        string title,
        int issueNumber,
        int publicationYear
    ) : base(title, publicationYear)
    {
        IssueNumber = issueNumber;
    }

    public override int LoanPeriodDays => 3;

    public override string ItemType => "Magazine";

    public override decimal DailyFine => 0.25m;

    public override string Describe()
    {
        return $"{base.Describe()} — issue #{IssueNumber}";
    }
}