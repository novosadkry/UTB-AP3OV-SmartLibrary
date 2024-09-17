namespace SmartLibrary.Core.Models
{
    public class Reader
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Loan> Loans { get; set; }

        public string FullName => $"{FirstName} {LastName}";
        public int Age => DateTime.Now.Year - BirthDate.Year;
    }
}
