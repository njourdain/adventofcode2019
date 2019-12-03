using System;
using Xunit;

namespace part2.tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(14, 2)]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void Test1(int mass, int totalFuelRequirement)
        {
            Assert.Equal(totalFuelRequirement, part2.Program.CalculateTotalFuelRequirement(mass));
        }
    }
}
