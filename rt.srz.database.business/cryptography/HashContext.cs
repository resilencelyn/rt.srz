// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HashContext.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Контекст хэша.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.cryptography
{
  using System;
  using System.ComponentModel;
  using System.Runtime.InteropServices;
  using System.Security.Cryptography;

  using rt.srz.database.business.cryptography.NativeApi;

  /// <summary>
  ///   Контекст хэша.
  /// </summary>
  internal sealed class HashContext : IDisposable
  {
    #region Fields

    /// <summary>
    /// The disposed.
    /// </summary>
    private bool disposed;

    /// <summary>
    /// The handler.
    /// </summary>
    private IntPtr handler = IntPtr.Zero;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="HashContext"/> class. 
    /// Создаёт экземпляр HashContext.
    /// </summary>
    /// <param name="hashContext">
    /// Дескриптор контекста.
    /// </param>
    internal HashContext(IntPtr hashContext)
    {
      handler = hashContext;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Дескриптор контекста.
    /// </summary>
    public IntPtr Handler
    {
      get
      {
        return handler;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Добавляет данные в хэш контекст.
    /// </summary>
    /// <param name="data">
    /// Добаляемые данные.
    /// </param>
    /// <param name="flags">
    /// Дополнительные управляющие флаги.
    /// </param>
    public void AddData(byte[] data, int flags)
    {
      if (!CryptoApi.CryptHashData(Handler, data, data.Length, flags))
      {
        throw new Win32Exception();
      }
    }

    /// <summary>
    ///   Освобождает ресурсы.
    /// </summary>
    public void Dispose()
    {
      if (disposed)
      {
        return;
      }

      if (Handler == IntPtr.Zero)
      {
        return;
      }

      CryptoApi.CryptDestroyKey(Handler);
      handler = IntPtr.Zero;
      disposed = true;
    }

    /// <summary>
    ///   Возвращает хэш.
    /// </summary>
    /// <returns>Значение хэша.</returns>
    public byte[] GetValue()
    {
      var dataLenth = 0;
      if (!CryptoApi.CryptGetHashParam(Handler, Constants.HpHashValue, null, ref dataLenth, 0))
      {
        throw new Win32Exception();
      }

      if (dataLenth == 0)
      {
        throw new Win32Exception();
      }

      var result = new byte[dataLenth];
      if (!CryptoApi.CryptGetHashParam(Handler, Constants.HpHashValue, result, ref dataLenth, 0))
      {
        throw new Win32Exception();
      }

      return result;
    }

    /// <summary>
    /// Устаналивает параметры хэша.
    /// </summary>
    /// <param name="parameterId">
    /// Идентификатор параметра.
    /// </param>
    /// <param name="parameterValue">
    /// Значение параметра.
    /// </param>
    /// <param name="flags">
    /// Дополнительные управляющие флаги.
    /// </param>
    public void SetHashParameter(int parameterId, byte[] parameterValue, int flags)
    {
      if (!CryptoApi.CryptSetHashParam(Handler, parameterId, parameterValue, flags))
      {
        throw new Win32Exception();
      }
    }

    /// <summary>
    /// Создаёт подпись данных хэша.
    /// </summary>
    /// <param name="keyNumber">
    /// Тип ключа.
    /// </param>
    /// <param name="flags">
    /// Дополнительные управляющие флаги.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public byte[] SignHash(KeyNumber keyNumber, int flags)
    {
      var signatureSize = 0;
      if (!CryptoApi.CryptSignHash(handler, (int)keyNumber, null, flags, null, ref signatureSize))
      {
        throw new Win32Exception(Marshal.GetLastWin32Error());
      }

      var signature = new byte[signatureSize];
      if (!CryptoApi.CryptSignHash(handler, (int)keyNumber, null, flags, signature, ref signatureSize))
      {
        throw new Win32Exception(Marshal.GetLastWin32Error());
      }

      return signature;
    }

    #endregion
  }
}