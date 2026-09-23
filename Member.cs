public class Member
{
    public const int MaxActiveLoans = 3;

    private readonly List<Loan> _loans = new();

    public string MembershipId { get; }
    public string Name { get; }

    public IReadOnlyList<Loan> Loans => _loans;

    public Member(string membershipId, string name)
    {
        if (string.IsNullOrWhiteSpace(membershipId))
        {
            throw new ArgumentException(
                "Membership id is required.",
                nameof(membershipId)
            );
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Name is required.",
                nameof(name)
            );
        }

        MembershipId = membershipId;
        Name = name.Trim();
    }

    public int ActiveLoanCount => _loans.Count(l => !l.IsReturned);

    public bool CanBorrow => ActiveLoanCount < MaxActiveLoans;

    public decimal TotalFinesOwed => _loans.Sum(l => l.Fine);

    internal void Attach(Loan loan) => _loans.Add(loan);
}