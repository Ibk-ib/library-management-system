public class Library
{
    private readonly List<LibraryItem> _items = new();
    private readonly List<Member> _members = new();
    private readonly List<Loan> _loans = new();

    public string Name { get; }

    public Library(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("A library needs a name.", nameof(name));

        Name = name;
    }

    public IReadOnlyList<LibraryItem> Items => _items;
    public IReadOnlyList<Member> Members => _members;
    public IReadOnlyList<Loan> Loans => _loans;


    public void AddItem(LibraryItem item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        _items.Add(item);
    }

    public void RegisterMember(Member member)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member));

        if (_members.Any(m => m.MembershipId == member.MembershipId))
            throw new InvalidOperationException(
                $"Membership id {member.MembershipId} is already taken."
            );

        _members.Add(member);
    }

    public Loan Borrow(int itemId, string membershipId, DateOnly today)
    {
        LibraryItem item = FindItem(itemId);
        Member member = FindMember(membershipId);

        if (!member.CanBorrow)
            throw new InvalidOperationException(
                $"{member.Name} already has the maximum of {Member.MaxActiveLoans} items on loan.");

        if (item is IReservable { IsReserved: true } reserved &&
            reserved.ReservedFor != member.Name)
            throw new InvalidOperationException(
                $"\"{item.Title}\" is reserved for {reserved.ReservedFor}.");

        item.MarkAsBorrowed();

        if (item is IReservable r)
            r.CancelReservation();

        var loan = new Loan(item, member, today);
        _loans.Add(loan);
        member.Attach(loan);

        return loan;
    }

    public decimal Return(int itemId, DateOnly today)
    {
        Loan loan = _loans.FirstOrDefault(l => l.Item.Id == itemId && !l.IsReturned)
            ?? throw new InvalidOperationException($"There is no open loan for item {itemId}.");

        loan.Complete(today);
        loan.Item.MarkAsReturned();
        return loan.Fine;
    }

    public IEnumerable<LibraryItem> Search(string term) =>
    _items.Where(i => i.Describe().Contains(term, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<LibraryItem> AvailableItems() =>
    _items.Where(i => !i.IsOnLoan);

    public IEnumerable<Loan> OverdueLoans(DateOnly today) =>
    _loans.Where(l => !l.IsReturned && l.DueOn < today);

    private LibraryItem FindItem(int id) =>
    _items.FirstOrDefault(i => i.Id == id)
    ?? throw new KeyNotFoundException($"No item with id {id}.");

    private Member FindMember(string membershipId) =>
    _members.FirstOrDefault(m => m.MembershipId == membershipId)
    ?? throw new KeyNotFoundException($"No member with id {membershipId}.");
}