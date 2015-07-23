// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBase.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.cs.business.request
{
  using System;
  using System.Collections.Generic;

  using rt.cs.business.config;
  using rt.srz.model.Hl7;
  using rt.srz.model.Hl7.person;
  using rt.srz.model.Hl7.person.target;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  /// <summary>
  ///   The message base.
  /// </summary>
  public abstract class MessageBase : IMessageExporter
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageBase"/> class.
    /// </summary>
    /// <param name="reason"></param>
    /// <param name="messType">
    ///   The mess Type.
    /// </param>
    /// <param name="reasonType">
    ///   The reason Type.
    /// </param>
    protected MessageBase(int reason, MessageType messType, string reasonType)
    {
      Reason = reason;
      MessType = messType;
      ReasonType = reasonType;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the reason.
    /// </summary>
    public int Reason { get; private set; }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets the message type.
    /// </summary>
    protected MessageType MessType { get; private set; }

    /// <summary>
    ///   Gets the reason type.
    /// </summary>
    protected string ReasonType { get; private set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The applies to.
    /// </summary>
    /// <param name="reason">
    /// The reason.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool AppliesTo(int reason)
    {
      return reason == Reason;
    }

    /// <summary>
    /// Формирует сообщение
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="statementCurrent">
    ///   Текущее заявление.
    /// </param>
    /// <param name="medicalInsuranceCurrent">
    ///   The medical Insurance Current.
    /// </param>
    /// <param name="statementPrevios">
    ///   The statement Previos.
    /// </param>
    /// <param name="medicalInsurancePrevios">
    ///   The medical Insurance Previos.
    /// </param>
    /// <returns>
    /// The <see cref="BaseMessageTemplate"/>.
    /// </returns>
    public abstract BaseMessageTemplate GetMessageTemplate(Guid messageId, Statement statementCurrent, MedicalInsurance medicalInsuranceCurrent, Statement statementPrevios = null, MedicalInsurance medicalInsurancePrevios = null);

    #endregion

    #region Methods

    /// <summary>
    /// Формирует Evn
    /// </summary>
    /// <param name="time">
    /// The time.
    /// </param>
    /// <returns>
    /// The <see cref="Evn"/>.
    /// </returns>
    protected virtual Evn GetEvn(DateTime time)
    {
      return new Evn
             {
               CodeReasonEvent = ReasonType,
               DateRegistrationEvent = Hl7Helper.FormatDateTime(time)
             };
    }

    /// <summary>
    /// The get msh.
    /// </summary>
    /// <param name="messageId">
    ///   The message Id.
    /// </param>
    /// <returns>
    /// The <see cref="MSH"/>.
    /// </returns>
    protected virtual MSH GetMsh(Guid messageId)
    {
      var msh = new MSH();

      // MSH.1
      msh.FieldDivider = Hl7Helper.BHS_Delimiter;

      // MSH.2
      msh.SpecialSymbol = Hl7Helper.BHS_CodeSymbols;

      ////// MSH.3
      ////msh.OriginApplicationName = new BHS3 { Application = "СРЗ " + sender.Code };

      ////// MSH.4
      ////msh.OriginOrganizationName = new BHS4 { CodeOfRegion = sender.Code };

      ////// MSH.5
      ////msh.ApplicationName.Application = "ЦК ЕРП";

      ////// MSH.6
      ////msh.OrganizationName = new BHS6 { FomsCode = "00" };

      // MSH.7
      msh.DateTimeCreation = Hl7Helper.FormatCurrentDateTime();

      // MSH.9
      msh.MessageType = MessType;

      // MSH.10
      msh.Identificator = messageId.ToString();

      // MSH.11
      msh.TypeWork = new TypeWork { Type = ConfigManager.TypeWork };

      // MSH.12
      msh.VersionStandartId = new VersionStandartId();

      // MSH.15
      msh.ConfirmationTypeGateWay = "AL";

      // MSH.16
      msh.ConfirmationTypeFoms = "AL";

      return msh;
    }

    /// <summary>
    /// The get pid.
    /// </summary>
    /// <param name="statementCurrent">
    /// Текущее заявление
    /// </param>
    /// <returns>
    /// The <see cref="MessagePid"/>.
    /// </returns>
    protected virtual MessagePid GetPid(Statement statementCurrent)
    {
      var personData = statementCurrent.InsuredPersonData;

      var deadInfo = statementCurrent.InsuredPerson.DeadInfo;
      var insuredPerson = statementCurrent.InsuredPerson;

      var pid = new MessagePid();

      // PID.11
      var adr = statementCurrent.Address;
      var adr2 = statementCurrent.Address2 ?? adr;
      pid.AddressList = new List<Address>
                        {
                          adr.IsHomeless.HasValue && adr.IsHomeless.Value
                            ? new Address { AddressType = "L" }
                            : new Address { Region = adr.GetOkatoRegion(), Country = "RUS", AddressType = "L", }
                        };

      if (!(adr.IsHomeless.HasValue && adr.IsHomeless.Value))
      {
        pid.AddressList.Add(new Address { Region = adr2.GetOkatoRegion(), Country = "RUS", AddressType = "H", });
      }

      // PID.7
      pid.BirthDay = personData.Birthday.HasValue ? Hl7Helper.FormatDate(personData.Birthday.Value) : string.Empty;

      // PID.29
      if (deadInfo != null && deadInfo.ActRecordDate.HasValue)
      {
        pid.DeadDay = Hl7Helper.FormatDate(deadInfo.ActRecordDate.Value);
      }

      // PID.5
      pid.FioList = new List<Fio>
                    {
                      new Fio(
                        new Surname(personData.FirstName), 
                        personData.LastName, 
                        personData.MiddleName, 
                        "L")
                    };

      // PID.3
      pid.IdentificatorsList = new List<Identificators>();

      // Документ УДЛ
      var document = statementCurrent.DocumentUdl;
      pid.IdentificatorsList.Add(
                                 new Identificators
                                 {
                                   identificator = document.SeriesNumber,
                                   identificatorType = document.DocumentType.Code,
                                   identificatorFrom = Hl7Helper.FormatDate(document.DateIssue),
                                   identificatorTo =
                                     document.DateExp.HasValue
                                       ? Hl7Helper.FormatDate(document.DateExp.Value)
                                       : null
                                 });

      document = statementCurrent.ResidencyDocument;
      if (document != null)
      {
        pid.IdentificatorsList.Add(
                                   new Identificators
                                   {
                                     identificator = document.SeriesNumber,
                                     identificatorType = document.DocumentType.Code,
                                     identificatorFrom = Hl7Helper.FormatDate(document.DateIssue),
                                     identificatorTo =
                                       document.DateExp.HasValue
                                         ? Hl7Helper.FormatDate(document.DateExp.Value)
                                         : null
                                   });
      }

      // Номер ЕНП
      if (!string.IsNullOrEmpty(insuredPerson.MainPolisNumber))
      {
        pid.IdentificatorsList.Add(
                                   new Identificators
                                   {
                                     identificatorType = "NI",
                                     identificator = insuredPerson.MainPolisNumber
                                   });
      }

      // СНИЛС
      if (!string.IsNullOrEmpty(personData.Snils))
      {
        pid.IdentificatorsList.Add(new Identificators { identificatorType = "PEN", identificator = personData.Snils });
      }

      // PID.30
      pid.IsDead = deadInfo != null ? "1" : "0";

      // PID.26"
      if (!personData.IsRefugee)
      {
        pid.Nationality = new National
                          {
                            Nationality = personData.IsNotCitizenship ? "Б/Г" : personData.Citizenship.Code,
                            TableCode = Oid.Страна
                          };
      }
      else
      {
        pid.Nationality = new National
                          {
                            // Является беженцем
                            Nationality = "1",
                            TableCode = "1.2.643.2.40.3.3.0.6.19"
                          };
      }

      // PID.23
      pid.PlaceOfBirth = personData.Birthplace;

      // PID.32
      pid.ReliabilityIdList = new List<string>();

      // PID.8
      pid.Sex = personData.Gender.Code;

      return pid;
    }

    /// <summary>
    ///   Формирует Pv1
    /// </summary>
    /// <returns>
    ///   The <see cref="Pv1" />.
    /// </returns>
    protected virtual Pv1 GetPv1()
    {
      return new Pv1();
    }

    /// <summary>
    ///   The get zvn.
    /// </summary>
    /// <returns>
    ///   The <see cref="Zvn" />.
    /// </returns>
    protected virtual Zvn GetZvn()
    {
      return new Zvn();
    }

    #endregion
  }
}