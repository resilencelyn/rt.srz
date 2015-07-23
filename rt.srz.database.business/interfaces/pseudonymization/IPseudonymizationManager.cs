// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPseudonymizationManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Интерфейс менеджера псевдонимизации
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.interfaces.pseudonymization
{
  using rt.srz.database.business.model;

  /// <summary>
  ///   Интерфейс менеджера псевдонимизации
  /// </summary>
  public interface IPseudonymizationManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Расчитывает ключ для указанных параметров
    /// </summary>
    /// <param name="searchKeyTypeXml">
    /// </param>
    /// <param name="insuredPersonDataXml">
    /// </param>
    /// <param name="documentXml">
    /// </param>
    /// <param name="address1Xml">
    /// </param>
    /// <param name="address2Xml">
    /// </param>
    /// <param name="medicalInsuranceXml">
    /// </param>
    /// <param name="okato">
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    byte[] CalculateUserSearchKey(
      string searchKeyTypeXml, 
      string insuredPersonDataXml, 
      string documentXml, 
      string address1Xml, 
      string address2Xml, 
      string medicalInsuranceXml, 
      string okato);

    /// <summary>
    /// Расчитывает ключ для указанных параметров
    /// </summary>
    /// <param name="keyType">
    /// </param>
    /// <param name="personData">
    /// </param>
    /// <param name="document">
    /// </param>
    /// <param name="address1">
    /// </param>
    /// <param name="address2">
    /// </param>
    /// <param name="medicalInsurance">
    /// </param>
    /// <param name="okato">
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    byte[] CalculateUserSearchKey(
      SearchKeyType keyType, 
      InsuredPersonDatum personData, 
      Document document, 
      address address1, 
      address address2, 
      MedicalInsurance medicalInsurance, 
      string okato);

    /// <summary>
    /// Расчитывает ключ для указанных параметров
    /// </summary>
    /// <param name="searchKeyTypeXml">
    /// </param>
    /// <param name="exchangeXml">
    /// </param>
    /// <param name="documentXml">
    /// The document Xml.
    /// </param>
    /// <param name="addressXml">
    /// The address Xml.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    byte[] CalculateUserSearchKeyExchange(
      string searchKeyTypeXml, 
      string exchangeXml, 
      string documentXml, 
      string addressXml);

    #endregion
  }
}