using System.Collections.Generic;
using System.Text.RegularExpressions;
using Common.Logging;
using SpTools.Validation;

namespace SpTools.CommandLine
{
    public class CommandLineParser : ICommandLineParser
    {
        private static readonly ILog Logger = LogManager.GetLogger<CommandLineParser>();
        public IDictionary<string, string> Parse(string commandLine)
        {
            ParametersValidator.IsNotNull(commandLine, ()=>commandLine);
            Logger.DebugFormat("Parsing command line {0}", commandLine);
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
            Logger.DebugFormat("Parsed command line {0}, {1} matches", commandLine, result.Count);
            return result;
        }
    }
}