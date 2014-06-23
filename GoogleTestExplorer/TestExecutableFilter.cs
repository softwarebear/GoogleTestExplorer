namespace GoogleTestExplorer
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class TestExecutableFilter : ITestExecutableFilter
    {
        private Regex testRegEx = new Regex("tests?(.unit)?", RegexOptions.IgnoreCase);

        public IEnumerable<string> FilterIn(IEnumerable<string> executables)
        {
            return executables.Where(s => testRegEx.Match(s).Success);
        }
    }
}