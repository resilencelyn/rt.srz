// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextFileWriter.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The text file writer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.commons
{
  #region references

  using System;
  using System.IO;

  #endregion

  /// <summary>
  ///   The text file writer.
  /// </summary>
  public class TextFileWriter : IDisposable
  {
    #region Fields

    /// <summary>
    ///   The file.
    /// </summary>
    public readonly FileStream File;

    /// <summary>
    ///   The writer.
    /// </summary>
    public readonly TextWriter Writer;

    /// <summary>
    ///   The is disposed.
    /// </summary>
    private bool isDisposed;

    #endregion

    // public TextFileWriter(string path, Encoding encoding = new Encoding(), bool truncate = false)
    // {
    // this.File = FileSystemPhysical.FileOpenWrite(path, truncate);
    // try
    // {
    // this.Writer = new StreamWriter(this.File, (encoding != null) ? encoding : Encoding.Default);
    // }
    // catch
    // {
    // this.File.Dispose();
    // this.File = null;
    // throw;
    // }
    // }
    #region Public Methods and Operators

    /// <summary>
    ///   The dispose.
    /// </summary>
    public void Dispose()
    {
      var isFinalization = false;
      DoDispose(isFinalization);
      GC.SuppressFinalize(this);
    }

    #endregion

    #region Methods

    /// <summary>
    /// The do dispose.
    /// </summary>
    /// <param name="isFinalization">
    /// The is finalization.
    /// </param>
    private void DoDispose(bool isFinalization)
    {
      if (!isDisposed)
      {
        if (!isFinalization)
        {
          if (Writer != null)
          {
            Writer.Dispose();
          }

          if (File != null)
          {
            File.Dispose();
          }
        }

        isDisposed = true;
      }
    }

    #endregion
  }
}