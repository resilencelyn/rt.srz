// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorRelevantDocumentUdl.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator citizenship.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The validator citizenship.
  /// </summary>
  public class ValidatorRelevantDocumentUdl : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorRelevantDocumentUdl"/> class. 
    /// Initializes a new instance of the <see cref="ValidatorCitizenship"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorRelevantDocumentUdl(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Simple, sessionFactory)
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the caption.
    /// </summary>
    public override string Caption
    {
      get
      {
        return Resource.CaptionValidatorCitizenship;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check object.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    public override void CheckObject(Statement statement)
    {
      ////ISession session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      ////var ipData = statement.InsuredPersonData;
      ////var citizenship = ipData.Citizenship;
      ////int docType = statement.DocumentUdl.DocumentType.Id;
      ////var residencyDocument = statement.ResidencyDocument;
      ////int resDocType = -1;
      ////if (residencyDocument != null)
      ////  resDocType = residencyDocument.DocumentType.Id;

      ////var category = ipData.Category;

      ////switch (category.Id)
      ////{
      ////  case CategoryPerson.TerritorialRf:
      ////  case CategoryPerson.WorkerRf:
      ////    if (citizenship == null)
      ////      break;
      ////    var dateFiling = statement.DateFiling;
      ////    if (statement.InsuredPersonData.Birthday == null || dateFiling == null)
      ////      break;
      ////    var age = Age.CalculateAgeOnDate(statement.InsuredPersonData.Birthday.Value, dateFiling.Value);

      ////    if (age >= 14) //больше 14 лет - есть паспорт
      ////    {
      ////      if (docType != DocumentType.PassportRf && docType != DocumentType.DocumentType13)
      ////        throw new FaultRelevantDocumentUdlException(
      ////          new ExceptionInfo(model.Properties.Resource.FaultRelevantDocumentUdlExceptionRFCitizenCode),
      ////          model.Properties.Resource.FaultRelevantDocumentUdlExceptionRFCitizenMessage);
      ////      // "Не указан паспот или временное удостоверение личности
      ////    }
      ////    else
      ////    {
      ////      if (docType != DocumentType.BirthCertificateRf && docType != DocumentType.DocumentType24)
      ////        // наличие свидетельства о рождении
      ////        throw new FaultRelevantDocumentUdlException(
      ////          new ExceptionInfo(model.Properties.Resource.FaultRelevantDocumentUdlExceptionYoungRFCitizenCode),
      ////          model.Properties.Resource.FaultRelevantDocumentUdlExceptionYoungRFCitizenMessage);
      ////    }
      ////    break;

      ////    // "Иностранный гражданин"
      ////  case CategoryPerson.TerritorialAlienPermanently:
      ////  case CategoryPerson.WorkerAlienPermanently:
      ////    //проверка наличия удостоверения личности
      ////    if (docType != DocumentType.DocumentType9 && docType != DocumentType.DocumentType21)
      ////      throw new FaultRelevantDocumentUdlException(
      ////        new ExceptionInfo(model.Properties.Resource.FaultRelevantDocumentUdlExceptionWithoutCertificateCode),
      ////        model.Properties.Resource.FaultRelevantDocumentUdlExceptionWithoutCertificateMessage);
      ////    //проверка наличия вида на жительство
      ////    if (resDocType != DocumentType.DocumentType11)
      ////      throw new FaultRelevantDocumentUdlException(
      ////        new ExceptionInfo(model.Properties.Resource.FaultRelevantDocumentUdlExceptionNotRFCitizenCode),
      ////        model.Properties.Resource.FaultRelevantDocumentUdlExceptionNotRFCitizenMessage);
      ////    break;

      ////    // "Иностранный гражданин" 
      ////  case CategoryPerson.TerritorialAlienTeporary:
      ////  case CategoryPerson.WorkerAlienTeporary:
      ////    //проверка наличия удостоверения личности
      ////    if (docType != DocumentType.DocumentType9 && docType != DocumentType.DocumentType21)
      ////      throw new FaultRelevantDocumentUdlException(
      ////        new ExceptionInfo(model.Properties.Resource.FaultRelevantDocumentUdlExceptionWithoutCertificateCode),
      ////        model.Properties.Resource.FaultRelevantDocumentUdlExceptionWithoutCertificateMessage);
      ////    //проверка разрешения на проживания в РФ
      ////    if (resDocType != DocumentType.DocumentType23)
      ////      throw new FaultRelevantDocumentUdlException(
      ////        new ExceptionInfo(model.Properties.Resource.FaultRelevantDocumentUdlExceptionTempNotRFCitizenCode),
      ////        model.Properties.Resource.FaultRelevantDocumentUdlExceptionTempNotRFCitizenMessage);
      ////    break;

      ////  // "Беженец"
      ////  case CategoryPerson.TerritorialRefugee:
      ////  case CategoryPerson.WorkerRefugee:
      ////    if (docType != DocumentType.DocumentType12
      ////         && docType != DocumentType.DocumentType10
      ////         && docType != DocumentType.DocumentType25)
      ////      throw new FaultRelevantDocumentUdlException(
      ////        new ExceptionInfo(model.Properties.Resource.FaultRelevantDocumentUdlExceptionRefugeeCode),
      ////        model.Properties.Resource.FaultRelevantDocumentUdlExceptionRefugeeMessage);
      ////    break;

      ////    // "Без гражданства"
      ////  case CategoryPerson.TerritorialStatelessPermanently:
      ////  case CategoryPerson.WorkerStatelessPermanently:
      ////    //проверка наличия удостоверения личности
      ////    if (resDocType != DocumentType.DocumentType22)
      ////      throw new FaultRelevantDocumentUdlException(
      ////        new ExceptionInfo(model.Properties.Resource.FaultRelevantDocumentUdlExceptionWithoutCertificateCode),
      ////        model.Properties.Resource.FaultRelevantDocumentUdlExceptionWithoutCertificateMessage);
      ////    //проверка наличия вида на жительство
      ////    if (docType != DocumentType.DocumentType11)
      ////      throw new FaultRelevantDocumentUdlException(
      ////        new ExceptionInfo(model.Properties.Resource.FaultRelevantDocumentUdlExceptionNotCitizenCode),
      ////        model.Properties.Resource.FaultRelevantDocumentUdlExceptionNotCitizenMessage);
      ////    break;

      ////    // "Без гражданства"
      ////  case CategoryPerson.TerritorialStatelessTeporary:
      ////  case CategoryPerson.WorkerStatelessTeporary:
      ////    if (docType != DocumentType.DocumentType22 && resDocType != DocumentType.DocumentType23)
      ////      throw new FaultRelevantDocumentUdlException(
      ////        new ExceptionInfo(model.Properties.Resource.FaultRelevantDocumentUdlExceptionNotCitizen2Code),
      ////        model.Properties.Resource.FaultRelevantDocumentUdlExceptionNotCitizen2Message);
      ////    break;

      ////}
    }

    #endregion
  }
}