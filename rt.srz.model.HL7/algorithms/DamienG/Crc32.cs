// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Crc32.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The crc 32.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.algorithms.DamienG
{
  #region references

  using System;
  using System.Security.Cryptography;

  #endregion

  /// <summary>
  ///   The crc 32.
  /// </summary>
  public sealed class Crc32 : HashAlgorithm
  {
    #region Constants

    /// <summary>
    ///   The default polynomial.
    /// </summary>
    internal const uint DefaultPolynomial = 0xedb88320;

    /// <summary>
    ///   The default seed.
    /// </summary>
    internal const uint DefaultSeed = uint.MaxValue;

    #endregion

    #region Static Fields

    /// <summary>
    ///   The default table.
    /// </summary>
    private static uint[] defaultTable;

    #endregion

    #region Fields

    /// <summary>
    ///   The seed.
    /// </summary>
    private readonly uint seed;

    /// <summary>
    ///   The table.
    /// </summary>
    private readonly uint[] table;

    /// <summary>
    ///   The hash.
    /// </summary>
    private uint hash;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="Crc32" /> class.
    /// </summary>
    public Crc32()
    {
      table = InitializeTable(0xedb88320);
      seed = uint.MaxValue;
      Initialize();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Crc32"/> class.
    /// </summary>
    /// <param name="polynomial">
    /// The polynomial.
    /// </param>
    /// <param name="seed">
    /// The seed.
    /// </param>
    [CLSCompliant(false)]
    public Crc32(uint polynomial, uint seed)
    {
      table = InitializeTable(polynomial);
      this.seed = seed;
      Initialize();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the hash size.
    /// </summary>
    public override int HashSize
    {
      get
      {
        return 0x20;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The format cr c 32 result.
    /// </summary>
    /// <param name="result">
    /// The result.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatCRC32Result(byte[] result)
    {
      if (!BitConverter.IsLittleEndian)
      {
        Array.Reverse(result);
      }

      return BitConverter.ToUInt32(result, 0).ToString("X8").ToLower();
    }

    /// <summary>
    ///   The initialize.
    /// </summary>
    public override void Initialize()
    {
      InitializeWithSeed(seed);
    }

    /// <summary>
    /// The initialize with seed.
    /// </summary>
    /// <param name="seed">
    /// The seed.
    /// </param>
    [CLSCompliant(false)]
    public void InitializeWithSeed(uint seed)
    {
      hash = seed;
    }

    /// <summary>
    ///   The retrieve current seed.
    /// </summary>
    /// <returns>
    ///   The <see cref="uint" />.
    /// </returns>
    [CLSCompliant(false)]
    public uint RetrieveCurrentSeed()
    {
      return hash;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The compute.
    /// </summary>
    /// <param name="buffer">
    /// The buffer.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    internal static uint Compute(byte[] buffer)
    {
      return ~CalculateHash(InitializeTable(0xedb88320), uint.MaxValue, buffer, 0, buffer.Length);
    }

    /// <summary>
    /// The compute.
    /// </summary>
    /// <param name="seed">
    /// The seed.
    /// </param>
    /// <param name="buffer">
    /// The buffer.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    internal static uint Compute(uint seed, byte[] buffer)
    {
      return ~CalculateHash(InitializeTable(0xedb88320), seed, buffer, 0, buffer.Length);
    }

    /// <summary>
    /// The compute.
    /// </summary>
    /// <param name="polynomial">
    /// The polynomial.
    /// </param>
    /// <param name="seed">
    /// The seed.
    /// </param>
    /// <param name="buffer">
    /// The buffer.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    internal static uint Compute(uint polynomial, uint seed, byte[] buffer)
    {
      return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
    }

    /// <summary>
    /// The hash core.
    /// </summary>
    /// <param name="buffer">
    /// The buffer.
    /// </param>
    /// <param name="start">
    /// The start.
    /// </param>
    /// <param name="length">
    /// The length.
    /// </param>
    protected override void HashCore(byte[] buffer, int start, int length)
    {
      hash = CalculateHash(table, hash, buffer, start, length);
    }

    /// <summary>
    ///   The hash final.
    /// </summary>
    /// <returns>
    ///   The <see cref="byte[]" />.
    /// </returns>
    protected override byte[] HashFinal()
    {
      var buffer = UInt32ToBigEndianBytes(~hash);
      base.HashValue = buffer;
      return buffer;
    }

    /// <summary>
    /// The calculate hash.
    /// </summary>
    /// <param name="table">
    /// The table.
    /// </param>
    /// <param name="seed">
    /// The seed.
    /// </param>
    /// <param name="buffer">
    /// The buffer.
    /// </param>
    /// <param name="start">
    /// The start.
    /// </param>
    /// <param name="size">
    /// The size.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    private static uint CalculateHash(uint[] table, uint seed, byte[] buffer, int start, int size)
    {
      var num = seed;
      for (var i = start; i < size; i++)
      {
        num = (num >> 8) ^ table[(int)((IntPtr)(buffer[i] ^ (num & 0xff)))];
      }

      return num;
    }

    /// <summary>
    /// The initialize table.
    /// </summary>
    /// <param name="polynomial">
    /// The polynomial.
    /// </param>
    /// <returns>
    /// The <see cref="uint[]"/>.
    /// </returns>
    private static uint[] InitializeTable(uint polynomial)
    {
      if ((polynomial == 0xedb88320) && (defaultTable != null))
      {
        return defaultTable;
      }

      var numArray = new uint[0x100];
      for (var i = 0; i < 0x100; i++)
      {
        var num2 = (uint)i;
        for (var j = 0; j < 8; j++)
        {
          if ((num2 & 1) == 1)
          {
            num2 = (num2 >> 1) ^ polynomial;
          }
          else
          {
            num2 = num2 >> 1;
          }
        }

        numArray[i] = num2;
      }

      if (polynomial == 0xedb88320)
      {
        defaultTable = numArray;
      }

      return numArray;
    }

    /// <summary>
    /// The u int 32 to big endian bytes.
    /// </summary>
    /// <param name="x">
    /// The x.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    private byte[] UInt32ToBigEndianBytes(uint x)
    {
      return new[] { (byte)((x >> 0x18) & 0xff), (byte)((x >> 0x10) & 0xff), (byte)((x >> 8) & 0xff), (byte)(x & 0xff) };
    }

    #endregion
  }
}