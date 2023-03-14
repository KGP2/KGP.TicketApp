using FluentAssertions;

namespace KGP.TicketApp.Backend.Tests
{
    public class Tests
    {
        private string value;

        [SetUp]
        public void Setup()
        {
            value = string.Empty;
        }

        [Test]
        public void Test1()
        {
            value.Length.Should().Be(0);
        }
    }
}