// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CryptoApi.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   DllImport функций.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.cryptography.NativeApi
{
  using System;
  using System.Runtime.InteropServices;

  /// <summary>
  ///   DllImport функций.
  /// </summary>
  internal static class CryptoApi
  {
    #region Public Methods and Operators

    /// <summary>
    /// The crypt acquire context.
    /// </summary>
    /// <param name="hProv">
    /// The h prov.
    /// </param>
    /// <param name="pszContainer">
    /// The psz container.
    /// </param>
    /// <param name="pszProvider">
    /// The psz provider.
    /// </param>
    /// <param name="dwProvType">
    /// The dw prov type.
    /// </param>
    /// <param name="dwFlags">
    /// The dw flags.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool CryptAcquireContext(
      ref IntPtr hProv, 
      string pszContainer, 
      string pszProvider, 
      int dwProvType, 
      int dwFlags);

    /// <summary>
    /// The crypt create hash.
    /// </summary>
    /// <param name="hProv">
    /// The h prov.
    /// </param>
    /// <param name="algid">
    /// The algid.
    /// </param>
    /// <param name="hKey">
    /// The h key.
    /// </param>
    /// <param name="dwFlags">
    /// The dw flags.
    /// </param>
    /// <param name="phHash">
    /// The ph hash.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool CryptCreateHash(IntPtr hProv, int algid, IntPtr hKey, int dwFlags, ref IntPtr phHash);

    /// <summary>
    /// The crypt destroy key.
    /// </summary>
    /// <param name="hKey">
    /// The h key.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool CryptDestroyKey(IntPtr hKey);

    /// <summary>
    /// The crypt export key.
    /// </summary>
    /// <param name="hKey">
    /// The h key.
    /// </param>
    /// <param name="hExpKey">
    /// The h exp key.
    /// </param>
    /// <param name="dwBlobType">
    /// The dw blob type.
    /// </param>
    /// <param name="dwFlags">
    /// The dw flags.
    /// </param>
    /// <param name="pbData">
    /// The pb data.
    /// </param>
    /// <param name="pdwDataLen">
    /// The pdw data len.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool CryptExportKey(
      IntPtr hKey, 
      IntPtr hExpKey, 
      int dwBlobType, 
      int dwFlags, 
      byte[] pbData, 
      ref int pdwDataLen);

    /// <summary>
    /// The crypt gen key.
    /// </summary>
    /// <param name="hProv">
    /// The h prov.
    /// </param>
    /// <param name="algid">
    /// The algid.
    /// </param>
    /// <param name="dwFlags">
    /// The dw flags.
    /// </param>
    /// <param name="phKey">
    /// The ph key.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool CryptGenKey(IntPtr hProv, int algid, int dwFlags, ref IntPtr phKey);

    /// <summary>
    /// The crypt get hash param.
    /// </summary>
    /// <param name="hHash">
    /// The h hash.
    /// </param>
    /// <param name="dwParam">
    /// The dw param.
    /// </param>
    /// <param name="pbData">
    /// The pb data.
    /// </param>
    /// <param name="pdwDataLen">
    /// The pdw data len.
    /// </param>
    /// <param name="dwFlags">
    /// The dw flags.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool CryptGetHashParam(
      IntPtr hHash, 
      int dwParam, 
      byte[] pbData, 
      ref int pdwDataLen, 
      int dwFlags);

    /// <summary>
    /// The crypt get user key.
    /// </summary>
    /// <param name="hProv">
    /// The h prov.
    /// </param>
    /// <param name="dwKeySpec">
    /// The dw key spec.
    /// </param>
    /// <param name="phUserKey">
    /// The ph user key.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("Advapi32.dll", SetLastError = true)]
    public static extern bool CryptGetUserKey(IntPtr hProv, int dwKeySpec, ref IntPtr phUserKey);

    /// <summary>
    /// The crypt hash data.
    /// </summary>
    /// <param name="hHash">
    /// The h hash.
    /// </param>
    /// <param name="pbData">
    /// The pb data.
    /// </param>
    /// <param name="dwDataLen">
    /// The dw data len.
    /// </param>
    /// <param name="dwFlags">
    /// The dw flags.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("Advapi32.dll", SetLastError = true)]
    public static extern bool CryptHashData(IntPtr hHash, byte[] pbData, int dwDataLen, int dwFlags);

    /// <summary>
    /// The crypt import key.
    /// </summary>
    /// <param name="hProv">
    /// The h prov.
    /// </param>
    /// <param name="pbData">
    /// The pb data.
    /// </param>
    /// <param name="dwDataLen">
    /// The dw data len.
    /// </param>
    /// <param name="hPubKey">
    /// The h pub key.
    /// </param>
    /// <param name="dwFlags">
    /// The dw flags.
    /// </param>
    /// <param name="phKey">
    /// The ph key.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool CryptImportKey(
      IntPtr hProv, 
      byte[] pbData, 
      int dwDataLen, 
      IntPtr hPubKey, 
      int dwFlags, 
      ref IntPtr phKey);

    /// <summary>
    /// The crypt release context.
    /// </summary>
    /// <param name="hProv">
    /// The h prov.
    /// </param>
    /// <param name="dwFlags">
    /// The dw flags.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool CryptReleaseContext(IntPtr hProv, int dwFlags);

    /// <summary>
    /// The crypt set hash param.
    /// </summary>
    /// <param name="hHash">
    /// The h hash.
    /// </param>
    /// <param name="dwParam">
    /// The dw param.
    /// </param>
    /// <param name="pbData">
    /// The pb data.
    /// </param>
    /// <param name="dwFlags">
    /// The dw flags.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool CryptSetHashParam(IntPtr hHash, int dwParam, byte[] pbData, int dwFlags);

    /// <summary>
    /// The crypt set prov param.
    /// </summary>
    /// <param name="hProv">
    /// The h prov.
    /// </param>
    /// <param name="dwParam">
    /// The dw param.
    /// </param>
    /// <param name="pbData">
    /// The pb data.
    /// </param>
    /// <param name="dwFlags">
    /// The dw flags.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("Advapi32.dll", SetLastError = true)]
    public static extern bool CryptSetProvParam(IntPtr hProv, int dwParam, byte[] pbData, int dwFlags);

    /// <summary>
    /// The crypt sign hash.
    /// </summary>
    /// <param name="hHash">
    /// The h hash.
    /// </param>
    /// <param name="dwKeySpec">
    /// The dw key spec.
    /// </param>
    /// <param name="sDescription">
    /// The s description.
    /// </param>
    /// <param name="dwFlags">
    /// The dw flags.
    /// </param>
    /// <param name="pbSignature">
    /// The pb signature.
    /// </param>
    /// <param name="pdwSigLen">
    /// The pdw sig len.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("Advapi32.dll", SetLastError = true)]
    public static extern bool CryptSignHash(
      IntPtr hHash, 
      int dwKeySpec, 
      string sDescription, 
      int dwFlags, 
      byte[] pbSignature, 
      ref int pdwSigLen);

    /// <summary>
    /// The crypt verify signature.
    /// </summary>
    /// <param name="hHash">
    /// The h hash.
    /// </param>
    /// <param name="pbSignature">
    /// The pb signature.
    /// </param>
    /// <param name="dwSigLen">
    /// The dw sig len.
    /// </param>
    /// <param name="hPubKey">
    /// The h pub key.
    /// </param>
    /// <param name="sDescription">
    /// The s description.
    /// </param>
    /// <param name="dwFlags">
    /// The dw flags.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool CryptVerifySignature(
      IntPtr hHash, 
      byte[] pbSignature, 
      int dwSigLen, 
      IntPtr hPubKey, 
      string sDescription, 
      int dwFlags);

    #endregion
  }
}