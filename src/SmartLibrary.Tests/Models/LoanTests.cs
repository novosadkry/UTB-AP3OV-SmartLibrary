using SmartLibrary.Core.Models;

namespace SmartLibrary.Tests.Models
{
    public class LoanTests
    {
        [Fact]
        public void IsOverdue_ReturnsTrue_WhenLoanIsOverdue()
        {
            // Arrange
            var loan = new Loan
            {
                DueDate = DateTime.Now.AddDays(-1),
                ReturnDate = null
            };

            // Act
            var isOverdue = loan.IsOverdue;

            // Assert
            Assert.True(isOverdue);
        }

        [Fact]
        public void IsOverdue_ReturnsFalse_WhenLoanIsReturned()
        {
            // Arrange
            var loan = new Loan
            {
                DueDate = DateTime.Now.AddDays(-1),
                ReturnDate = DateTime.Now
            };

            // Act
            var isOverdue = loan.IsOverdue;

            // Assert
            Assert.False(isOverdue);
        }

        [Fact]
        public void DaysOverdue_ReturnsCorrectNumberOfDays()
        {
            // Arrange
            var loan = new Loan
            {
                DueDate = DateTime.Now.AddDays(-5),
                ReturnDate = null
            };

            // Act
            var daysOverdue = loan.DaysOverdue;

            // Assert
            Assert.Equal(5, daysOverdue);
        }

        [Fact]
        public void DaysOverdue_ShouldReturnZero_WhenLoanIsReturned()
        {
            // Arrange
            var loan = new Loan
            {
                DueDate = DateTime.Now.AddDays(-5),
                ReturnDate = DateTime.Today
            };

            // Act
            var daysOverdue = loan.DaysOverdue;

            // Assert
            Assert.Equal(0, daysOverdue);
        }
    }
}
