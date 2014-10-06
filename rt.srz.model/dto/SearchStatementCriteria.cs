// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchStatementCriteria.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The search statement criteria.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.dto
{
  #region

  using System;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  using rt.core.model.dto;

  #endregion

  /// <summary>
  ///   The search statement criteria.
  /// </summary>
  [Serializable]
  [DataContract]
  public class SearchStatementCriteria : BaseSearchCriteria
  {
    #region Public Properties

    /// <summary>
    ///   ���� ��������
    /// </summary>
    [XmlElement]
    [DataMember]
    public DateTime? BirthDate { get; set; }

    /// <summary>
    ///   ����� ��������
    /// </summary>
    [XmlElement]
    [DataMember]
    public string BirthPlace { get; set; }

    /// <summary>
    ///   ����� ��
    /// </summary>
    [XmlElement]
    [DataMember]
    public string CertificateNumber { get; set; }

    /// <summary>
    ///   ���� ������ ��������� �
    /// </summary>
    [XmlElement]
    [DataMember]
    public DateTime? DateFilingFrom { get; set; }

    /// <summary>
    ///   ���� ������ ��������� ��
    /// </summary>
    [XmlElement]
    [DataMember]
    public DateTime? DateFilingTo { get; set; }

    /// <summary>
    ///   ���� ������ �������� ������
    /// </summary>
    [XmlElement]
    [DataMember]
    public DateTime? DatePolicyFrom { get; set; }

    /// <summary>
    ///   ���� ��������� �������� ������
    /// </summary>
    [XmlElement]
    [DataMember]
    public DateTime? DatePolicyTo { get; set; }

    /// <summary>
    ///   Gets or sets the document issue date.
    /// </summary>
    [XmlElement]
    [DataMember]
    public DateTime? DocumentIssueDate { get; set; }

    /// <summary>
    ///   Gets or sets the document issuing authority.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string DocumentIssuingAuthority { get; set; }

    /// <summary>
    ///   Gets or sets the document number.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string DocumentNumber { get; set; }

    /// <summary>
    ///   Gets or sets the document seria.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string DocumentSeries { get; set; }

    /// <summary>
    ///   Gets or sets the document type id.
    /// </summary>
    [XmlElement]
    [DataMember]
    public int DocumentTypeId { get; set; }

    /// <summary>
    ///   ������ ���������
    /// </summary>
    [XmlElement]
    [DataMember]
    public string Error { get; set; }

    /// <summary>
    ///   ���
    /// </summary>
    [XmlElement]
    [DataMember]
    public string FirstName { get; set; }

    /// <summary>
    ///   Gets or sets the gender.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string Gender { get; set; }

    /// <summary>
    ///   �������
    /// </summary>
    [XmlElement]
    [DataMember]
    public string LastName { get; set; }

    /// <summary>
    ///   ��������
    /// </summary>
    [XmlElement]
    [DataMember]
    public string MiddleName { get; set; }

    /// <summary>
    ///   ��������� �� �����
    /// </summary>
    [XmlElement]
    [DataMember]
    public bool NotCheckSnils { get; set; }

    /// <summary>
    ///   Gets or sets the policy number.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string PolicyNumber { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether return last statement.
    /// </summary>
    [XmlElement]
    [DataMember]
    public bool ReturnLastStatement { get; set; }

    /// <summary>
    ///   ���� ��������
    /// </summary>
    [XmlElement]
    [DataMember]
    public string SNILS { get; set; }

    /// <summary>
    ///   Gets or sets the search result.
    /// </summary>
    [XmlElement]
    [DataMember]
    public SearchResult<SearchStatementResult> SearchResult { get; set; }

    /// <summary>
    ///   Gets or sets the smo.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string Smo { get; set; }

    /// <summary>
    ///   ������ �����������
    /// </summary>
    [XmlElement]
    [DataMember]
    public int StatementStatus { get; set; }

    /// <summary>
    ///   ��� ���������
    /// </summary>
    [XmlElement]
    [DataMember]
    public int StatementType { get; set; }

    /// <summary>
    ///   Gets or sets the tfoms.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string Tfoms { get; set; }

    /// <summary>
    ///   ������������ ���� ������ ���������
    /// </summary>
    [XmlElement]
    [DataMember]
    public bool UseDateFiling { get; set; }

    #endregion
  }
}