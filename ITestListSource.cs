namespace GoogleTestExplorer
{
    using System.Collections.Generic;

    public interface ITestListSource
    {
        IEnumerable<string> FindTestsIn(string executablePath);
    }
}