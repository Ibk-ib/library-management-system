public class Loan
{
    public LibraryItem Item { get; }
    public Member Borrower { get; }
    public DateOnly BorrowedOn { get; }
    public DateOnly DueOn { get; }
    public DateOnly? ReturnedOn { get; private set; }

    public Loan(LibraryItem item, Member borrower, DateOnly borrowedOn)
    {
        Item = item;
        Borrower = borrower;
        BorrowedOn = borrowedOn;

        DueOn = borrowedOn.AddDays(item.LoanPeriodDays);
    }

    public bool IsReturned => ReturnedOn.HasValue;

    public int DaysLate => ReturnedOn is null
    ? 0
    : Math.Max(0, ReturnedOn.Value.DayNumber - DueOn.DayNumber);

    public decimal Fine => Item.CalculateFine(DaysLate);

    public void Complete(DateOnly returnedOn)
    {
        if (IsReturned)
        {
            throw new InvalidOperationException("This loan has already been closed.");
        }

        if (returnedOn < BorrowedOn)
        {
            throw new ArgumentException(
                "An item cannot be returned before it was borrowed.",
                nameof(returnedOn)
            );
        }

        ReturnedOn = returnedOn;
    }
}