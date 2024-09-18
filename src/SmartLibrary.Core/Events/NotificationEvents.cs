using SmartLibrary.Core.Models;

namespace SmartLibrary.Core.Events
{
    public abstract record NotificationEvent;
    public record NewBookAddedEvent(Book Book) : NotificationEvent;
    public record BookBorrowedEvent(Reader Reader, Book Book) : NotificationEvent;
    public record OverdueBookReturnedEvent(Loan Loan) : NotificationEvent;
}
