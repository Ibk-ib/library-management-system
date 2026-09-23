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

List<LibraryItem> items = new List<LibraryItem>();

items.Add(book1);
items.Add(book2);
items.Add(book3);
items.Add(dvd);
items.Add(magazine);

foreach (LibraryItem item in items)
{
    Console.WriteLine($"{item.Describe()} | loan for {item.LoanPeriodDays} days and Fine is: {item.DailyFine:C}");
}


Member qanitat = new Member("M003", "Emiola Qanitat");

book2.Reserve(qanitat);

Console.WriteLine(
    $"\"{book2.Title}\" is now reserved for {book2.ReservedFor}"
);