public class Library
{
    private readonly List<LibraryItem> _items = new();
    private readonly List<Member> _members = new();
    private readonly List<Loan> _loans = new();
    private readonly Dictionary<int, ReservationQueue> _reservationQueues = new();

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






    private ReservationQueue GetReservationQueue(int itemId)
    {
        if (!_reservationQueues.TryGetValue(itemId, out var queue))
        {
            queue = new ReservationQueue();
            _reservationQueues[itemId] = queue;
        }

        return queue;
    }

    private Member? GetFirstReservation(int itemId)
    {
        if (!_reservationQueues.TryGetValue(itemId, out var queue))
        {
            return null;
        }

        return queue.Peek();
    }

    private Member? GetNextReservation(int itemId)
    {
        if (!_reservationQueues.TryGetValue(itemId, out var queue))
        {
            return null;
        }

        return queue.Dequeue();
    }

    public void Reserve(int itemId, Member member)
    {
        LibraryItem item = FindItem(itemId);

        if (item is not IReservable)
        {
            throw new InvalidOperationException(
                $"\"{item.Title}\" cannot be reserved."
            );
        }

        ReservationQueue queue = GetReservationQueue(itemId);

        queue.Enqueue(member);
    }

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
        {
            throw new InvalidOperationException(
                $"{member.Name} already has the maximum of {member.MaxActiveLoans} items on loan."
            );
        }

        if (item is IReservable)
        {
            Member? firstMember = GetFirstReservation(itemId);

            if (firstMember is not null &&
                firstMember.MembershipId != membershipId)
            {
                throw new InvalidOperationException(
                    $"\"{item.Title}\" is reserved for {firstMember.Name}."
                );
            }
        }

        if (item is not IDigital)
        {
            item.MarkAsBorrowed();
        }

        if (item is IReservable)
        {
            Member? firstMember = GetFirstReservation(itemId);

            if (firstMember is not null &&
                firstMember.MembershipId == membershipId)
            {
                GetNextReservation(itemId);
            }
        }

        var loan = new Loan(item, member, today);

        _loans.Add(loan);
        member.Attach(loan);

        return loan;
    }

    public decimal Return(
     int itemId,
     string membershipId,
     DateOnly today)
    {
        Loan loan = _loans.FirstOrDefault(
            l =>
                l.Item.Id == itemId &&
                l.Borrower.MembershipId == membershipId &&
                !l.IsReturned
        ) ?? throw new InvalidOperationException(
            $"There is no open loan for item {itemId} for member {membershipId}."
        );

        loan.Complete(today);

        if (loan.Item is not IDigital)
        {
            loan.Item.MarkAsReturned();
        }

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


    public void PrintDailySummary(DateOnly today)
    {
        Console.WriteLine($"=== Daily Summary for {today} ===");

        Console.WriteLine("\n-- Items Out --");

        foreach (Loan loan in _loans.Where(l => !l.IsReturned))
        {
            Console.WriteLine(
                $"{loan.Item.Title} — {loan.Borrower.Name}"
            );
        }

        Console.WriteLine("\n-- Overdue --");

        foreach (Loan loan in OverdueLoans(today))
        {
            Console.WriteLine(
                $"{loan.Item.Title} — " +
                $"{loan.Borrower.Name} — " +
                $"{loan.DaysLate} days late — " +
                $"{loan.Fine:C}"
            );
        }

        Console.WriteLine("\n-- Members at Limit --");

        foreach (Member member in _members.Where(m => !m.CanBorrow))
        {
            Console.WriteLine(
                $"{member.Name} — {member.ActiveLoanCount} active loans"
            );
        }

        Console.WriteLine("\n-- Outstanding Fines --");

        decimal totalFines = _members.Sum(m => m.TotalFinesOwed);

        Console.WriteLine($"Total outstanding fines: {totalFines:C}");
    }
}


