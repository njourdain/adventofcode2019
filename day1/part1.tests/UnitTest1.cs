using Xunit;

namespace part1.tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void It_ShouldReturnRequiredFuel(int mass, int fuelRequirement)
        {
            Assert.Equal(part1.Program.CalculateFuelRequirement(mass), fuelRequirement);
        }
    }
}
