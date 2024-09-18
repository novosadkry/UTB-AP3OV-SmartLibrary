using SmartLibrary.Core.Models;

namespace SmartLibrary.Tests.Models
{
    public class ReaderTests
    {
        public static TheoryData<DateTime> BirthDates =>
            new()
            {
                new DateTime(2000, 12, 31),
                new DateTime(2000, 1, 1),
                DateTime.Today.AddYears(-1).AddDays(1)
            };

        [Fact]
        public void FullName_ReturnsCorrectFullName()
        {
            // Arrange
            var reader = new Reader
            {
                FirstName = "Keanu",
                LastName = "Reeves"
            };

            // Act
            var fullName = reader.FullName;

            // Assert
            Assert.Equal("Keanu Reeves", fullName);
        }

        [Theory]
        [MemberData(nameof(BirthDates))]
        public void Age_ReturnsCorrectAge(DateTime birthDate)
        {
            // Arrange
            var reader = new Reader
            {
                BirthDate = birthDate
            };

            var now = DateTime.Now;
            var expectedAge = now.Year - birthDate.Year;
            if (now < birthDate.AddYears(expectedAge)) expectedAge--;

            // Act
            var age = reader.Age;

            // Assert
            Assert.Equal(expectedAge, age);
        }
    }
}
