namespace GoogleTestExplorer.Tests.Unit
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class TestDiscoveryFilteringTests
    {
        private TestDiscoverer sut;

        private IEnumerable<string> executables;

        private IDiscoveryContext discoveryContext;

        private IMessageLogger messageLogger;

        private ITestCaseDiscoverySink testCaseDiscoverySink;

        private ITestListSource testListSource;

        private ITestExecutableFilter testExecutableFilter;

        [SetUp]
        public void SetUp()
        {
            GivenATestDiscoverer();
        }

        [TestCase]
        public void DoesNotDiscoverUnitTests()
        {
            GivenThereAreNoTestExecutables();

            WhenTheUserRunsSomeTests();

            ThenNoTestsAreFound();
        }

        [TestCase]
        public void DiscoversTests()
        {
            GivenThereAreTestExecutablesEndingWithTestOrTests();

            WhenTheUserRunsSomeTests();

            ThenSomeTestsAreFound();
        }

        [TestCase]
        public void DiscoversTestsUnit()
        {
            GivenThereAreTestExecutablesEndingWithTestOrTestsDotUnit();

            WhenTheUserRunsSomeTests();

            ThenSomeTestsUnitAreFound();
        }

        private void GivenATestDiscoverer()
        {
            testListSource = Substitute.For<ITestListSource>();

            sut = new TestDiscoverer(testListSource);

            discoveryContext = Substitute.For<IDiscoveryContext>();
            messageLogger = Substitute.For<IMessageLogger>();
            testCaseDiscoverySink = Substitute.For<ITestCaseDiscoverySink>();
        }

        private void GivenThereAreNoTestExecutables()
        {
            executables= new[] { "wibble.exe" };
        }

        private void GivenThereAreTestExecutablesEndingWithTestOrTests()
        {
            executables = new[] { "wibble.exe", "wibble.TEST.exe", "wibble.test.exe", "wibble.tests.exe" };
        }

        private void GivenThereAreTestExecutablesEndingWithTestOrTestsDotUnit()
        {
            executables = new[] { "wibble.exe", "wibble.TEST.unit.exe", "wibble.test.UNIT.exe", "wibble.tests.unit.exe" };
        }

        private void WhenTheUserRunsSomeTests()
        {
            sut.DiscoverTests(executables, discoveryContext, messageLogger, testCaseDiscoverySink);
        }

        private void ThenNoTestsAreFound()
        {
            testListSource.DidNotReceive().FindTestsIn(Arg.Any<string>());
        }

        private void ThenSomeTestsAreFound()
        {
            testListSource.DidNotReceive().FindTestsIn(Arg.Is("wibble.exe"));
            testListSource.Received().FindTestsIn(Arg.Is("wibble.TEST.exe"));
            testListSource.Received().FindTestsIn(Arg.Is("wibble.test.exe"));
            testListSource.Received().FindTestsIn(Arg.Is("wibble.tests.exe"));
        }

        private void ThenSomeTestsUnitAreFound()
        {
            testListSource.DidNotReceive().FindTestsIn(Arg.Is("wibble.exe"));
            testListSource.Received().FindTestsIn(Arg.Is("wibble.TEST.unit.exe"));
            testListSource.Received().FindTestsIn(Arg.Is("wibble.test.UNIT.exe"));
            testListSource.Received().FindTestsIn(Arg.Is("wibble.tests.unit.exe"));
        }
    }
}
