using System;
using FluentAssertions;
using Xunit;

namespace Battle.Tests
{
    public class HeadQuartersTest
    {
        [Fact]
        public void EnlistSoldierToHQShouldReturnSoldierId()
        {
            var soldier1 = "Jos";
            var soldier2 = "Jos2";
            var hq = new HeadQuarters();

            var id1 = hq.ReportEnlistment(soldier1);
            var id2 = hq.ReportEnlistment(soldier2);

            id1.Should().NotBe(id2);
        }

        [Fact]
        public void REportEmptySoldierNameShouldThrowArgumentException()
        {
            var hq = new HeadQuarters();

            Assert.Throws<ArgumentException>( () => hq.ReportEnlistment(String.Empty));
        }
    }
}