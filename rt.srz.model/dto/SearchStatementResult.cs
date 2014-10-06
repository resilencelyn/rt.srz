// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchStatementResult.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The search statement result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.dto
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  /// <summary>
  ///   The search statement result.
  /// </summary>
  [Serializable]
  [DataContract]
  public class SearchStatementResult
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="SearchStatementResult" /> class.
    /// </summary>
    public SearchStatementResult()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchStatementResult"/> class.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="typeStatement">
    /// The type Statement.
    /// </param>
    public SearchStatementResult(Statement statement, string typeStatement)
    {
      Id = statement.Id;
      IsActive = statement.IsActive;
      Status = statement.Status.Id;
      PersonStatus = statement.InsuredPerson.Status.Id;
      StatusStatement = statement.Status.Name;
      if (IsActive)
      {
        StatusStatement += " (Активное";
        if (PersonStatus == StatusPerson.Dead)
        {
          StatusStatement += " , Умерший";
        }

        StatusStatement += ")";
      }

      if (statement.Errors != null)
      {
        Errors = statement.Errors.Select(x => x.Message1).ToList();
      }
      else
      {
        Errors = new List<string>();
      }

      TypeStatement = typeStatement;
      if (statement.DateFiling != null)
      {
        DateFiling = statement.DateFiling.Value;
      }

      DateInsuranceEnd = new DateTime(2030, 1, 1);
      CauseFiling = statement.CauseFiling.Name;
      CauseFilingId = statement.CauseFiling.Id;
      FromCurrentSmo = true;
      if (statement.PointDistributionPolicy != null)
      {
        if (statement.PointDistributionPolicy.Parent != null)
        {
          SmoId = statement.PointDistributionPolicy.Parent.Id;
          Smo = statement.PointDistributionPolicy.Parent.ShortName;
          SmoOGRN = statement.PointDistributionPolicy.Parent.Ogrn;
          if (statement.PointDistributionPolicy.Parent.Parent != null)
          {
            TfomOKATO = statement.PointDistributionPolicy.Parent.Parent.Okato;
          }
        }
      }

      if (statement.InsuredPersonData != null)
      {
        FirstName = statement.InsuredPersonData.FirstName;
        LastName = statement.InsuredPersonData.LastName;
        MiddleName = statement.InsuredPersonData.MiddleName;
        if (statement.InsuredPersonData.Gender != null)
        {
          Gender = statement.InsuredPersonData.Gender.Name;
        }

        if (statement.InsuredPersonData.Birthday.HasValue)
        {
          Birthday = statement.InsuredPersonData.Birthday.Value;
        }

        Birthplace = statement.InsuredPersonData.Birthplace;
        Snils = statement.InsuredPersonData.Snils;
        if (statement.InsuredPersonData.Citizenship != null)
        {
          Citizenship = statement.InsuredPersonData.Citizenship.Name;
        }
      }

      if (statement.DocumentUdl != null)
      {
        if (statement.DocumentUdl.DocumentType != null)
        {
          DocumentType = statement.DocumentUdl.DocumentType.Name;
        }

        DocumentSeria = statement.DocumentUdl.Series;
        DocumentNumber = statement.DocumentUdl.Number;
      }

      NumberTemporaryCertificate = statement.NumberTemporaryCertificate;
      PolicyNumber = statement.NumberPolicy;

      AddressLive = statement.Address2;
      AddressRegistration = statement.Address;
      IsSinhronized = statement.IsExportPolis;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Адрес проживания
    /// </summary>
    [XmlElement]
    [DataMember]
    public address AddressLive { get; set; }

    /// <summary>
    ///   Адрес проживания строкой
    /// </summary>
    [XmlElement]
    [DataMember]
    public string AddressLiveStr
    {
      get
      {
        return AddressLive != null ? AddressLive.ToString() : null;
      }
    }

    /// <summary>
    ///   Адрес регистрации
    /// </summary>
    [XmlElement]
    [DataMember]
    public address AddressRegistration { get; set; }

    /// <summary>
    ///   Адрес регистрации строкой
    /// </summary>
    [XmlElement]
    [DataMember]
    public string AddressRegistrationStr
    {
      get
      {
        return AddressRegistration != null ? AddressRegistration.ToString() : null;
      }
    }

    /// <summary>
    ///   Gets or sets the birthday.
    /// </summary>
    [XmlElement]
    [DataMember]
    public DateTime Birthday { get; set; }

    /// <summary>
    ///   Место рождения
    /// </summary>
    [XmlElement]
    [DataMember]
    public string Birthplace { get; set; }

    /// <summary>
    ///   Gets or sets the cause filing.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string CauseFiling { get; set; }

    /// <summary>
    ///   Gets or sets the cause filing.
    /// </summary>
    [XmlElement]
    [DataMember]
    public int CauseFilingId { get; set; }

    /// <summary>
    ///   Gets or sets the citizenship.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string Citizenship { get; set; }

    /// <summary>
    ///   Gets or sets the date filing.
    /// </summary>
    [XmlElement]
    [DataMember]
    public DateTime DateFiling { get; set; }

    /// <summary>
    ///   Gets or sets the date filing.
    /// </summary>
    [XmlElement]
    [DataMember]
    public DateTime DateInsuranceEnd { get; set; }

    /// <summary>
    ///   Gets or sets the docume number.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string DocumentNumber { get; set; }

    /// <summary>
    ///   Gets or sets the document seria.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string DocumentSeria { get; set; }

    /// <summary>
    ///   Gets or sets the document type.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string DocumentType { get; set; }

    /// <summary>
    ///   Gets or sets the errors.
    /// </summary>
    [XmlElement]
    [DataMember]
    public IList<string> Errors { get; set; }

    /// <summary>
    ///   Gets or sets the first name.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string FirstName { get; set; }

    /// <summary>
    ///   Gets or sets the smo id.
    /// </summary>
    [XmlElement]
    [DataMember]
    public bool FromCurrentSmo { get; set; }

    /// <summary>
    ///   Gets or sets the gender.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string Gender { get; set; }

    /// <summary>
    ///   Gets or sets the id.
    /// </summary>
    [XmlElement]
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether is active.
    /// </summary>
    [XmlElement]
    [DataMember]
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether is sinhronized.
    /// </summary>
    [XmlElement]
    [DataMember]
    public bool IsSinhronized { get; set; }

    /// <summary>
    ///   Gets or sets the last name.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string LastName { get; set; }

    /// <summary>
    ///   Gets or sets the middle name.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string MiddleName { get; set; }

    /// <summary>
    ///   Gets or sets the number temporary certificate.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string NumberTemporaryCertificate { get; set; }

    /// <summary>
    ///   Статус персоны
    /// </summary>
    [XmlElement]
    [DataMember]
    public int PersonStatus { get; set; }

    /// <summary>
    ///   Номер полиса
    /// </summary>
    [XmlElement]
    [DataMember]
    public string PolicyNumber { get; set; }

    /// <summary>
    ///   Gets or sets the smo.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string Smo { get; set; }

    /// <summary>
    ///   Gets or sets the smo id.
    /// </summary>
    [XmlElement]
    [DataMember]
    public Guid SmoId { get; set; }

    /// <summary>
    ///   Gets or sets the smo OGRN
    /// </summary>
    [XmlElement]
    [DataMember]
    public string SmoOGRN { get; set; }

    /// <summary>
    ///   Gets or sets the snils.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string Snils { get; set; }

    /// <summary>
    ///   Статус заявления
    /// </summary>
    [XmlElement]
    [DataMember]
    public int Status { get; set; }

    /// <summary>
    ///   Gets or sets the status statement.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string StatusStatement { get; set; }

    /// <summary>
    ///   Gets or sets the tfom OKATO
    /// </summary>
    [XmlElement]
    [DataMember]
    public string TfomOKATO { get; set; }

    /// <summary>
    ///   Gets or sets the type statement.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string TypeStatement { get; set; }

    #endregion
  }
}