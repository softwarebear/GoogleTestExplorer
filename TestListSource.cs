namespace GoogleTestExplorer
{
    using System.Collections.Generic;
    using System.Threading;

    class TestListSource : ITestListSource
    {
        public IEnumerable<string> FindTestsIn(string executablePath)
        {
            throw new System.NotImplementedException();
        }
    }
}