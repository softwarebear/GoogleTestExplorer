namespace GoogleTestExplorer
{
    using System.Collections.Generic;

    public class TestListSource : ITestListSource
    {
        public IEnumerable<string> FindTestsIn(string executablePath)
        {
            throw new System.NotImplementedException();
        }
    }
}