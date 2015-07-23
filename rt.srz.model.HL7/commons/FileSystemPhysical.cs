// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileSystemPhysical.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The file system physical.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.commons
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System.Threading;

  using rt.srz.model.Hl7.commons.Enumerations;
  using rt.srz.model.Hl7.dotNetX;

  #endregion

  /// <summary>
  ///   The file system physical.
  /// </summary>
  public static class FileSystemPhysical
  {
    #region Public Methods and Operators

    /// <summary>
    /// The apply file path.
    /// </summary>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    public static void ApplyFilePath(string filePath)
    {
      var directoryName = Path.GetDirectoryName(filePath);
      if (!string.IsNullOrEmpty(directoryName))
      {
        CreateFolder(directoryName);
      }
    }

    /// <summary>
    /// The change extension.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    /// <param name="newExtension">
    /// The new extension.
    /// </param>
    public static void ChangeExtension(string path, string newExtension)
    {
      if (!string.IsNullOrEmpty(path))
      {
        MoveFile(path, Path.ChangeExtension(path, "dat"), null);
      }
    }

    /// <summary>
    /// The check file position.
    /// </summary>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    /// <param name="position">
    /// The position.
    /// </param>
    /// <param name="allowUnexistingFile">
    /// The allow unexisting file.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CheckFilePosition(string filePath, long position, bool allowUnexistingFile = false)
    {
      if (position < 0L)
      {
        return false;
      }

      if (position == 0L)
      {
        if (!allowUnexistingFile)
        {
          return FileExists(filePath);
        }

        return true;
      }

      return FileExists(filePath) && (position <= FileSize(filePath));
    }

    /// <summary>
    /// The clear folder.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    /// <param name="ignoreErrors">
    /// The ignore errors.
    /// </param>
    public static void ClearFolder(string path, bool ignoreErrors = false)
    {
      RemoveFilesAndFolders(Path.Combine(path, "*.*"), ignoreErrors);
    }

    /// <summary>
    /// The copy file.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="destination">
    /// The destination.
    /// </param>
    /// <param name="backupFolder">
    /// The backup folder.
    /// </param>
    public static void CopyFile(string source, string destination, string backupFolder = "")
    {
      var move = false;
      DoFileReposition(source, destination, backupFolder, move);
    }

    /// <summary>
    /// The create folder.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    public static void CreateFolder(string path)
    {
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }
    }

    /// <summary>
    /// The create folders.
    /// </summary>
    /// <param name="paths">
    /// The paths.
    /// </param>
    public static void CreateFolders(params string[] paths)
    {
      if (paths != null)
      {
        if (paths.Length == 1)
        {
          CreateFolder(paths[0]);
        }
        else
        {
          var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
          foreach (var str in paths)
          {
            if (!string.IsNullOrEmpty(str) && !set.Contains(str))
            {
              CreateFolder(str);
              set.Add(str);
            }
          }
        }
      }
    }

    /// <summary>
    /// The ensure solid file name.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string EnsureSolidFileName(string fileName)
    {
      if (string.IsNullOrEmpty(fileName))
      {
        return "%20";
      }

      var invalidFileNameChars = Path.GetInvalidFileNameChars();
      StringBuilder builder = null;
      var length = fileName.Length;
      for (var i = 0; i < length; i++)
      {
        var ch = fileName[i];
        if (((ch == '%') || (ch == ' ')) || invalidFileNameChars.Contains(ch))
        {
          if (builder == null)
          {
            var startIndex = 0;
            var num4 = i;
            var capacity = length + 4;
            builder = new StringBuilder(fileName, startIndex, num4, capacity);
          }

          if (ch == '%')
          {
            builder.Append("%%");
          }
          else
          {
            builder.AppendFormat("%{0:X}", (int)ch);
          }
        }
        else if (builder != null)
        {
          builder.Append(ch);
        }
      }

      if (builder != null)
      {
        return builder.ToString();
      }

      return fileName;
    }

    /// <summary>
    /// The file crop to stream.
    /// </summary>
    /// <param name="fileStream">
    /// The file stream.
    /// </param>
    /// <returns>
    /// The <see cref="long"/>.
    /// </returns>
    public static long FileCropToStream(FileStream fileStream)
    {
      var position = fileStream.Position;
      fileStream.SetLength(position);
      return position;
    }

    /// <summary>
    /// The file exists.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool FileExists(string path)
    {
      return !string.IsNullOrEmpty(path) && File.Exists(path);
    }

    /// <summary>
    /// The file locked.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    /// <param name="anyExceptionReturnsTrue">
    /// The any exception returns true.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool FileLocked(string path, bool anyExceptionReturnsTrue = false)
    {
      Exception lockedException = null;
      return CheckFileLocked(path, ref lockedException, anyExceptionReturnsTrue);
    }

    /// <summary>
    /// The file make writable.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    public static void FileMakeWritable(string path)
    {
      if (FileExists(path))
      {
        File.SetAttributes(path, FileAttributes.Normal);
      }
    }

    /// <summary>
    /// The file open write.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    /// <param name="truncate">
    /// The truncate.
    /// </param>
    /// <returns>
    /// The <see cref="FileStream"/>.
    /// </returns>
    public static FileStream FileOpenWrite(string path, bool truncate = false)
    {
      FileMakeWritable(path);
      var stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
      if (truncate)
      {
        try
        {
          stream.SetLength(0L);
        }
        catch
        {
          stream.Dispose();
          throw;
        }
      }

      return stream;
    }

    /// <summary>
    /// The file size.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    /// <returns>
    /// The <see cref="long"/>.
    /// </returns>
    public static long FileSize(string path)
    {
      var info = new FileInfo(path);
      return info.Length;
    }

    /// <summary>
    /// The folder exists.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool FolderExists(string path)
    {
      return !string.IsNullOrEmpty(path) && Directory.Exists(path);
    }

    /// <summary>
    /// The move file.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="destination">
    /// The destination.
    /// </param>
    /// <param name="backupFolder">
    /// The backup folder.
    /// </param>
    public static void MoveFile(string source, string destination, string backupFolder = "")
    {
      var move = true;
      DoFileReposition(source, destination, backupFolder, move);
    }

    /// <summary>
    /// The remove empty subfolders.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    /// <param name="ignoreErrors">
    /// The ignore errors.
    /// </param>
    public static void RemoveEmptySubfolders(string path, bool ignoreErrors = false)
    {
      if (Directory.Exists(path))
      {
        try
        {
          var selfRemove = false;
          DoRemoveFoldersWithoutFiles(path, selfRemove);
        }
        catch
        {
          if (!ignoreErrors)
          {
            throw;
          }
        }
      }
    }

    /// <summary>
    /// The remove file.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    public static void RemoveFile(string path)
    {
      if (FileExists(path))
      {
        var makeWritable = true;
        WaitFileUnlock(path, makeWritable);
        File.SetAttributes(path, FileAttributes.Normal);
        File.Delete(path);
      }
    }

    /// <summary>
    /// The remove files.
    /// </summary>
    /// <param name="paths">
    /// The paths.
    /// </param>
    public static void RemoveFiles(params string[] paths)
    {
      if (paths != null)
      {
        RemoveFiles((IEnumerable<string>)paths);
      }
    }

    /// <summary>
    /// The remove files.
    /// </summary>
    /// <param name="paths">
    /// The paths.
    /// </param>
    public static void RemoveFiles(IEnumerable<string> paths)
    {
      if (paths != null)
      {
        foreach (var str in paths)
        {
          RemoveFile(str);
        }
      }
    }

    /// <summary>
    /// The remove files.
    /// </summary>
    /// <param name="mask">
    /// The mask.
    /// </param>
    public static void RemoveFiles(string mask)
    {
      RemoveFiles(Directory.GetFiles(Path.GetDirectoryName(mask), Path.GetFileName(mask)));
    }

    /// <summary>
    /// The remove files and folders.
    /// </summary>
    /// <param name="mask">
    /// The mask.
    /// </param>
    /// <param name="ignoreErrors">
    /// The ignore errors.
    /// </param>
    public static void RemoveFilesAndFolders(string mask, bool ignoreErrors = false)
    {
      RemoveFiles(mask);
      RemoveFolders(mask, ignoreErrors);
    }

    /// <summary>
    /// The remove folder.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    /// <param name="ignoreErrors">
    /// The ignore errors.
    /// </param>
    public static void RemoveFolder(string path, bool ignoreErrors = false)
    {
      if (Directory.Exists(path))
      {
        try
        {
          DoRemoveFolder(path);
        }
        catch
        {
          if (!ignoreErrors)
          {
            throw;
          }
        }
      }
    }

    /// <summary>
    /// The remove folders.
    /// </summary>
    /// <param name="paths">
    /// The paths.
    /// </param>
    public static void RemoveFolders(params string[] paths)
    {
      RemoveFolders(false, paths);
    }

    /// <summary>
    /// The remove folders.
    /// </summary>
    /// <param name="paths">
    /// The paths.
    /// </param>
    /// <param name="ignoreErrors">
    /// The ignore errors.
    /// </param>
    public static void RemoveFolders(IEnumerable<string> paths, bool ignoreErrors = false)
    {
      if (paths != null)
      {
        foreach (var str in paths)
        {
          RemoveFolder(str, ignoreErrors);
        }
      }
    }

    /// <summary>
    /// The remove folders.
    /// </summary>
    /// <param name="ignoreErrors">
    /// The ignore errors.
    /// </param>
    /// <param name="paths">
    /// The paths.
    /// </param>
    public static void RemoveFolders(bool ignoreErrors, params string[] paths)
    {
      if (paths != null)
      {
        RemoveFolders(paths, ignoreErrors);
      }
    }

    /// <summary>
    /// The remove folders.
    /// </summary>
    /// <param name="mask">
    /// The mask.
    /// </param>
    /// <param name="ignoreErrors">
    /// The ignore errors.
    /// </param>
    public static void RemoveFolders(string mask, bool ignoreErrors = false)
    {
      RemoveFolders(ignoreErrors, Directory.GetDirectories(Path.GetDirectoryName(mask), Path.GetFileName(mask)));
    }

    /// <summary>
    /// The retrieve relative path.
    /// </summary>
    /// <param name="rootFolder">
    /// The root folder.
    /// </param>
    /// <param name="path">
    /// The path.
    /// </param>
    /// <param name="foreignYieldsNull">
    /// The foreign yields null.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveRelativePath(string rootFolder, string path, bool foreignYieldsNull = true)
    {
      if ((path != null) && path.StartsWith(rootFolder, StringComparison.OrdinalIgnoreCase))
      {
        path = path.Substring(rootFolder.Length);
        if ((path.Length > 0) && (path[0] == Path.DirectorySeparatorChar))
        {
          path = path.Substring(1);
        }

        return path;
      }

      if (!foreignYieldsNull)
      {
        return path;
      }

      return null;
    }

    /// <summary>
    ///   The retrieve temporary folder.
    /// </summary>
    /// <returns>
    ///   The <see cref="string" />.
    /// </returns>
    public static string RetrieveTemporaryFolder()
    {
      return Path.GetTempPath();
    }

    /// <summary>
    /// The select file infos.
    /// </summary>
    /// <param name="folderPath">
    /// The folder path.
    /// </param>
    /// <param name="searchMask">
    /// The search mask.
    /// </param>
    /// <param name="searchMode">
    /// The search mode.
    /// </param>
    /// <param name="sortMode">
    /// The sort mode.
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// </exception>
    public static IEnumerable<FileInfo> SelectFileInfos(
      string folderPath, 
      string searchMask = "", 
      TreeCollectMode searchMode = 0, 
      EntriesSortMode sortMode = 0)
    {
      IEnumerable<FileInfo> files;
      var bTrimFirst = true;
      folderPath = TStringHelper.StringToEmpty(folderPath, bTrimFirst);
      if (!FolderExists(folderPath))
      {
        return null;
      }

      var folderInfo = new DirectoryInfo(folderPath);
      switch (searchMode)
      {
        case TreeCollectMode.RootOnly:
          files = folderInfo.GetFiles(CorrectFileSearchMask(searchMask));
          break;

        case TreeCollectMode.LeavesOnly:
        {
          var list = new List<FileInfo>();
          DoCollectFinalFiles(list, folderInfo, CorrectFileSearchMask(searchMask));
          files = list;
          break;
        }

        case TreeCollectMode.Everywhere:
          files = folderInfo.GetFiles(CorrectFileSearchMask(searchMask), SearchOption.AllDirectories);
          break;

        default:
          throw new ArgumentException(string.Format("режим поиска файлов не поддерживается: {0}", searchMode));
      }

      switch (sortMode)
      {
        case EntriesSortMode.Unsorted:
          return files;

        case EntriesSortMode.NameAscending:
          return from f in files orderby f.FullName select f;

        case EntriesSortMode.NameDescending:
          return from f in files orderby f.FullName descending select f;

        case EntriesSortMode.CreationPointAscending:
          return from f in files orderby f.CreationTimeUtc select f;

        case EntriesSortMode.CreationPointDescending:
          return from f in files orderby f.CreationTimeUtc descending select f;

        case EntriesSortMode.ModificationPointAscending:
          return from f in files orderby f.LastWriteTimeUtc select f;

        case EntriesSortMode.ModificationPointDescending:
          return from f in files orderby f.LastWriteTimeUtc descending select f;
      }

      throw new ArgumentException(string.Format("режим сортировки при поиске файлов не поддерживается: {0}", sortMode));
    }

    /// <summary>
    /// The select files.
    /// </summary>
    /// <param name="folderPath">
    /// The folder path.
    /// </param>
    /// <param name="searchMask">
    /// The search mask.
    /// </param>
    /// <param name="searchMode">
    /// The search mode.
    /// </param>
    /// <param name="sortMode">
    /// The sort mode.
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable"/>.
    /// </returns>
    public static IEnumerable<string> SelectFiles(
      string folderPath, 
      string searchMask = "", 
      TreeCollectMode searchMode = 0, 
      EntriesSortMode sortMode = 0)
    {
      var enumerable = SelectFileInfos(folderPath, searchMask, searchMode, sortMode);
      if (enumerable == null)
      {
        return null;
      }

      return from f in enumerable select f.FullName;
    }

    /// <summary>
    /// The wait file unlock.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    /// <param name="makeWritable">
    /// The make writable.
    /// </param>
    public static void WaitFileUnlock(string path, bool makeWritable = false)
    {
      if (FileExists(path))
      {
        if (makeWritable)
        {
          FileMakeWritable(path);
        }

        Exception lockedException = null;
        while (CheckFileLocked(path, ref lockedException, false))
        {
          Thread.Sleep(100);
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The check file locked.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    /// <param name="lockedException">
    /// The locked exception.
    /// </param>
    /// <param name="anyExceptionReturnsTrue">
    /// The any exception returns true.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private static bool CheckFileLocked(
      string path, 
      ref Exception lockedException, 
      bool anyExceptionReturnsTrue = false)
    {
      if (FileExists(path))
      {
        try
        {
          var fileAttributes = File.GetAttributes(path);
          var flag = (fileAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
          try
          {
            if (flag)
            {
              File.SetAttributes(path, FileAttributes.Normal);
            }

            File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None).Dispose();
          }
          finally
          {
            if (flag)
            {
              File.SetAttributes(path, fileAttributes);
            }
          }
        }
        catch (IOException exception)
        {
          if ((lockedException == null) || (lockedException.Message != exception.Message))
          {
            FomsLogger.WriteLog(
                                LogType.Local, 
                                string.Format("[{0}] {1}", exception.Message, path), 
                                "[FileLocked] ", 
                                LogErrorType.None);
          }

          lockedException = exception;
          return true;
        }
        catch (Exception exception2)
        {
          if (!anyExceptionReturnsTrue)
          {
            throw;
          }

          FomsLogger.WriteError(exception2, null);
          lockedException = exception2;
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// The correct file search mask.
    /// </summary>
    /// <param name="searchMask">
    /// The search mask.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private static string CorrectFileSearchMask(string searchMask)
    {
      if (!string.IsNullOrEmpty(searchMask))
      {
        return searchMask;
      }

      return "*";
    }

    /// <summary>
    /// The do collect final files.
    /// </summary>
    /// <param name="files">
    /// The files.
    /// </param>
    /// <param name="folderInfo">
    /// The folder info.
    /// </param>
    /// <param name="searchMask">
    /// The search mask.
    /// </param>
    private static void DoCollectFinalFiles(IList<FileInfo> files, DirectoryInfo folderInfo, string searchMask)
    {
      var directories = folderInfo.GetDirectories();
      if (directories != null)
      {
        var flag = false;
        foreach (var info in directories)
        {
          flag = true;
          DoCollectFinalFiles(files, info, searchMask);
        }

        if (flag)
        {
          return;
        }
      }

      var infoArray2 = folderInfo.GetFiles(searchMask);
      if (infoArray2 != null)
      {
        foreach (var info2 in infoArray2)
        {
          files.Add(info2);
        }
      }
    }

    /// <summary>
    /// The do file reposition.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="destination">
    /// The destination.
    /// </param>
    /// <param name="backupFolder">
    /// The backup folder.
    /// </param>
    /// <param name="move">
    /// The move.
    /// </param>
    private static void DoFileReposition(string source, string destination, string backupFolder, bool move)
    {
      if (string.Compare(source, destination, StringComparison.OrdinalIgnoreCase) == 0)
      {
        if (backupFolder != null)
        {
          CopyFile(destination, Path.Combine(backupFolder, Path.GetFileName(destination)), null);
        }
      }
      else if (FileExists(source))
      {
        ApplyFilePath(destination);
        if (FileExists(destination))
        {
          if (backupFolder != null)
          {
            var strB = Path.Combine(backupFolder, Path.GetFileName(destination));
            if (string.Compare(source, strB, StringComparison.OrdinalIgnoreCase) == 0)
            {
              RemoveFile(destination);
            }
            else
            {
              MoveFile(destination, strB, null);
            }
          }
          else
          {
            RemoveFile(destination);
          }
        }

        if (move)
        {
          var makeWritable = true;
          WaitFileUnlock(source, makeWritable);
          File.Move(source, destination);
        }
        else
        {
          File.Copy(source, destination);
        }
      }
    }

    /// <summary>
    /// The do prepare folder to remove.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    private static void DoPrepareFolderToRemove(string path)
    {
      File.SetAttributes(path, FileAttributes.Normal);
      var files = Directory.GetFiles(path);
      if (files != null)
      {
        foreach (var str in files)
        {
          File.SetAttributes(str, FileAttributes.Normal);
        }
      }

      files = Directory.GetDirectories(path);
      if (files != null)
      {
        foreach (var str2 in files)
        {
          DoPrepareFolderToRemove(str2);
        }
      }
    }

    /// <summary>
    /// The do remove folder.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    private static void DoRemoveFolder(string path)
    {
      try
      {
        var recursive = true;
        Directory.Delete(path, recursive);
      }
      catch
      {
        DoPrepareFolderToRemove(path);
        var flag2 = true;
        Directory.Delete(path, flag2);
      }
    }

    /// <summary>
    /// The do remove folders without files.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    /// <param name="selfRemove">
    /// The self remove.
    /// </param>
    private static void DoRemoveFoldersWithoutFiles(string path, bool selfRemove = true)
    {
      IEnumerable<string> directories = Directory.GetDirectories(path);
      if (directories != null)
      {
        foreach (var str in directories)
        {
          DoRemoveFoldersWithoutFiles(str, true);
        }
      }

      if (selfRemove)
      {
        IEnumerable<string> fileSystemEntries = Directory.GetFileSystemEntries(path);
        if (fileSystemEntries != null)
        {
          using (var enumerator2 = fileSystemEntries.GetEnumerator())
          {
            while (enumerator2.MoveNext())
            {
              var current = enumerator2.Current;
              return;
            }
          }
        }

        DoRemoveFolder(path);
      }
    }

    #endregion
  }
}