namespace SmartLibrary.Core.Models
{
    public class Reader
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Loan> Loans { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public int Age
        {
            get
            {
                var age = DateTime.Now.Year - BirthDate.Year;
                if (DateTime.Now < BirthDate.AddYears(age)) age--;
                return age;
            }
        }
    }
}
