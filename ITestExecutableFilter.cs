namespace GoogleTestExplorer
{
    using System.Collections.Generic;

    public interface ITestExecutableFilter
    {
        IEnumerable<string> FilterIn(IEnumerable<string> executables);
    }
}