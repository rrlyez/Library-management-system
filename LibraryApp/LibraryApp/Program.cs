using LibraryApp.Data;
using LibraryApp.Repositories;

var connectionString =
    "Host=localhost;Port=5432;Database=library;Username=postgres;Password=";

var database = new DatabaseConnection(connectionString);
var unitOfWork = new UnitOfWork(database);

var books = await unitOfWork.Books.GetAvailableBooksAsync();

Console.WriteLine("Available books:");

foreach (var book in books)
{
    Console.WriteLine($"{book.BookId} | {book.Title} | {book.InventoryNumber} | {book.BranchName}");
}

var issues = await unitOfWork.Issues.GetActiveIssuesAsync();

Console.WriteLine();
Console.WriteLine("Active issues:");

foreach (var issue in issues)
{
    Console.WriteLine($"{issue.IssueId} | {issue.FirstName} {issue.LastName} | {issue.Title} | {issue.Status}");
}

var fines = await unitOfWork.ReaderFines.GetReaderFinesAsync();

Console.WriteLine();
Console.WriteLine("Reader fines:");

foreach (var fine in fines)
{
    Console.WriteLine($"{fine.FineId} | {fine.FirstName} {fine.LastName} | {fine.Amount} | {fine.Status}");
}

Console.WriteLine();
Console.WriteLine("Issue book:");

await unitOfWork.Issues.IssueBookAsync(
    3,
    2,
    1,
    DateTime.Today.AddDays(10),
    1
);

Console.WriteLine("Book issued successfully.");

Console.WriteLine();
Console.WriteLine("Active issues after issue:");

var issuesAfterIssue = await unitOfWork.Issues.GetActiveIssuesAsync();

foreach (var issue in issuesAfterIssue)
{
    Console.WriteLine(
        $"{issue.IssueId} | {issue.FirstName} {issue.LastName} | {issue.Title} | {issue.Status}"
    );
}

Console.WriteLine();
Console.WriteLine("Return book:");

await unitOfWork.Issues.ReturnBookAsync(
    2,
    1
);

Console.WriteLine("Book returned successfully.");

var issuesAfterReturn = await unitOfWork.Issues.GetActiveIssuesAsync();

Console.WriteLine();
Console.WriteLine("Active issues after return:");

foreach (var issue in issuesAfterReturn)
{
    Console.WriteLine(
        $"{issue.IssueId} | {issue.FirstName} {issue.LastName} | {issue.Title} | {issue.Status}"
    );
}