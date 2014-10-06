// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileStreamFoms.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The file stream foms.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.commons
{
  #region references

  using System;
  using System.IO;
  using System.Security.Cryptography;

  #endregion

  /// <summary>
  ///   The file stream foms.
  /// </summary>
  public sealed class FileStreamFoms : FileStream
  {
    #region Fields

    /// <summary>
    ///   The hash buffer.
    /// </summary>
    private readonly byte[] hashBuffer;

    /// <summary>
    ///   The hash read calculator.
    /// </summary>
    private HashAlgorithm hashReadCalculator;

    /// <summary>
    ///   The hash read calculator finalize.
    /// </summary>
    private bool hashReadCalculatorFinalize;

    /// <summary>
    ///   The hash write calculator.
    /// </summary>
    private HashAlgorithm hashWriteCalculator;

    /// <summary>
    ///   The hash write calculator finalize.
    /// </summary>
    private bool hashWriteCalculatorFinalize;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FileStreamFoms"/> class.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    /// <param name="mode">
    /// The mode.
    /// </param>
    /// <param name="access">
    /// The access.
    /// </param>
    /// <param name="share">
    /// The share.
    /// </param>
    public FileStreamFoms(string path, FileMode mode, FileAccess access, FileShare share)
      : base(path, mode, access, share)
    {
      hashBuffer = new byte[1];
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the hash read calculator.
    /// </summary>
    public HashAlgorithm HashReadCalculator
    {
      get
      {
        return hashReadCalculator;
      }
    }

    /// <summary>
    ///   Gets the hash write calculator.
    /// </summary>
    public HashAlgorithm HashWriteCalculator
    {
      get
      {
        return hashWriteCalculator;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The file open read.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    /// <returns>
    /// The <see cref="FileStreamFoms"/>.
    /// </returns>
    public static FileStreamFoms FileOpenRead(string path)
    {
      return new FileStreamFoms(path, FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    /// <summary>
    /// The file open write.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    /// <returns>
    /// The <see cref="FileStreamFoms"/>.
    /// </returns>
    public static FileStreamFoms FileOpenWrite(string path)
    {
      FileSystemPhysical.FileMakeWritable(path);
      return new FileStreamFoms(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
    }

    /// <summary>
    /// The begin read.
    /// </summary>
    /// <param name="array">
    /// The array.
    /// </param>
    /// <param name="offset">
    /// The offset.
    /// </param>
    /// <param name="numBytes">
    /// The num bytes.
    /// </param>
    /// <param name="userCallback">
    /// The user callback.
    /// </param>
    /// <param name="stateObject">
    /// The state object.
    /// </param>
    /// <returns>
    /// The <see cref="IAsyncResult"/>.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// </exception>
    public override IAsyncResult BeginRead(
      byte[] array, 
      int offset, 
      int numBytes, 
      AsyncCallback userCallback, 
      object stateObject)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// The begin write.
    /// </summary>
    /// <param name="array">
    /// The array.
    /// </param>
    /// <param name="offset">
    /// The offset.
    /// </param>
    /// <param name="numBytes">
    /// The num bytes.
    /// </param>
    /// <param name="userCallback">
    /// The user callback.
    /// </param>
    /// <param name="stateObject">
    /// The state object.
    /// </param>
    /// <returns>
    /// The <see cref="IAsyncResult"/>.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// </exception>
    public override IAsyncResult BeginWrite(
      byte[] array, 
      int offset, 
      int numBytes, 
      AsyncCallback userCallback, 
      object stateObject)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   The draw out read hash.
    /// </summary>
    /// <returns>
    ///   The <see cref="byte[]" />.
    /// </returns>
    public byte[] DrawOutReadHash()
    {
      return DrawOutHash(ref hashReadCalculator, ref hashReadCalculatorFinalize);
    }

    /// <summary>
    ///   The draw out write hash.
    /// </summary>
    /// <returns>
    ///   The <see cref="byte[]" />.
    /// </returns>
    public byte[] DrawOutWriteHash()
    {
      return DrawOutHash(ref hashWriteCalculator, ref hashWriteCalculatorFinalize);
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="array">
    /// The array.
    /// </param>
    /// <param name="offset">
    /// The offset.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public override int Read(byte[] array, int offset, int count)
    {
      var num = base.Read(array, offset, count);
      if (num > 0)
      {
        AddToHash(hashReadCalculator, array, offset, num);
      }

      return num;
    }

    /// <summary>
    ///   The read byte.
    /// </summary>
    /// <returns>
    ///   The <see cref="int" />.
    /// </returns>
    public override int ReadByte()
    {
      var num = base.ReadByte();
      if (num >= 0)
      {
        hashBuffer[0] = (byte)num;
        var offset = 0;
        var count = 1;
        AddToHash(hashReadCalculator, hashBuffer, offset, count);
      }

      return num;
    }

    /// <summary>
    /// The reset hash read calculator.
    /// </summary>
    /// <param name="hashCalculator">
    /// The hash calculator.
    /// </param>
    public void ResetHashReadCalculator(HashAlgorithm hashCalculator)
    {
      ResetHashCalculator(ref hashReadCalculator, ref hashReadCalculatorFinalize, hashCalculator);
    }

    /// <summary>
    /// The reset hash write calculator.
    /// </summary>
    /// <param name="hashCalculator">
    /// The hash calculator.
    /// </param>
    public void ResetHashWriteCalculator(HashAlgorithm hashCalculator)
    {
      ResetHashCalculator(ref hashWriteCalculator, ref hashWriteCalculatorFinalize, hashCalculator);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="array">
    /// The array.
    /// </param>
    /// <param name="offset">
    /// The offset.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    public override void Write(byte[] array, int offset, int count)
    {
      base.Write(array, offset, count);
      AddToHash(hashWriteCalculator, array, offset, count);
    }

    /// <summary>
    /// The write byte.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public override void WriteByte(byte value)
    {
      base.WriteByte(value);
      hashBuffer[0] = value;
      var offset = 0;
      var count = 1;
      AddToHash(hashWriteCalculator, hashBuffer, offset, count);
    }

    #endregion

    #region Methods

    /// <summary>
    /// The add to hash.
    /// </summary>
    /// <param name="hashCalculator">
    /// The hash calculator.
    /// </param>
    /// <param name="buffer">
    /// The buffer.
    /// </param>
    /// <param name="offset">
    /// The offset.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    private static void AddToHash(HashAlgorithm hashCalculator, byte[] buffer, int offset, int count)
    {
      if (hashCalculator != null)
      {
        hashCalculator.TransformBlock(buffer, offset, count, buffer, offset);
      }
    }

    /// <summary>
    /// The reset hash calculator.
    /// </summary>
    /// <param name="hashCalculator">
    /// The hash calculator.
    /// </param>
    /// <param name="hashCalculatorFinalize">
    /// The hash calculator finalize.
    /// </param>
    /// <param name="newHashCalculator">
    /// The new hash calculator.
    /// </param>
    private static void ResetHashCalculator(
      ref HashAlgorithm hashCalculator, 
      ref bool hashCalculatorFinalize, 
      HashAlgorithm newHashCalculator)
    {
      var algorithm = hashCalculator;
      hashCalculator = newHashCalculator;
      if (hashCalculator != null)
      {
        hashCalculator.Initialize();
        hashCalculatorFinalize = true;
      }
      else if (algorithm != null)
      {
        algorithm.Clear();
      }
    }

    /// <summary>
    /// The draw out hash.
    /// </summary>
    /// <param name="hashCalculator">
    /// The hash calculator.
    /// </param>
    /// <param name="hashCalculatorFinalize">
    /// The hash calculator finalize.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    private byte[] DrawOutHash(ref HashAlgorithm hashCalculator, ref bool hashCalculatorFinalize)
    {
      if (hashCalculator != null)
      {
        try
        {
          FinalizeHash(hashCalculator, hashCalculatorFinalize);
          return hashCalculator.Hash;
        }
        finally
        {
          HashAlgorithm newHashCalculator = null;
          ResetHashCalculator(ref hashCalculator, ref hashCalculatorFinalize, newHashCalculator);
        }
      }

      return null;
    }

    /// <summary>
    /// The finalize hash.
    /// </summary>
    /// <param name="hashCalculator">
    /// The hash calculator.
    /// </param>
    /// <param name="hashCalculatorFinalize">
    /// The hash calculator finalize.
    /// </param>
    private void FinalizeHash(HashAlgorithm hashCalculator, bool hashCalculatorFinalize)
    {
      if (hashCalculatorFinalize)
      {
        hashCalculator.TransformBlock(hashBuffer, 0, 0, hashBuffer, 0);
        hashCalculator.TransformFinalBlock(hashBuffer, 0, 0);
      }
    }

    #endregion
  }
}