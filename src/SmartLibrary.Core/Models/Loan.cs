namespace SmartLibrary.Core.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ReaderId { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public virtual Book Book { get; set; }
        public virtual Reader Reader { get; set; }

        public bool IsOverdue => ReturnDate == null && DateTime.Now > DueDate;
        public int DaysOverdue => IsOverdue ? (DateTime.Now - DueDate).Days : 0;
    }
}
