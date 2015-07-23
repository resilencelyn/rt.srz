// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageA08Base.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The a 08.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.cs.business.request.messages.A08
{
  using System;
  using System.Collections.Generic;

  using rt.srz.model.Hl7;
  using rt.srz.model.Hl7.person;
  using rt.srz.model.Hl7.person.messages;
  using rt.srz.model.Hl7.person.target;
  using rt.srz.model.srz;

  /// <summary>
  ///   The a 08.
  /// </summary>
  public abstract class MessageA08Base : MessageBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageA08Base"/> class.
    /// </summary>
    /// <param name="reason">
    /// The reason.
    /// </param>
    /// <param name="typeReason">
    /// The type Reason.
    /// </param>
    protected MessageA08Base(int reason, string typeReason)
      : base(reason, new MessageType { MessType = "ADT", TransactionCode = "A08", StructureType = "ADT_A01" }, typeReason)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Формирует сообщение
    /// </summary>
    /// <param name="messageId">
    ///   The message Id.
    /// </param>
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
    public override BaseMessageTemplate GetMessageTemplate(Guid messageId, Statement statementCurrent, MedicalInsurance medicalInsuranceCurrent, Statement statementPrevios = null, MedicalInsurance medicalInsurancePrevios = null)
    {
      var adt1 = new ADT_A01();

      adt1.Msh = GetMsh(messageId);
      adt1.Evn = GetEvn(medicalInsuranceCurrent.StateDateFrom);
      adt1.Pv1 = GetPv1();
      adt1.Pid = GetPid(statementCurrent);
      adt1.InsuranceList = GetInsuranceList(
                                            statementCurrent,
                                            medicalInsuranceCurrent,
                                            statementPrevios,
                                            medicalInsurancePrevios);

      adt1.Zvn = GetZvn();
      return adt1;
    }

    /// <summary>
    /// The get zvn.
    /// </summary>
    /// <returns>
    /// The <see cref="Zvn"/>.
    /// </returns>
    protected override Zvn GetZvn()
    {
      return null;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Формирует список страховок
    /// </summary>
    /// <param name="statementCurrent">
    /// Текущее заявление
    /// </param>
    /// <param name="medicalInsurance">
    /// The medical Insurance.
    /// </param>
    /// <param name="statementPrevios">
    /// The statement Previos.
    /// </param>
    /// <param name="medicalInsurancePrevios">
    /// The medical Insurance Previos.
    /// </param>
    /// <returns>
    /// The <see cref="List{ADT_A01_INSURANCE}"/>.
    /// </returns>
    protected abstract List<ADT_A01_INSURANCE> GetInsuranceList(
      Statement statementCurrent,
      MedicalInsurance medicalInsurance,
      Statement statementPrevios,
      MedicalInsurance medicalInsurancePrevios);

    #endregion
  }
}