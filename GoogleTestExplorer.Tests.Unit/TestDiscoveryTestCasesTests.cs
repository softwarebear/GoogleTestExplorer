namespace GoogleTestExplorer.Tests.Unit
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class TestDiscoveryTestCasesTests
    {
        private ITestListSource testListSource;

        private TestDiscoverer sut;

        private IEnumerable<string> executables;

        private IDiscoveryContext discoveryContext;
        private IMessageLogger messageLogger;
        private ITestCaseDiscoverySink testCaseDiscoverySink;

        [SetUp]
        public void SetUp()
        {
            GivenATestDiscoverer();
        }

        [TestCase]
        public void DoesNotDiscoverAnyTestCases()
        {
            GivenThereAreTestExecutablesWithNoTestCases();

            WhenTheUserRunsSomeTests();

            ThenNoTestCasesAreFound();
        }

        [TestCase]
        public void DoesDiscoverSomeTestCases()
        {
            GivenThereAreTestExecutablesWithSomeTestCases();

            WhenTheUserRunsSomeTests();

            ThenSomeTestCasesAreFound();
        }

        private void GivenATestDiscoverer()
        {
            testListSource = Substitute.For<ITestListSource>();

            sut = new TestDiscoverer(testListSource);

            discoveryContext = Substitute.For<IDiscoveryContext>();
            messageLogger = Substitute.For<IMessageLogger>();
            testCaseDiscoverySink = Substitute.For<ITestCaseDiscoverySink>();
        }

        private void GivenThereAreTestExecutablesWithNoTestCases()
        {
            executables = new[] { "tests.exe" };
        }

        private void GivenThereAreTestExecutablesWithSomeTestCases()
        {
            executables = new[] { "tests.exe" };

            testListSource.FindTestsIn(Arg.Is("tests.exe"));

            testCaseDiscoverySink.SendTestCase(
                Arg.Is<TestCase>(
                    tc => tc.FullyQualifiedName == "TestSuite.TestCase"
                    && tc.ExecutorUri == new Uri("http://explorer.test.google.softwarebear.com")));
        }

        private void WhenTheUserRunsSomeTests()
        {
            sut.DiscoverTests(executables, discoveryContext, messageLogger, testCaseDiscoverySink);
        }

        private void ThenNoTestCasesAreFound()
        {
            testCaseDiscoverySink.DidNotReceive().SendTestCase(Arg.Any<TestCase>());       
        }

        private void ThenSomeTestCasesAreFound()
        {
            testCaseDiscoverySink.Received().SendTestCase(Arg.Is<TestCase>(tc => tc.FullyQualifiedName == "TestSuite.TestCase"));
        }
    }
}
