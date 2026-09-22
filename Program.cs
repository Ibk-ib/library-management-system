Book book1 = new Book("The Great Gatsby", "F. Scott Fitzgerald", 1925);
book1.IsOnLoan = false;

Book book2 = new Book("To Kill a Mockingbird", "Harper Lee", 1960);
book2.IsOnLoan = false;

Book book3 = new Book("1984", "George Orwell", 1949);
book3.IsOnLoan = false;


List<Book> books = new List<Book>();
    books.Add(book1);
    books.Add(book2);
    books.Add(book3);

foreach (Book book in books) {
    Console.WriteLine("{0} ({1}) by {2} - {3}", book.Title, book.PublicationYear, book.Author, book.IsOnLoan ? "On Loan" : "Available");
}