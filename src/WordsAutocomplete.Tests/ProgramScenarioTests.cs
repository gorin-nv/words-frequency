using Moq;
using NUnit.Framework;

namespace WordsAutocomplete.Tests
{
    [TestFixture]
    public class ProgramScenarioTests
    {
        private ProgramScenario _programScenario;

        [SetUp]
        public void SetUp()
        {
            var dataSource = Mock.Of<IDataSource>();
            var dataDestination = Mock.Of<IDataDestination>();
            _programScenario = new ProgramScenario(dataSource, dataDestination);
        }
    }
}