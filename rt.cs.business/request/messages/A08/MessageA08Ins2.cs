// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageA08Ins2.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The message a 08 ins 2.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.cs.business.request.messages.A08
{
  using System.Collections.Generic;

  using rt.srz.model.Hl7;
  using rt.srz.model.Hl7.person.messages;
  using rt.srz.model.Hl7.person.target;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  /// <summary>
  ///   The message a 08 ins 2.
  /// </summary>
  public abstract class MessageA08Ins2 : MessageA08Ins1
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageA08Ins2"/> class.
    /// </summary>
    /// <param name="reason">
    /// The reason.
    /// </param>
    /// <param name="typeReason">
    /// The type reason.
    /// </param>
    protected MessageA08Ins2(int reason, string typeReason)
      : base(reason, typeReason)
    {
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
    protected override List<ADT_A01_INSURANCE> GetInsuranceList(
      Statement statementCurrent,
      MedicalInsurance medicalInsurance,
      Statement statementPrevios,
      MedicalInsurance medicalInsurancePrevios)
    {
      return new List<ADT_A01_INSURANCE>
             {
               GetInsurancePrevios(statementPrevios, medicalInsurancePrevios), 
               GetInsuranceCurrent(statementCurrent, medicalInsurance, 2)
             };
    }

    /// <summary>
    /// The get insurance previos.
    /// </summary>
    /// <param name="statement">
    /// The statement current.
    /// </param>
    /// <param name="medicalInsurance">
    /// The medical insurance.
    /// </param>
    /// <returns>
    /// The <see cref="ADT_A01_INSURANCE"/>.
    /// </returns>
    private ADT_A01_INSURANCE GetInsurancePrevios(Statement statement, MedicalInsurance medicalInsurance)
    {
      var in1 = new IN1();

      // IN1.1	SI	Да	Порядковый номер сегмента
      in1.Id = "1";

      // IN1.2	CWE	Да	Идентификатор плана страхования
      in1.PlanId = new PlanId();

      // IN1.3	CX	Да	Идентификатор организации
      in1.CompanyId = new CompanyId { Id = medicalInsurance.Smo.Ogrn, CompanyIdType = "NII" };

      // IN1.4	XON	Нет	Наименование организации
      in1.CompanyName = new CompanyName { Name = medicalInsurance.Smo.FullName };

      // IN1.12	DT	Да	Дата начала действия страховки
      in1.DateBeginInsurence = Hl7Helper.FormatDate(medicalInsurance.DateFrom);

      // IN1.13	DT	Да	Дата окончания действия страховки
      in1.DateEndInsurence = medicalInsurance.DateTo.Year == 2200 ? null : Hl7Helper.FormatDate(medicalInsurance.DateTo);

      // IN1.15	IS	Да	Код территории страхования
      in1.CodeOfRegion = medicalInsurance.Smo.Parent.Okato;

      // IN1.35	IS	Нет	Тип страховки
      in1.InsuranceType = medicalInsurance.PolisType != null ? medicalInsurance.PolisType.Code : string.Empty;

      // IN1.36	ST	Да	Номер страховки
      in1.InsuranceSerNum = medicalInsurance.SeriesNumber;

      var personData = statement.InsuredPersonData;

      // IN1.16	XPN	Да	Фамилия, имя, отчество 
      in1.FioList = new List<Fio>
                    {
                      new Fio(
                        new Surname(personData.FirstName), 
                        personData.LastName, 
                        personData.MiddleName, 
                        "L")
                    };

      // IN1.18	DTM	Да	Дата рождения
      in1.BirthDay = personData.Birthday.HasValue ? Hl7Helper.FormatDate(personData.Birthday.Value) : null;

      var adr = statement.Address;
      var adr2 = statement.Address2 ?? adr;
      in1.AddressList = new List<Address>
                        {
                          adr.IsHomeless.HasValue && adr.IsHomeless.Value
                            ? new Address { AddressType = "L" }
                            : new Address { Region = adr.Okato, Country = "RUS", AddressType = "L", }
                        };

      if (!(adr.IsHomeless.HasValue && adr.IsHomeless.Value))
      {
        in1.AddressList.Add(new Address { Region = adr2.Okato, Country = "RUS", AddressType = "H", });
      }

      // IN1.42 
      in1.Employment = new Employment { employment = CategoryPerson.IsWorking(personData.Category.Id) ? "1" : "0" };

      // IN1.43	IS	Усл	Пол
      in1.Sex = personData.Gender.Code;

      // IN1.49	CX	Да	Список идентификаторов
      in1.IdentificatorsList = new List<Identificators>();

      var document = statement.DocumentUdl;
      in1.IdentificatorsList.Add(
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

      document = statement.DocumentRegistration;

      if (document != null)
      {
        in1.IdentificatorsList.Add(
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

      // Номер полиса
      if (!string.IsNullOrEmpty(statement.InsuredPerson.MainPolisNumber))
      {
        in1.IdentificatorsList.Add(
                                   new Identificators
                                   {
                                     identificatorType = "NI",
                                     identificator = statement.InsuredPerson.MainPolisNumber
                                   });
      }

      // СНИЛС
      if (!string.IsNullOrEmpty(personData.Snils))
      {
        in1.IdentificatorsList.Add(new Identificators { identificatorType = "PEN", identificator = personData.Snils });
      }

      // IN1.52	ST	Да	Место рождения
      in1.PlaceOfBirth = personData.Birthplace;

      return new ADT_A01_INSURANCE { In1 = in1 };
    }

    #endregion
  }
}