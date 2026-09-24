Book book1 = new Book("The Great Gatsby", "F. Scott Fitzgerald", "978-0-7432-7356-5", 1925);
//book1.IsOnLoan = false;

Book book2 = new Book("To Kill a Mockingbird", "Harper Lee", "978-0-06-112008-4", 1960);
//book2.IsOnLoan = false;

Book book3 = new Book("1984", "George Orwell", "978-0-452-28423-1", 1949);
//book3.IsOnLoan = false;


List<Book> books = new List<Book>();
    books.Add(book1);
    books.Add(book2);
    books.Add(book3);

foreach (Book book in books) {

    Console.WriteLine("Borrowing {0}...", book.Title);

    book.MarkAsBorrowed();

    Console.WriteLine("done.");

    Console.WriteLine("Borrowing {0} again...", book.Title);

    try
    {
        book.MarkAsBorrowed();
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine("blocked — {0}", ex.Message);
    }
}

Dvd dvd = new Dvd("Inception", 2010, "Christopher Nolan", 148);
Magazine magazine = new Magazine("National Geographic", 202, 2021);


Console.WriteLine(book1.Describe());
Console.WriteLine(book2.Describe());
Console.WriteLine(book3.Describe());
Console.WriteLine(dvd.Describe());
Console.WriteLine(magazine.Describe());

//List<LibraryItem> items = new List<LibraryItem>();

//items.Add(book1);
//items.Add(book2);
//items.Add(book3);
//items.Add(dvd);
//items.Add(magazine);

//foreach (LibraryItem item in items)
//{
//    Console.WriteLine($"{item.Describe()} | loan for {item.LoanPeriodDays} days and Fine is: {item.DailyFine:C}");
//}

//Console.WriteLine(
//    $"\"{book2.Title}\" is now reserved for {book2.ReservedFor}"
//);

var today = new DateOnly(2026, 5, 7);
var library = new Library("Riverside Community Library");
var qanitat = new Member("M001", "Emiola Qanitat");
var ibraheem = new Member("M002", "Abdullah Ibraheem");


library.AddItem(book1);
library.AddItem(book2);
library.AddItem(book3);
library.AddItem(dvd);
library.AddItem(magazine);

library.RegisterMember(qanitat);
library.RegisterMember(ibraheem);

Console.WriteLine();
Console.WriteLine($"=== {library.Name} ===");
Console.WriteLine();
Console.WriteLine("-- Catalogue --");

foreach (LibraryItem item in library.Items)
{
    Console.WriteLine(
        $"{item.Describe()} | loan for {item.LoanPeriodDays} days, and Fine is {item.DailyFine:C} per day late"
    );
}

var loan1 = library.Borrow(book1.Id, qanitat.MembershipId, today);

Console.WriteLine(
    $"{qanitat.Name} borrowed \"{loan1.Item.Title}\" — due {loan1.DueOn}"
);

var loan2 = library.Borrow( dvd.Id, qanitat.MembershipId, today);
Console.WriteLine(
    $"{qanitat.Name} borrowed \"{loan2.Item.Title}\" — due {loan2.DueOn}"
);

try
{
    library.Borrow(book1.Id, ibraheem.MembershipId, today);
    Console.WriteLine("Qanitat was allowed to borrow the book.");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Ibraheem's borrowing was blocked — {ex.Message}");
}

book2.Reserve(ibraheem);
Console.WriteLine(
    $"\"{book2.Title}\" is now reserved for {book2.ReservedFor}"
);

try
{
    library.Borrow(book2.Id, qanitat.MembershipId, today);
    Console.WriteLine("Ibraheem was allowed to reserve the book.");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Qanitat's borrowing was blocked — {ex.Message}");
}

Console.WriteLine(
    $"Can a magazine be reserved? {magazine is IReservable}"
);

var fineBook = library.Return(
    book1.Id,
    today.AddDays(25)
);

Console.WriteLine(
    $"\"{book1.Title}\" returned 4 days late -> fine {fineBook:C}"
);

var fineDvd = library.Return(
    dvd.Id,
    today.AddDays(11)
);

Console.WriteLine(
    $"\"{dvd.Title}\" returned 4 days late -> fine {fineDvd:C}"
);

Console.WriteLine(
    $"{qanitat.Name}: {qanitat.Loans.Count} loans on record, " +
    $"{qanitat.ActiveLoanCount} active, {qanitat.TotalFinesOwed:C} owed."
);

Console.WriteLine();
Console.WriteLine("-- Search --");

foreach (var hint in library.Search("clean"))
{
    Console.WriteLine($" found: {hint.Describe()}");
}