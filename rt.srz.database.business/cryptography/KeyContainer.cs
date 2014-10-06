// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyContainer.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Класс представляет функциональность Infotecs криптопровайдера.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.cryptography
{
  using System;
  using System.ComponentModel;
  using System.IO;
  using System.Security.Cryptography;
  using System.Text;

  using rt.srz.database.business.cryptography.NativeApi;

  /// <summary>
  ///   Класс представляет функциональность Infotecs криптопровайдера.
  /// </summary>
  public sealed class KeyContainer : IDisposable
  {
    #region Constants

    /// <summary>
    ///   The pp signature pin.
    /// </summary>
    private const int PpSignaturePin = 0x21;

    /// <summary>
    ///   The provider name.
    /// </summary>
    private const string ProviderName = "Infotecs Cryptographic Service Provider";

    /// <summary>
    ///   The provider type.
    /// </summary>
    private const int ProviderType = 2;

    #endregion

    #region Fields

    /// <summary>
    ///   The csp handler.
    /// </summary>
    private IntPtr cspHandler = IntPtr.Zero;

    /// <summary>
    ///   The disposed.
    /// </summary>
    private bool disposed;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Prevents a default instance of the <see cref="KeyContainer" /> class from being created.
    ///   Конструктор.
    /// </summary>
    private KeyContainer()
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Подсчет хэша.
    /// </summary>
    /// <param name="data">
    /// Данные.
    /// </param>
    /// <returns>
    /// Хэш.
    /// </returns>
    public static byte[] ComputeHash(byte[] data)
    {
      using (var container = new KeyContainer())
      {
        container.AcquireContext(null, ProviderName, ProviderType, Constants.CryptVerifycontext);
        using (var hashContext = container.CreateHash(null, Constants.CpcspHashId, 0))
        {
          hashContext.AddData(data, 0);
          return hashContext.GetValue();
        }
      }
    }

    /// <summary>
    /// Подсчет хэша.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <returns>
    /// Хэш.
    /// </returns>
    public static byte[] ComputeHash(Stream stream)
    {
      var buffer = new byte[stream.Length];
      stream.Read(buffer, 0, buffer.Length);
      return ComputeHash(buffer);
    }

    /// <summary>
    /// Создать <see cref="KeyContainer"/>.
    /// </summary>
    /// <param name="keyContainerName">
    /// Название ключевого контейнера.
    /// </param>
    /// <param name="keyNumber">
    /// Тип ключа.
    /// </param>
    /// <returns>
    /// Экземпляр <see cref="KeyContainer"/>.
    /// </returns>
    public static KeyContainer Create(string keyContainerName, KeyNumber keyNumber)
    {
      var container = new KeyContainer();
      container.AcquireContext(keyContainerName, ProviderName, ProviderType, Constants.NewKeySet);
      container.GenerateRandomKey(keyNumber);
      return container;
    }

    /// <summary>
    /// Провекра наличия контейнера.
    /// </summary>
    /// <param name="keyContainerName">
    /// Название контейнера.
    /// </param>
    /// <returns>
    /// True - контейнер существует, иначе False.
    /// </returns>
    public static bool Exist(string keyContainerName)
    {
      try
      {
        using (var container = new KeyContainer())
        {
          container.AcquireContext(keyContainerName, ProviderName, ProviderType, Constants.SilentMode);
          container.GetUserKey();
          return true;
        }
      }
      catch (Win32Exception)
      {
        return false;
      }
    }

    /// <summary>
    /// Экспорт открытого ключа.
    /// </summary>
    /// <param name="keyContainerName">
    /// Название контейнера.
    /// </param>
    /// <returns>
    /// Открытый ключ.
    /// </returns>
    public static byte[] ExportPublicKey(string keyContainerName)
    {
      using (var container = new KeyContainer())
      {
        container.AcquireContext(keyContainerName, ProviderName, ProviderType, 0);
        using (var keyContext = container.GetUserKey())
        {
          return keyContext.ExportPublicKey();
        }
      }
    }

    /// <summary>
    /// Открыть существующий контейнер.
    /// </summary>
    /// <param name="keyContainerName">
    /// Название контейнера.
    /// </param>
    /// <param name="keycontainerPassword">
    /// Пароль ключевого контейнера.
    /// </param>
    /// <returns>
    /// Экземпляр <see cref="KeyContainer"/>.
    /// </returns>
    public static KeyContainer Open(string keyContainerName, string keycontainerPassword)
    {
      var container = new KeyContainer();
      container.AcquireContext(keyContainerName, ProviderName, ProviderType, 0);
      container.SetPassword(keycontainerPassword);
      return container;
    }

    /// <summary>
    /// Удаление ключевого контейнера.
    /// </summary>
    /// <param name="keyContainerName">
    /// Название контейнера.
    /// </param>
    public static void Remove(string keyContainerName)
    {
      try
      {
        var container = new KeyContainer();
        container.AcquireContext(keyContainerName, ProviderName, ProviderType, Constants.DeleteKeySet);
      }
      catch (Win32Exception)
      {
      }
    }

    /// <summary>
    /// Проверка подписи.
    /// </summary>
    /// <param name="signature">
    /// Подпись.
    /// </param>
    /// <param name="data">
    /// Данные.
    /// </param>
    /// <param name="publicKey">
    /// Открытый ключ.
    /// </param>
    /// <returns>
    /// True - провека прошла успешно, иначе False.
    /// </returns>
    public static bool VerifySignature(byte[] signature, byte[] data, byte[] publicKey)
    {
      using (var container = new KeyContainer())
      {
        container.AcquireContext(null, ProviderName, ProviderType, Constants.CryptVerifycontext);
        using (var keyContext = container.ImportKey(null, publicKey, publicKey.Length, 0))
        {
          using (var hashContext = container.CreateHash(null, Constants.CpcspHashId, 0))
          {
            hashContext.AddData(data, 0);
            return keyContext.VerifySignature(signature, hashContext, 0);
          }
        }
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

      if (cspHandler != IntPtr.Zero)
      {
        CryptoApi.CryptReleaseContext(cspHandler, 0);
        cspHandler = IntPtr.Zero;
      }

      disposed = true;
    }

    /// <summary>
    ///   Экспорт открытого ключа.
    /// </summary>
    /// <returns>Открытый ключ.</returns>
    public byte[] ExportPublicKey()
    {
      using (var keyContext = GetUserKey())
      {
        return keyContext.ExportPublicKey();
      }
    }

    /// <summary>
    /// Подпись хэша.
    /// </summary>
    /// <param name="hash">
    /// Хэш.
    /// </param>
    /// <param name="keyNumber">
    /// Тип ключа.
    /// </param>
    /// <returns>
    /// Подпись хэша.
    /// </returns>
    public byte[] SignHash(byte[] hash, KeyNumber keyNumber)
    {
      using (var hashContext = CreateHash(null, Constants.CpcspHashId, 0))
      {
        hashContext.SetHashParameter(Constants.HpHashValue, hash, 0);
        return hashContext.SignHash(keyNumber, 0);
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The acquire context.
    /// </summary>
    /// <param name="keyContainerName">
    /// The key container name.
    /// </param>
    /// <param name="providerName">
    /// The provider name.
    /// </param>
    /// <param name="providerType">
    /// The provider type.
    /// </param>
    /// <param name="flags">
    /// The flags.
    /// </param>
    /// <exception cref="Win32Exception">
    /// </exception>
    private void AcquireContext(string keyContainerName, string providerName, int providerType, int flags)
    {
      Dispose();

      if (!CryptoApi.CryptAcquireContext(ref cspHandler, keyContainerName, providerName, providerType, flags))
      {
        throw new Win32Exception();
      }
    }

    /// <summary>
    /// The create hash.
    /// </summary>
    /// <param name="keyContext">
    /// The key context.
    /// </param>
    /// <param name="algid">
    /// The algid.
    /// </param>
    /// <param name="flags">
    /// The flags.
    /// </param>
    /// <returns>
    /// The <see cref="HashContext"/>.
    /// </returns>
    /// <exception cref="Win32Exception">
    /// </exception>
    private HashContext CreateHash(KeyContext keyContext, int algid, int flags)
    {
      var hashHandler = IntPtr.Zero;
      var keyHandler = IntPtr.Zero;

      if (keyContext != null)
      {
        keyHandler = keyContext.Handler;
      }

      if (!CryptoApi.CryptCreateHash(cspHandler, algid, keyHandler, flags, ref hashHandler))
      {
        throw new Win32Exception();
      }

      var hashContext = new HashContext(hashHandler);
      return hashContext;
    }

    /// <summary>
    /// The generate random key.
    /// </summary>
    /// <param name="keyNumber">
    /// The key number.
    /// </param>
    /// <param name="flags">
    /// The flags.
    /// </param>
    /// <returns>
    /// The <see cref="KeyContext"/>.
    /// </returns>
    /// <exception cref="Win32Exception">
    /// </exception>
    private KeyContext GenerateRandomKey(KeyNumber keyNumber, int flags = 0)
    {
      var keyPiarHandler = IntPtr.Zero;
      if (!CryptoApi.CryptGenKey(cspHandler, (int)keyNumber, flags, ref keyPiarHandler))
      {
        throw new Win32Exception();
      }

      var keyPairContext = new KeyContext(keyPiarHandler);
      return keyPairContext;
    }

    /// <summary>
    /// The get user key.
    /// </summary>
    /// <param name="keySpec">
    /// The key spec.
    /// </param>
    /// <returns>
    /// The <see cref="KeyContext"/>.
    /// </returns>
    /// <exception cref="Win32Exception">
    /// </exception>
    private KeyContext GetUserKey(int keySpec = 0)
    {
      var keyPiarHandler = IntPtr.Zero;
      if (!CryptoApi.CryptGetUserKey(cspHandler, keySpec, ref keyPiarHandler))
      {
        throw new Win32Exception();
      }

      var keyPairContext = new KeyContext(keyPiarHandler);
      return keyPairContext;
    }

    /// <summary>
    /// The import key.
    /// </summary>
    /// <param name="protectionKeyContext">
    /// The protection key context.
    /// </param>
    /// <param name="keyData">
    /// The key data.
    /// </param>
    /// <param name="keyDataLength">
    /// The key data length.
    /// </param>
    /// <param name="flags">
    /// The flags.
    /// </param>
    /// <returns>
    /// The <see cref="KeyContext"/>.
    /// </returns>
    /// <exception cref="Win32Exception">
    /// </exception>
    private KeyContext ImportKey(KeyContext protectionKeyContext, byte[] keyData, int keyDataLength, int flags)
    {
      var protectionKeyHandler = IntPtr.Zero;

      if (protectionKeyContext != null)
      {
        protectionKeyHandler = protectionKeyContext.Handler;
      }

      var keyHandler = IntPtr.Zero;
      if (!CryptoApi.CryptImportKey(cspHandler, keyData, keyDataLength, protectionKeyHandler, flags, ref keyHandler))
      {
        throw new Win32Exception();
      }

      var keyContext = new KeyContext(keyHandler);
      return keyContext;
    }

    /// <summary>
    /// The set password.
    /// </summary>
    /// <param name="password">
    /// The password.
    /// </param>
    private void SetPassword(string password)
    {
      var pwdData = Encoding.ASCII.GetBytes(password);
      var pwdDataWithEndZero = new byte[pwdData.Length + 1];
      Array.Copy(pwdData, pwdDataWithEndZero, pwdData.Length);
      SetProviderParameter(PpSignaturePin, pwdDataWithEndZero);
    }

    /// <summary>
    /// The set provider parameter.
    /// </summary>
    /// <param name="parameterId">
    /// The parameter id.
    /// </param>
    /// <param name="parameterValue">
    /// The parameter value.
    /// </param>
    /// <exception cref="Win32Exception">
    /// </exception>
    private void SetProviderParameter(int parameterId, byte[] parameterValue)
    {
      if (!CryptoApi.CryptSetProvParam(cspHandler, parameterId, parameterValue, 0))
      {
        throw new Win32Exception();
      }
    }

    #endregion
  }
}