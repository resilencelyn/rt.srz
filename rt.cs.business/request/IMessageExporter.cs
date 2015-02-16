// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMessageExporter.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.cs.business.request
{
  using System;

  using rt.srz.model.Hl7.person;
  using rt.srz.model.srz;

  /// <summary>
  ///   The MessageBase interface.
  /// </summary>
  public interface IMessageExporter
  {
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
    bool AppliesTo(int reason);

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
    BaseMessageTemplate GetMessageTemplate(Guid messageId, Statement statementCurrent, MedicalInsurance medicalInsuranceCurrent, Statement statementPrevios = null, MedicalInsurance medicalInsurancePrevios = null);

    #endregion
  }
}