// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageA08Ins1.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The message a 08 ins 1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.cs.business.request.messages.A08
{
  using System.Collections.Generic;
  using System.Globalization;

  using rt.srz.model.Hl7;
  using rt.srz.model.Hl7.person.messages;
  using rt.srz.model.Hl7.person.target;
  using rt.srz.model.srz;

  /// <summary>
  ///   The message a 08 ins 1.
  /// </summary>
  public abstract class MessageA08Ins1 : MessageA08Base
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageA08Ins1"/> class.
    /// </summary>
    /// <param name="reason">
    /// The reason.
    /// </param>
    /// <param name="typeReason">
    /// The type reason.
    /// </param>
    protected MessageA08Ins1(int reason, string typeReason)
      : base(reason, typeReason)
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// Возвращает текущую страховку
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="medicalInsurance">
    /// The medical Insurance.
    /// </param>
    /// <param name="id">
    /// Номер страховки в сегменте
    /// </param>
    /// <returns>
    /// Страховка
    /// </returns>
    protected ADT_A01_INSURANCE GetInsuranceCurrent(Statement statement, MedicalInsurance medicalInsurance, int id = 1)
    {
      var in1 = new IN1();

      // IN1.1	SI	Да	Порядковый номер сегмента
      in1.Id = id.ToString(CultureInfo.InvariantCulture);

      // IN1.2	CWE	Да	Идентификатор плана страхования
      in1.PlanId = new PlanId();

      // IN1.3	CX	Да	Идентификатор организации
      in1.CompanyId = new CompanyId { Id = medicalInsurance.Smo.Ogrn, CompanyIdType = "NII" };

      // IN1.4	XON	Нет	Наименование организации
      in1.CompanyName = new CompanyName { Name = medicalInsurance.Smo.FullName };

      // IN1.12	DT	Да	Дата начала действия страховки
      in1.DateBeginInsurence = Hl7Helper.FormatDate(medicalInsurance.DateFrom);

      // IN1.13	DT	Да	Дата окончания действия страховки
      in1.DateEndInsurence = medicalInsurance.DateTo.Year == 2200 ? string.Empty : Hl7Helper.FormatDate(medicalInsurance.DateTo);

      // IN1.15	IS	Да	Код территории страхования
      in1.CodeOfRegion = medicalInsurance.Smo.Parent.Okato;

      // IN1.35	IS	Нет	Тип страховки
      in1.InsuranceType = medicalInsurance.PolisType != null ? medicalInsurance.PolisType.Code : string.Empty;

      // IN1.36	ST	Да	Номер страховки
      in1.InsuranceSerNum = medicalInsurance.SeriesNumber;

      return new ADT_A01_INSURANCE { In1 = in1 };
    }

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
    protected override List<ADT_A01_INSURANCE> GetInsuranceList(
      Statement statementCurrent, 
      MedicalInsurance medicalInsurance, 
      Statement statementPrevios, 
      MedicalInsurance medicalInsurancePrevios)
    {
      return new List<ADT_A01_INSURANCE> { GetInsuranceCurrent(statementCurrent, medicalInsurance) };
    }

    #endregion
  }
}