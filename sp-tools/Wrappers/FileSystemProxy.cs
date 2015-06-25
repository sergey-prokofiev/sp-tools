﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpTools.Wrappers
{
	/// <summary>
	/// Wrapper on file system
	/// </summary>
	public class FileSystemProxy : IFileSystemProxy
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

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
			Logger.Debug("Directory '{0}' created successfully", result);
			return result;
		}

		/// <summary>
		/// <see cref="IFileSystemProxy"/>
		/// </summary>
		public void DeleteFile(string fname)
		{
			File.Delete(fname);
			Logger.Debug("File '{0}' was deleted", fname);
		}

		/// <summary>
		/// <see cref="IFileSystemProxy"/>
		/// </summary>
		public void DeleteDirectory(string dirName)
		{
			Directory.Delete(dirName, true);
			Logger.Debug("Directory '{0}' was deleted", dirName);
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
		public IReadOnlyCollection<string> FindFiles(string dir, string mask)
		{
			var files = Directory.EnumerateFiles(dir, mask, SearchOption.TopDirectoryOnly);
			return files.ToArray();
		}

	}
}