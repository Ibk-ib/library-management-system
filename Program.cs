var today = new DateOnly(2026, 3, 2);

var library = new Library("Riverside Community Library");

// =====================================================
// ITEMS
// =====================================================

var book1 = new Book(
    "The Hobbit",
    "J.R.R. Tolkien",
    "978-0261102217",
    1937
);

var book2 = new Book(
    "Clean Code",
    "Robert C. Martin",
    "978-0132350884",
    2008
);

var book3 = new Book(
    "The Great Gatsby",
    "F. Scott Fitzgerald",
    "978-0743273565",
    1925
);

var dvd = new Dvd(
    "Inception",
    148,
    "PG-13",
    2010
);

var magazine = new Magazine(
    "New Scientist",
    3521,
    2026
);

library.AddItem(book1);
library.AddItem(book2);
library.AddItem(book3);
library.AddItem(dvd);
library.AddItem(magazine);


// =====================================================
// MEMBERS
// =====================================================

var qanitat = new PremiumMember(
    "M-001",
    "Qanitat"
);

var ibk = new StandardMember(
    "M-002",
    "Ibk"
);

var emima = new StaffMember(
    "M-003",
    "Emima"
);

library.RegisterMember(qanitat);
library.RegisterMember(ibk);
library.RegisterMember(emima);


// =====================================================
// CATALOGUE
// =====================================================

Console.WriteLine($"=== {library.Name} ===");

Console.WriteLine("\n-- Catalogue --");

foreach (LibraryItem item in library.Items)
{
    Console.WriteLine(
        $"{item.Describe()} | " +
        $"loan {item.LoanPeriodDays} days"
    );
}


// =====================================================
// BORROWING
// =====================================================

Console.WriteLine("\n-- Borrowing --");

var loan1 = library.Borrow(
    book1.Id,
    qanitat.MembershipId,
    today
);

Console.WriteLine(
    $"{qanitat.Name} borrowed \"{loan1.Item.Title}\" — due {loan1.DueOn}"
);


var loan2 = library.Borrow(
    dvd.Id,
    ibk.MembershipId,
    today
);

Console.WriteLine(
    $"{ibk.Name} borrowed \"{loan2.Item.Title}\" — due {loan2.DueOn}"
);


var loan3 = library.Borrow(
    book3.Id,
    emima.MembershipId,
    today
);

Console.WriteLine(
    $"{emima.Name} borrowed \"{loan3.Item.Title}\" — due {loan3.DueOn}"
);


// =====================================================
// RESERVATIONS
// =====================================================

Console.WriteLine("\n-- Reservations --");

library.Reserve(
    book2.Id,
    qanitat
);

Console.WriteLine(
    $"{qanitat.Name} reserved \"{book2.Title}\""
);


library.Reserve(
    book2.Id,
    ibk
);

Console.WriteLine(
    $"{ibk.Name} joined the reservation queue for \"{book2.Title}\""
);


// =====================================================
// RESERVATION RULE
// =====================================================

Console.WriteLine("\n-- Reservation Rule --");

try
{
    library.Borrow(
        book2.Id,
        emima.MembershipId,
        today
    );

    Console.WriteLine(
        $"{emima.Name} was allowed to borrow \"{book2.Title}\"."
    );
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(
        $"Emima's borrowing was blocked — {ex.Message}"
    );
}


// =====================================================
// RETURNING
// =====================================================

Console.WriteLine("\n-- Returning --");

var fineBook = library.Return(
    book1.Id,
    qanitat.MembershipId,
    today.AddDays(25)
);

Console.WriteLine(
    $"\"{book1.Title}\" returned 4 days late -> fine {fineBook:C}"
);


var fineDvd = library.Return(
    dvd.Id,
    ibk.MembershipId,
    today.AddDays(11)
);

Console.WriteLine(
    $"\"{dvd.Title}\" returned 4 days late -> fine {fineDvd:C}"
);


var fineBook3 = library.Return(
    book3.Id,
    emima.MembershipId,
    today.AddDays(25)
);

Console.WriteLine(
    $"\"{book3.Title}\" returned 4 days late -> fine {fineBook3:C}"
);


// =====================================================
// MEMBERS
// =====================================================

Console.WriteLine("\n-- Members --");

Console.WriteLine(
    $"{qanitat.Name}: " +
    $"{qanitat.ActiveLoanCount} active loans, " +
    $"{qanitat.TotalFinesOwed:C} fines owed"
);

Console.WriteLine(
    $"{ibk.Name}: " +
    $"{ibk.ActiveLoanCount} active loans, " +
    $"{ibk.TotalFinesOwed:C} fines owed"
);

Console.WriteLine(
    $"{emima.Name}: " +
    $"{emima.ActiveLoanCount} active loans, " +
    $"{emima.TotalFinesOwed:C} fines owed"
);


// =====================================================
// SEARCH
// =====================================================

Console.WriteLine("\n-- Search --");

foreach (LibraryItem item in library.Search("clean"))
{
    Console.WriteLine(
        $"Found: {item.Describe()}"
    );
}


// =====================================================
// DAILY SUMMARY
// =====================================================

Console.WriteLine();

library.PrintDailySummary(today.AddDays(25));