using System.Collections.Generic;

namespace SpTools.Wrappers
{
    /// <summary>
    /// A wrapepr for file system operations
    /// </summary>
    public interface IFileSystemProxy
    {
        /// <summary>
        /// Create a subfoilder in %TEMP% fodler
        /// </summary>
        /// <returns></returns>
        string CreateTempEmptyFolder();

        /// <summary>
        /// Delete a file
        /// </summary>
        /// <param name="fname"></param>
        void DeleteFile(string fname);

        /// <summary>
        /// Delete an empty directory
        /// </summary>
        void DeleteDirectory(string dirName);

        /// <summary>
        /// Compine parts of path
        /// </summary>
        string Combine(string path1, string path2);

        /// <summary>
        /// Search for files in a directory
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="mask"></param>
        /// <param name="searchInSubdirs"></param>
        /// <returns></returns>
        IReadOnlyCollection<string> FindFiles(string dir, string mask, bool searchInSubdirs=false);
    }
}