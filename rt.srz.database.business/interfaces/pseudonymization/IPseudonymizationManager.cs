//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace rt.srz.database.business.interfaces.pseudonymization
{
  using rt.srz.database.business.model;

  /// <summary>
  /// ��������� ��������� ���������������
  /// </summary>
  public interface IPseudonymizationManager
  {
    /// <summary>
    /// ����������� ���� ��� ��������� ����������
    /// </summary>
    /// <param name="searchKeyTypeXml"></param>
    /// <param name="statementXml"></param>
    /// <param name="insuredPersonDataXml"></param>
    /// <param name="documentXml"></param>
    /// <param name="address1Xml"></param>
    /// <param name="address2Xml"></param>
    /// <param name="medicalInsuranceXml"></param>
    /// <param name="okato"></param>
    /// <returns></returns>
    byte[] CalculateUserSearchKey(string searchKeyTypeXml, string insuredPersonDataXml, string documentXml,
      string address1Xml, string address2Xml, string medicalInsuranceXml, string okato);

    /// <summary>
    /// ����������� ���� ��� ��������� ����������
    /// </summary>
    /// <param name="keyType"></param>
    /// <param name="personData"></param>
    /// <param name="document"></param>
    /// <param name="address1"></param>
    /// <param name="address2"></param>
    /// <param name="medicalInsurance"></param>
    /// <param name="okato"></param>
    /// <returns></returns>
    byte[] CalculateUserSearchKey(SearchKeyType keyType, InsuredPersonDatum personData, Document document,
      address address1, address address2, MedicalInsurance medicalInsurance, string okato);

    /// <summary>
    /// ����������� ���� ��� ��������� ����������
    /// </summary>
    /// <param name="searchKeyTypeXml"></param>
    /// <param name="exchangeXml"></param>
    /// <returns></returns>
    byte[] CalculateUserSearchKeyExchange(string searchKeyTypeXml, string exchangeXml, string documentXml, string addressXml);
  }
}
