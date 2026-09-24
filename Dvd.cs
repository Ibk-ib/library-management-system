public class Dvd : LibraryItem, IReservable
{
    public int RuntimeMinutes { get; }
    public string AgeRating { get; }

    public Dvd(
        string title,
        int runtimeMinutes,
        string ageRating,
        int publicationYear
    ) : base(title, publicationYear, new StandardFinePolicy(1.00m))
    {
        RuntimeMinutes = runtimeMinutes;
        AgeRating = ageRating;
    }

    public override int LoanPeriodDays => 7;

    public override string ItemType => "DVD";

    public override string Describe()
    {
        return $"{base.Describe()} — {RuntimeMinutes} min, rated {AgeRating}";
    }

    public string? ReservedFor { get; private set; }

    public bool IsReserved => ReservedFor is not null;

    public void Reserve(Member member)
    {
        if (IsReserved)
        {
            throw new InvalidOperationException(
                $"\"{Title}\" is already reserved for {ReservedFor}."
            );
        }

        ReservedFor = member.Name;
    }

    public void CancelReservation()
    {
        ReservedFor = null;
    }
}