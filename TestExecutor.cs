namespace GoogleTestExplorer
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

    public class TestExecutor : ITestExecutor
    {
        public const string ExecutorUri = "http://executor.test.google.softwarebear.com";
 
        public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            throw new NotImplementedException();
        }

        public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }
    }
}