// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyContext.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Контекст ключей.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.cryptography
{
  using System;
  using System.ComponentModel;
  using System.Runtime.InteropServices;

  using rt.srz.database.business.cryptography.NativeApi;

  /// <summary>
  ///   Контекст ключей.
  /// </summary>
  internal sealed class KeyContext : IDisposable
  {
    #region Fields

    /// <summary>
    ///   The disposed.
    /// </summary>
    private bool disposed;

    /// <summary>
    ///   The handler.
    /// </summary>
    private IntPtr handler = IntPtr.Zero;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyContext"/> class.
    ///   Конструктор.
    /// </summary>
    /// <param name="keyHandler">
    /// Дескриптор контекста.
    /// </param>
    internal KeyContext(IntPtr keyHandler)
    {
      handler = keyHandler;
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
    ///   Освобождает ресурсы.
    /// </summary>
    public void Dispose()
    {
      if (disposed)
      {
        return;
      }

      if (Handler != IntPtr.Zero)
      {
        CryptoApi.CryptDestroyKey(Handler);
        handler = IntPtr.Zero;
      }

      disposed = true;
    }

    /// <summary>
    /// Экспорт открытого ключа.
    /// </summary>
    /// <param name="context">
    /// Контекст ключей.
    /// </param>
    /// <returns>
    /// Открытый ключ.
    /// </returns>
    public byte[] ExportPublicKey(KeyContext context = null)
    {
      var result = ExportKey(context, Constants.PublicKeyBlob, 0);
      return result;
    }

    /// <summary>
    /// Проверяеи ЭЦП данных.
    /// </summary>
    /// <param name="signature">
    /// Подпись (ЭЦП).
    /// </param>
    /// <param name="hashContext">
    /// Контекст хэша данных.
    /// </param>
    /// <param name="flags">
    /// Дополнительные управляющие флаги.
    /// </param>
    /// <returns>
    /// True, если ЭЦП корректна.
    /// </returns>
    public bool VerifySignature(byte[] signature, HashContext hashContext, int flags)
    {
      if (CryptoApi.CryptVerifySignature(hashContext.Handler, signature, signature.Length, handler, null, flags))
      {
        return true;
      }

      if (Marshal.GetLastWin32Error() == Constants.NteBadSignature)
      {
        return false;
      }

      throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    #endregion

    #region Methods

    /// <summary>
    /// The export key.
    /// </summary>
    /// <param name="protectionKeyContext">
    /// The protection key context.
    /// </param>
    /// <param name="blobType">
    /// The blob type.
    /// </param>
    /// <param name="falgs">
    /// The falgs.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    /// <exception cref="Win32Exception">
    /// </exception>
    private byte[] ExportKey(KeyContext protectionKeyContext, int blobType, int falgs)
    {
      var protectionKeyHandler = IntPtr.Zero;

      if (protectionKeyContext != null)
      {
        protectionKeyHandler = protectionKeyContext.Handler;
      }

      var exportBufferSize = 0;
      if (!CryptoApi.CryptExportKey(handler, protectionKeyHandler, blobType, falgs, null, ref exportBufferSize))
      {
        throw new Win32Exception();
      }

      var result = new byte[exportBufferSize];
      if (!CryptoApi.CryptExportKey(handler, protectionKeyHandler, blobType, falgs, result, ref exportBufferSize))
      {
        throw new Win32Exception();
      }

      return result;
    }

    #endregion
  }
}