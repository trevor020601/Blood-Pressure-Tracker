using SharedLibrary.Extensions;

namespace Tests.Extensions;

public class DateTimeExtensionsTest
{
    [Fact]
    public void ShouldThrowExceptionIfStartDateIsAfterEndDate()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
        {
            new DateTimeRange(new DateTime(2025, 10, 14), new DateTime(2025, 10, 13));
        });
        Assert.Equal("Start date must occur before end date.", ex.Message);
    }

    [Fact]
    public void ShouldCalculateAgeInYearsFromDateTimeRange()
    {
        var range1 = new DateTimeRange(new DateTime(2001, 02, 06), new DateTime(2025, 10, 14));
        var ageInYears1 = DateTimeExtensions.CalculateAge(range1);
        Assert.Equal(24, ageInYears1);

        var range2 = new DateTimeRange(new DateTime(2001, 10, 15), new DateTime(2025, 10, 14));
        var ageInYears2 = DateTimeExtensions.CalculateAge(range2);
        Assert.Equal(23, ageInYears2);

        var range3 = new DateTimeRange(new DateTime(2000, 10, 13), new DateTime(2025, 10, 14));
        var ageInYears3 = DateTimeExtensions.CalculateAge(range3);
        Assert.Equal(25, ageInYears3);
    }
}
