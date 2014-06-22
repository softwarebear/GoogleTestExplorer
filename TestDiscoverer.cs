namespace GoogleTestExplorer
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

    [FileExtension(@".exe")]
    [DefaultExecutorUri(TestExecutor.ExecutorUri)]
    public class TestDiscoverer : ITestDiscoverer
    {
        private readonly ITestExecutableFilter testExecutableFilter = new TestExecutableFilter();

        private readonly ITestListSource testListSource = new TestListSource();

        public TestDiscoverer(ITestListSource testListSource)
        {
            this.testListSource = testListSource;
        }

        public void DiscoverTests(
            IEnumerable<string> sources,
            IDiscoveryContext discoveryContext,
            IMessageLogger logger,
            ITestCaseDiscoverySink discoverySink)
        {
            var recognisedSources = testExecutableFilter.FilterIn(sources);

            Parallel.ForEach(
                recognisedSources,
                recognisedSource =>
                    {
                        foreach (var testCase in testListSource.FindTestsIn(recognisedSource))
                        {
                            discoverySink.SendTestCase(
                                new TestCase(testCase, new Uri(TestExecutor.ExecutorUri), recognisedSource));
                        }
                    });
        }
    }
}
