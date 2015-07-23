// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageA03.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.cs.business.request.messages.A03
{
  using System;

  using rt.srz.model.Hl7.person;
  using rt.srz.model.Hl7.person.messages;
  using rt.srz.model.Hl7.person.target;
  using rt.srz.model.srz;

  /// <summary>
  /// The message a03.
  /// </summary>
  public class MessageA03 : MessageBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MessageA03"/> class.
    /// </summary>
    public MessageA03()
      : base(srz.model.srz.concepts.ReasonType.П07, new MessageType { MessType = "ADT", TransactionCode = "A08", StructureType = "ADT_A03" }, "П07")
    {
    }

    /// <summary>
    /// The get message template.
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="statementCurrent">
    ///   The statement current.
    /// </param>
    /// <param name="medicalInsuranceCurrent">
    ///   The medical insurance current.
    /// </param>
    /// <param name="statementPrevios">
    ///   The statement previos.
    /// </param>
    /// <param name="medicalInsurancePrevios">
    ///   The medical insurance previos.
    /// </param>
    /// <returns>
    /// The <see cref="BaseMessageTemplate"/>.
    /// </returns>
    public override BaseMessageTemplate GetMessageTemplate(Guid messageId, Statement statementCurrent, MedicalInsurance medicalInsuranceCurrent, Statement statementPrevios = null, MedicalInsurance medicalInsurancePrevios = null)
    {
      var adt3 = new ADT_A03();

      if (statementCurrent.InsuredPerson.DeadInfo != null)
      {
        adt3.Evn = GetEvn(statementCurrent.InsuredPerson.DeadInfo.DateDead);
      }

      adt3.Pv1 = GetPv1();

      adt3.Pid = GetPid(statementCurrent);

      return adt3;
    }
  }
}