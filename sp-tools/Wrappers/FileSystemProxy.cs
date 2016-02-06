using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Logging;

namespace SpTools.Wrappers
{
	/// <summary>
	/// Wrapper on file system
	/// </summary>
	public class FileSystemProxy : IFileSystemProxy
	{
		private static readonly ILog Logger = LogManager.GetLogger<FileSystemProxy>();
		/// <summary>
		/// <see cref="IFileSystemProxy"/>
		/// </summary>
		public string CreateTempEmptyFolder()
		{
			var tmp = Path.GetTempPath();
			var dir = Guid.NewGuid().ToString();
			var result = Path.Combine(tmp, dir);
			Directory.CreateDirectory(result);
			if (!result.EndsWith("\\"))
			{
				result += "\\";
			}
			Logger.TraceFormat("Temp dir {0} created", result);
			return result;
		}

		/// <summary>
		/// <see cref="IFileSystemProxy"/>
		/// </summary>
		public void DeleteFile(string fname)
		{
			File.Delete(fname);
			Logger.TraceFormat("File {0} deleted", fname);

		}

		/// <summary>
		/// <see cref="IFileSystemProxy"/>
		/// </summary>
		public void DeleteDirectory(string dirName)
		{
			Directory.Delete(dirName, true);
			Logger.TraceFormat("Dir {0} deleted", dirName);
		}

		/// <summary>
		/// Combines two paths
		/// </summary>
		/// <param name="path1"></param>
		/// <param name="path2"></param>
		/// <returns></returns>
		public string Combine(string path1, string path2)
		{
			var result = Path.Combine(path1, path2);
			return result;
		}

		/// <summary>
		/// <see cref="IFileSystemProxy"/>
		/// </summary>
		public IReadOnlyCollection<string> FindFiles(string dir, string mask, bool searchInSubdirs)
		{
		    var opt = searchInSubdirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var files = Directory.EnumerateFiles(dir, mask, opt);
			return files.ToArray();
		}

	}
}