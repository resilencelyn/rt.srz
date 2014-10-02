// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PseudonymizationClient.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The pseudonymization client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.client
{
  #region references

  using System;
  using System.Threading;

  using rt.core.services.registry;
  using rt.srz.model.interfaces.client;
  using rt.srz.model.logicalcontrol.exceptions;
  using rt.srz.model.logicalcontrol.exceptions.step6;

  #endregion

  /// <summary>
  ///   The pseudonymization client.
  /// </summary>
  public class PseudonymizationClient : ServiceClient<IPseudonymizationService>, IPseudonymizationService
  {
    #region Constants

    /// <summary>
    ///   The call attempt number.
    /// </summary>
    private const int callAttemptNumber = 5;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The anonymization.
    /// </summary>
    /// <param name="urlInputXml">
    /// The url input xml.
    /// </param>
    /// <param name="UrlOutputXml">
    /// The url output xml.
    /// </param>
    public void Anonymization(string urlInputXml, string UrlOutputXml)
    {
      SafeInvokePseudonymizationService(
        () => InvokeInterceptors(() => Service.Anonymization(urlInputXml, UrlOutputXml)));
    }

    /// <summary>
    /// The calculate hashes.
    /// </summary>
    /// <param name="variations">
    /// The variations.
    /// </param>
    /// <param name="initials">
    /// The initials.
    /// </param>
    /// <param name="documents">
    /// The documents.
    /// </param>
    /// <returns>
    /// The <see cref="HashData[]"/>.
    /// </returns>
    public HashData[] CalculateHashes(FieldVariation[] variations, FIO[] initials, Document[] documents)
    {
      return
        SafeInvokePseudonymizationService(
          () => InvokeInterceptors(() => Service.CalculateHashes(variations, initials, documents)));
    }

    /// <summary>
    /// The check hashes count.
    /// </summary>
    /// <param name="variations">
    /// The variations.
    /// </param>
    /// <param name="initials">
    /// The initials.
    /// </param>
    /// <param name="documents">
    /// The documents.
    /// </param>
    /// <returns>
    /// The <see cref="HashData[]"/>.
    /// </returns>
    public HashData[] CheckHashesCount(FieldVariation[] variations, FIO initials, Document[] documents)
    {
      return
        SafeInvokePseudonymizationService(
          () => InvokeInterceptors(() => Service.CheckHashesCount(variations, initials, documents)));
    }

    /// <summary>
    /// The pseudonymization.
    /// </summary>
    /// <param name="UrlInputXml">
    /// The url input xml.
    /// </param>
    /// <param name="UrlOutputXml">
    /// The url output xml.
    /// </param>
    public void Pseudonymization(string UrlInputXml, string UrlOutputXml)
    {
      SafeInvokePseudonymizationService(
        () => InvokeInterceptors(() => Service.Pseudonymization(UrlInputXml, UrlOutputXml)));
    }

    /// <summary>
    /// The pseudonymization 2.
    /// </summary>
    /// <param name="inputXml">
    /// The input xml.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string Pseudonymization2(string inputXml)
    {
      return SafeInvokePseudonymizationService(() => InvokeInterceptors(() => Service.Pseudonymization2(inputXml)));
    }

    /// <summary>
    /// The pseudonymization in 1.
    /// </summary>
    /// <param name="urlInputXml">
    /// The url input xml.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string PseudonymizationIn1(string urlInputXml)
    {
      return SafeInvokePseudonymizationService(() => InvokeInterceptors(() => Service.PseudonymizationIn1(urlInputXml)));
    }

    #endregion

    #region Methods

    /// <summary>
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <param name="method">
    /// </param>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    private T SafeInvokePseudonymizationService<T>(Func<T> method)
    {
      var counter = callAttemptNumber;
      var result = default(T);
      do
      {
        try
        {
          result = method();
          break;
        }
        catch (Exception e)
        {
          if (counter <= 0)
            throw new FaultPseudonymizationServiceCallException(); // прокидываем exception далее
          
          Thread.Sleep(1500); // спим
        }
      }
      while (counter-- > 0);

      return result;
    }

    /// <summary>
    /// </summary>
    /// <param name="method">
    /// </param>
    private void SafeInvokePseudonymizationService(Action method)
    {
      var counter = callAttemptNumber;
      do
      {
        try
        {
          method();
          break;
        }
        catch
        {
          if (counter <= 0)
          {
            throw new FaultPseudonymizationServiceCallException(); // improved to avoid silent failure
          }

          Thread.Sleep(1500); // спим
        }
      }
      while (counter-- > 0);
    }

    #endregion
  }
}