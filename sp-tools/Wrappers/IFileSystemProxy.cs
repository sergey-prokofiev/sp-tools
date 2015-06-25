using System.Collections.Generic;

namespace SpTools.Wrappers
{
    public interface IFileSystemProxy
    {
        string CreateTempEmptyFolder();
        void DeleteFile(string fname);
        void DeleteDirectory(string dirName);
        string Combine(string path1, string path2);
        IReadOnlyCollection<string> FindFiles(string dir, string mask);
    }
}