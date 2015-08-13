using System.Collections.Generic;

namespace SpTools.CommandLine
{
    /// <summary>
    /// Interface for command line parsing
    /// </summary>
    public interface ICommandLineParser
    {
        /// <summary>
        /// Parse command line to parameter-value collection.
        /// </summary>
        /// <param name="commandLine"></param>
        /// <returns></returns>
        IDictionary<string, string> Parse(string commandLine);
    }
}