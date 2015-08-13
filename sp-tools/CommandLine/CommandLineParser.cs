using System.Collections.Generic;
using System.Text.RegularExpressions;
using SpTools.Validation;

namespace SpTools.CommandLine
{
    public class CommandLineParser : ICommandLineParser
    {

        public IDictionary<string, string> Parse(string commandLine)
        {
            ParametersValidator.IsNotNull(commandLine, ()=>commandLine);
            var result = new Dictionary<string, string>();
            var re = new Regex(@"(?<switch> -{1,2}\S*)(?:[=:]?|\s+)(?<value> [^-\s].*?)?(?=\s+[-\/]|$)", 
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matches = re.Matches(commandLine);
            foreach (Match m in matches)
            {
                var sw = m.Groups["switch"].Value;
                sw = sw.TrimStart(' ', '-');
                var val = m.Groups["value"].Value;
                val = val.Trim();
                result[sw] = val;
            }

            return result;
        }
    }
}