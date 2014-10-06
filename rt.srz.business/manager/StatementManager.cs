// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementManager.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The StatementManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Linq;

  using NHibernate;
  using NHibernate.Context;
  using NHibernate.Criterion;

  using NLog;

  using ProtoBuf;

  using rt.core.business.security.interfaces;
  using rt.srz.business.interfaces.logicalcontrol;
  using rt.srz.business.manager.cache;
  using rt.srz.model.dto;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The StatementManager.
  /// </summary>
  public partial class StatementManager
  {
    #region Fields

    /// <summary>
    ///   The logger.
    /// </summary>
    private readonly Logger logger;

    /// <summary>
    ///   The period manager.
    /// </summary>
    private readonly PeriodManager periodManager;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="StatementManager" /> class.
    /// </summary>
    public StatementManager()
    {
      logger = LogManager.GetCurrentClassLogger();
      periodManager = ObjectFactory.GetInstance<PeriodManager>();
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The apply active.
    /// </summary>
    /// <param name="person">
    /// The person.
    /// </param>
    public void ApplyActive(InsuredPerson person)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      // �������� ��� �� ���������� ���������
      var history =
        session.QueryOver<Statement>()
               .Where(x => x.InsuredPerson.Id == person.Id)
               .And(x => x.Status.Id != StatusStatement.Cancelled)
               .And(x => x.Status.Id != StatusStatement.Declined)
               .OrderBy(x => x.DateFiling)
               .Asc.ThenBy(x => x.Id)
               .Desc.List();

      // �������� ����� ������ ���������
      var last = history.FirstOrDefault();
      if (last != null)
      {
        // � ������ ������� ��� ����������� ���������
        last.IsActive = false;
        last.PreviousStatement = null;

        // ��������� ����� ������ � ������ ��������������� �������
        foreach (var x in history.Skip(1))
        {
          x.IsActive = false;
          x.PreviousStatement = last;
          last = x;
        }

        last.IsActive = true;
      }

      ObjectFactory.GetInstance<IInsuredPersonManager>().OnCanceledOrRemoveStatement(person);
    }

    /// <summary>
    /// �������������� ���������
    /// </summary>
    /// <param name="statementId">
    /// The statement Id.
    /// </param>
    public void CanceledStatement(Guid statementId)
    {
      var statement = GetById(statementId);
      if (StatusStatement.CanCanceled(statement.Status.Id))
      {
        var conceptCacheManager = ObjectFactory.GetInstance<IConceptCacheManager>();
        var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

        // ������ ������ ����������
        statement.Status = conceptCacheManager.GetById(StatusStatement.Cancelled);
        session.Update(statement);

        // ��������� ���������
        foreach (var medicalInsurance in statement.MedicalInsurances)
        {
          medicalInsurance.IsActive = false;
          session.Update(medicalInsurance);
        }

        ObjectFactory.GetInstance<IErrorManager>().Delete(x => x.Statement.Id == statementId);

        session.Flush();

        ApplyActive(statement.InsuredPerson);

        session.Flush();
      }
    }

    /// <summary>
    /// The create from example.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/> .
    /// </returns>
    public Statement CreateFromExample(Statement statement)
    {
      var result = new Statement
                   {
                     Address = statement.Address != null ? Serializer.DeepClone(statement.Address) : null, 
                     Address2 =
                       statement.Address2 != null ? Serializer.DeepClone(statement.Address2) : null, 
                     DocumentUdl = Serializer.DeepClone(statement.DocumentUdl), 
                     DocumentRegistration =
                       statement.DocumentRegistration != null
                         ? Serializer.DeepClone(statement.DocumentRegistration)
                         : null, 
                     ResidencyDocument =
                       statement.ResidencyDocument != null
                         ? Serializer.DeepClone(statement.ResidencyDocument)
                         : null, 
                     ContactInfo =
                       statement.ContactInfo != null ? Serializer.DeepClone(statement.ContactInfo) : null, 
                     FormManufacturing = statement.FormManufacturing, 
                     InsuredPerson = statement.InsuredPerson, 
                     InsuredPersonData =
                       statement.InsuredPersonData != null
                         ? Serializer.DeepClone(statement.InsuredPersonData)
                         : null, 
                     PointDistributionPolicy = statement.PointDistributionPolicy
                   };

      if (statement.MedicalInsurances != null)
      {
        result.MedicalInsurances = statement.MedicalInsurances.Where(x => x.PolisType.Id != PolisType.�).ToList();
        var polis = statement.PolisMedicalInsurance;
        if (polis != null)
        {
          result.NumberPolicy = polis.Enp;
        }
      }

      var polisMedicalInsurance = statement.PolisMedicalInsurance;
      result.NumberPolicy = polisMedicalInsurance != null ? polisMedicalInsurance.Enp : statement.NumberPolicy;

      if (statement.Address != null)
      {
        result.Address.Kladr = statement.Address.Kladr;
      }

      if (statement.Address2 != null)
      {
        result.Address2.Kladr = statement.Address2.Kladr;
      }

      if (statement.DocumentRegistration != null)
      {
        result.DocumentRegistration.DocumentType = statement.DocumentRegistration.DocumentType;
      }

      if (statement.InsuredPersonData != null)
      {
        result.InsuredPersonData.Gender = statement.InsuredPersonData.Gender;
        result.InsuredPersonData.Category = statement.InsuredPersonData.Category;
        result.InsuredPersonData.OldCountry = statement.InsuredPersonData.OldCountry;
        result.InsuredPersonData.Citizenship = statement.InsuredPersonData.Citizenship;
      }

      return result;
    }

    /// <summary>
    /// �������� ���� � ������
    /// </summary>
    /// <param name="statementId">
    /// The statement Id.
    /// </param>
    public void DeleteDeathInfo(Guid statementId)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var statement = ObjectFactory.GetInstance<IStatementManager>().GetById(statementId);
      var manager = ObjectFactory.GetInstance<IInsuredPersonManager>();
      var person = statement.InsuredPerson;
      person.Status = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(StatusPerson.Active);
      manager.SaveOrUpdate(person);
      session.Flush();
    }

    /// <summary>
    /// �������� ��������� �� InsuredPersonId � IsActive = 1
    /// </summary>
    /// <param name="insuredPersonId">
    /// The insured Person Id.
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/>.
    /// </returns>
    public Statement GetActiveByInsuredPersonId(Guid insuredPersonId)
    {
      return GetBy(x => x.InsuredPerson.Id == insuredPersonId).FirstOrDefault(x => x.IsActive);
    }

    /// <summary>
    /// �������� ������ ������������ � ���������� �� ��������� ������
    /// </summary>
    /// <param name="startDate">
    /// The start Date.
    /// </param>
    /// <param name="endDate">
    /// The end Date.
    /// </param>
    /// <returns>
    /// The <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    public IList<string> GetErrorsByPeriod(DateTime startDate, DateTime endDate)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      Statement statement = null;
      var result =
        session.QueryOver<Error>()
               .JoinAlias(x => x.Statement, () => statement)
               .Where(x => statement.DateFiling.IsBetween(startDate).And(endDate))
               .SelectList(x => x.SelectGroup(y => y.Message1))
               .List<string>();
      return result;
    }

    /// <summary>
    /// The get search statement result.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="SearchStatementResult"/>.
    /// </returns>
    public SearchStatementResult GetSearchStatementResult(Guid id)
    {
      var statement = GetById(id);
      var typeStatement =
        ObjectFactory.GetInstance<IConceptCacheManager>()
                     .GetById(Statement.GetTypeStatementId(statement.CauseFiling.Id))
                     .Name;

      return new SearchStatementResult(statement, typeStatement);
    }

    /// <summary>
    /// ������ ��������� �� �������� ���������(���������, XML)
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="srzOperationName">
    /// The srz Operation Name.
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/>.
    /// </returns>
    public Statement ImportStatementFromExternalSource(Statement statement, string srzOperationName)
    {
      if (srzOperationName != "�060")
      {
        return SaveStatement(statement);
      }

      // ����� - ����������� ������ ��� �������� "�060" - ������ �� ���� ������ ������� ������� ����� ���������� �������������

      // ����������� �����
      var searchKeys = CalculateSearchKeys(statement);

      // ���� �������������� ����
      var insuredPersons = ObjectFactory.GetInstance<IStatementSearchManager>().GetInsuredPersonsByKeys(searchKeys);

      // �� ������� �������������� ����, �������� �� ��������
      if (insuredPersons.Count == 0)
      {
        throw new FaultNotFoundInsuredPerson();
      }

      // �������� �������� ���������
      var insuredPerson = insuredPersons.First();
      var activeStatement = GetActiveByInsuredPersonId(insuredPerson.Id);
      if (activeStatement == null)
      {
        throw new FaultNotFoundInsuredPerson();
      }

      // ��������� ������ � ������� ������
      activeStatement.IsExportTemp = statement.IsExportTemp;
      activeStatement.IsExportPolis = statement.IsExportPolis;
      activeStatement.PrzBuffId = statement.PrzBuffId;
      activeStatement.PidId = statement.PidId;
      activeStatement.PolisId = statement.PolisId;
      activeStatement.DateFiling = statement.DateFiling;

      activeStatement.NumberPolicy = statement.NumberPolicy;
      activeStatement.Status = statement.Status;
      activeStatement.NotCheckPolisNumber = statement.NotCheckPolisNumber;
      activeStatement.InsuredPersonData.NotCheckSnils = statement.InsuredPersonData.NotCheckSnils;
      activeStatement.InsuredPersonData.NotCheckExistsSnils = statement.InsuredPersonData.NotCheckExistsSnils;
      activeStatement.InsuredPersonData.IsNotCitizenship = statement.InsuredPersonData.IsNotCitizenship;
      activeStatement.DocumentUdl.DocumentType = statement.DocumentUdl.DocumentType;
      activeStatement.DocumentUdl.ExistDocument = statement.DocumentUdl.ExistDocument;
      activeStatement.DocumentUdl.Number = statement.DocumentUdl.Number;
      activeStatement.DocumentUdl.Series = statement.DocumentUdl.Series;
      activeStatement.DocumentUdl.IssuingAuthority = statement.DocumentUdl.IssuingAuthority;
      activeStatement.DocumentUdl.DateIssue = statement.DocumentUdl.DateIssue;
      activeStatement.DocumentUdl.DateExp = statement.DocumentUdl.DateExp;

      activeStatement.AbsentPrevPolicy = true;

      activeStatement.PolicyIsIssued = true;
      var polis = statement.MedicalInsurances.First(x => x.PolisType.Id != PolisType.�);
      polis.Statement = activeStatement;
      activeStatement.MedicalInsurances.Add(polis);
      if (statement.ResidencyDocument != null)
      {
        activeStatement.ResidencyDocument = statement.ResidencyDocument;
      }

      // ��������� ���������� ���������
      return SaveStatement(activeStatement);
    }

    /// <summary>
    /// ���������� ���������
    /// </summary>
    /// <param name="sstatement">
    /// The sstatement.
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/> .
    /// </returns>
    public Statement SaveStatement(Statement sstatement)
    {
      var statement = sstatement;

      var statementSearchManager = ObjectFactory.GetInstance<IStatementSearchManager>();

      // ������� ������
      var sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
      var session = sessionFactory.GetCurrentSession();
      try
      {
        using (var transaction = session.BeginTransaction(IsolationLevel.ReadUncommitted))
        {
          // ������ ���� ������� ��������
          // ��������!!! ������������ ��������� ��� ��� � ���� ������ � � ������ ������ �� ��������
          // ��������!!! ���� ���� ���� ������� �������� ������� � ���� ������ ��� ������� ������, ���
          // ������ ���������, ��� ������ 
          ObjectFactory.GetInstance<ICheckManager>().CheckStatement(statement, CheckLevelEnum.Simple);

          // ����������� ��������������� ����
          SetAutoProperty(statement, null);

          // �������� ��������� ������ � ��, � ����� ������� � �� Flush(), ����� �� ������ ������� � ��
          // ���������� ������� ���������
          ObjectFactory.GetInstance<IStatementChangeDateManager>().SaveStatementChangeHistory(statement);

          // ���� ������ ID, �� ���������, ���� ���, �� �����������
          if (statement.Id == Guid.Empty)
          {
            session.SaveOrUpdate(statement);
          }
          else
          {
            session.Replicate(statement, ReplicationMode.Overwrite);
          }

          // ���������� ��������� ������
          SaveRelationData(statement);

          // ����� ����������� ������� � ��, ������� ���������� ��� ��������� �� ������ 
          session.Flush();

          // ������ ������
          var allKeys = CalculateSearchKeys(statement).ToList();

          // ����� Insured Person
          var currentPerson = statement.InsuredPerson;

          // ��������� ������� ��� ��������������, ����� ���������� ���� ��������
          var peopleKeys = statementSearchManager.GetInsuredPersonByStatement(statement, allKeys);
          InsuredPerson peoplePrevios = null;

          if (statement.PreviousStatement != null && statement.PreviousStatement.InsuredPerson != null
              && statement.PreviousStatement.InsuredPerson.Id != Guid.Empty)
          {
            peoplePrevios = statement.PreviousStatement.InsuredPerson;
          }

          statement.InsuredPerson = peoplePrevios ?? peopleKeys;

          // ��������� ��������
          if (peoplePrevios != null && peopleKeys.Id != Guid.Empty && peoplePrevios.Id != peopleKeys.Id)
          {
            ObjectFactory.GetInstance<IInsuredPersonManager>()
                         .AddTwinsFirstAndOther(new List<InsuredPerson> { peoplePrevios, peopleKeys });
          }

          // ���� ���� ���������, �� ���� ��������� ������������� ������
          if (currentPerson != null && currentPerson.Id != statement.InsuredPerson.Id)
          {
            foreach (var medIns in statement.MedicalInsurances)
            {
              medIns.InsuredPerson = statement.InsuredPerson;
            }

            session.Flush();
            ChangeInsuredPerson(statement.InsuredPerson, currentPerson);
          }

          foreach (var ins in statement.MedicalInsurances)
          {
            ins.InsuredPerson = statement.InsuredPerson;
          }

          // � ������ ���������� ������, � ����� ��������� � �� �� ���� ��������
          session.SaveOrUpdate(statement);
          session.Flush();

          // ������� ��������, ������ ��������, ������� ����� ������ ������� � ��
          ObjectFactory.GetInstance<ICheckManager>().CheckStatement(statement, CheckLevelEnum.Complex);

          // ���������� ������
          ObjectFactory.GetInstance<ISearchKeyManager>().SaveSearchKeys(statement, allKeys);
          session.Flush();

          // ������� ���������� � ����������, ������������
          SaveEmloymentHistry(statement);

          // ���������� ����������
          ApplyActive(statement.InsuredPerson);

          session.Flush();

          // �������� ���
          ObjectFactory.GetInstance<INumberPolicyCounterManager>()
                       .RecalculateNumberPolicyCounter(statement.NumberPolicy);
          session.Flush();

          // ������������ ����������� ���������
          ProcesingBadData(statement);

          // ������������ ����������� ���������
          ProcesingBadData(statement);

          // ������� ���� 
          ////var a01manager = ObjectFactory.GetInstance<IStatementADT_A01Manager>();
          ////var batch = a01manager.CreateBatchForExportADT_A01(statement);
          //// ��������� ADT_A01 ��� ���������� ��� � ������� ����� ��
          ////a01manager.Export_ADT_A01_ForFLK(batch, statement);
          ////session.Flush();

          // �������� ��������� � ����
          transaction.Commit();

          ////// ��������� ���������� � �������� ���������
          ////var uam = new UserActionManager();
          ////uam.LogAccessToPersonalData(statement, "��������� ������");
        }
      }
      catch (LogicalControlException)
      {
        session.Clear();
        throw;
      }
      catch (Exception)
      {
        // ��������� � ������� ������ ������
        CurrentSessionContext.Unbind(sessionFactory);
        session.Dispose();

        // ��������� ����� ������
        session = sessionFactory.OpenSession();
        CurrentSessionContext.Bind(session);
        throw;
      }

      return statement;
    }

    /// <summary>
    /// ���� ����� ���������
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    public void TrimStatementData(Statement statement)
    {
      if (statement.InsuredPersonData != null)
      {
        if (statement.InsuredPersonData.FirstName != null)
        {
          statement.InsuredPersonData.FirstName = TrimInside(statement.InsuredPersonData.FirstName);
            
            ////.ToUpperFirstLowerOther();
        }

        if (statement.InsuredPersonData.LastName != null)
        {
          statement.InsuredPersonData.LastName = TrimInside(statement.InsuredPersonData.LastName);
            
            ////.ToUpperFirstLowerOther();
        }

        if (statement.InsuredPersonData.MiddleName != null)
        {
          statement.InsuredPersonData.MiddleName = TrimInside(statement.InsuredPersonData.MiddleName);
            
            ////.ToUpperFirstLowerOther();
        }

        if (statement.InsuredPersonData.Birthplace != null)
        {
          statement.InsuredPersonData.Birthplace = TrimInside(statement.InsuredPersonData.Birthplace);
        }
      }

      TrimInsideDocument(statement.DocumentRegistration);
      TrimInsideDocument(statement.DocumentUdl);
      TrimInsideDocument(statement.ResidencyDocument);
    }

    /// <summary>
    /// The un bind statement.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    public void UnBindStatement(Statement statement)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      if (session.Contains(statement.Address))
      {
        session.Evict(statement.Address);
      }

      if (session.Contains(statement.Address2))
      {
        session.Evict(statement.Address2);
      }

      if (session.Contains(statement.ContactInfo))
      {
        session.Evict(statement.ContactInfo);
      }

      if (session.Contains(statement.DocumentRegistration))
      {
        session.Evict(statement.DocumentRegistration);
      }

      if (session.Contains(statement.DocumentUdl))
      {
        session.Evict(statement.DocumentUdl);
      }

      if (session.Contains(statement.ResidencyDocument))
      {
        session.Evict(statement.ResidencyDocument);
      }

      if (session.Contains(statement.InsuredPersonData))
      {
        session.Evict(statement.InsuredPersonData);
      }

      if (session.Contains(statement.InsuredPerson))
      {
        session.Evict(statement.InsuredPerson);
      }

      if (session.Contains(statement))
      {
        session.Evict(statement);
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// ����������� ����������� � ���������������� �����
    /// </summary>
    /// <param name="statement">
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable{SearchKey}"/>.
    /// </returns>
    private IEnumerable<SearchKey> CalculateSearchKeys(Statement statement)
    {
      // ������ ����������� ������
      IList<SearchKey> standardKeys = null;
      try
      {
        standardKeys = ObjectFactory.GetInstance<ISearchKeyManager>().CalculateStandardKeys(statement);
      }
      catch (Exception ex)
      {
        logger.Error(ex);
        throw new StandardSearchKeyCalculationException();
      }

      // ������ ���������������� ������
      IList<SearchKey> userKeys = null;
      try
      {
        // �������� ������� �����
        var tfoms = statement.PointDistributionPolicy.Parent.Parent;

        // �������� ��� ���������������� ����� ���������� ���������
        var keyTypeList =
          ObjectFactory.GetInstance<ISessionFactory>()
                       .GetCurrentSession()
                       .QueryOver<SearchKeyType>()
                       .Where(
                              x =>
                              x.Tfoms.Id == tfoms.Id && x.IsActive
                              && x.OperationKey.Id == OperationKey.FullScanAndSaveKey)
                       .List();

        // C������ ���������������� �����
        userKeys = ObjectFactory.GetInstance<ISearchKeyManager>()
                                .CalculateUserKeys(
                                                   keyTypeList, 
                                                   statement.InsuredPersonData, 
                                                   statement.DocumentUdl, 
                                                   statement.Address, 
                                                   statement.Address2 ?? statement.Address, 
                                                   statement.MedicalInsurances, 
                                                   tfoms.Okato);
      }
      catch (Exception ex)
      {
        logger.Error(ex);
        throw new UserSearchKeyCalculationException();
      }

      // ���������� ������ ���� ������
      return standardKeys.Concat(userKeys);
    }

    /// <summary>
    /// The change insured person.
    /// </summary>
    /// <param name="currentPerson">
    /// The current person.
    /// </param>
    /// <param name="person">
    /// The person.
    /// </param>
    private void ChangeInsuredPerson(InsuredPerson currentPerson, InsuredPerson person)
    {
      ApplyActive(person);
    }

    /// <summary>
    /// The procesing bad data.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    private void ProcesingBadData(Statement statement)
    {
      if (statement == null)
      {
        throw new ArgumentNullException("statement");
      }

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      // ������������ ����������� �������
      if (statement.DocumentUdl != null && statement.DocumentUdl.ExistDocument)
      {
        Statement st = null;
        var insuredPersonId = statement.InsuredPerson != null ? statement.InsuredPerson.Id : Guid.Empty;
        var docTypeId = statement.DocumentUdl.DocumentType.Id;
        var series = statement.DocumentUdl.Series;
        var number = statement.DocumentUdl.Number;

        var query =
          session.QueryOver<Document>()
                 .JoinAlias(x => x.Statements1, () => st)
                 .Where(x => x.DocumentType.Id == docTypeId)
                 .And(x => x.Series == series)
                 .And(x => x.Number == number)
                 .And(x => st.Id != statement.Id)
                 .And(x => st.Status.Id != StatusStatement.Cancelled)
                 .And(x => st.Status.Id != StatusStatement.Declined);
        if (insuredPersonId != Guid.Empty)
        {
          query.Where(x => st.InsuredPerson.Id != insuredPersonId);
        }

        // �������� ������ ����������� ���������� � ��
        foreach (var document in query.List())
        {
          document.IsBad = true;
          session.Update(document);
        }

        session.Flush();
      }

      // ��������� ������� ������
      if (statement.InsuredPersonData != null && statement.InsuredPersonData.NotCheckExistsSnils)
      {
        InsuredPersonDatum d = null;
        var query =
          session.QueryOver<Statement>()
                 .JoinAlias(x => x.InsuredPersonData, () => d)
                 .Where(x => d.Snils == statement.InsuredPersonData.Snils)
                 .And(x => x.InsuredPerson.Id != statement.InsuredPerson.Id)
                 .And(x => x.Id != statement.Id)
                 .And(x => x.Status.Id != StatusStatement.Cancelled)
                 .And(x => x.Status.Id != StatusStatement.Declined)
                 .List();

        foreach (var s in query)
        {
          s.InsuredPersonData.IsBadSnils = true;
          session.Update(s.InsuredPersonData);
        }

        session.Flush();
      }
    }

    /// <summary>
    /// The save contents.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    private void SaveContents(Statement statement)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      foreach (var content in statement.InsuredPersonData.Contents)
      {
        content.InsuredPersonData = statement.InsuredPersonData;
        if (session.Contains(content))
        {
          session.SaveOrUpdate(content);
        }
        else
        {
          session.Replicate(content, ReplicationMode.Overwrite);
        }
      }

      var fotos =
        statement.InsuredPersonData.Contents.Where(x => x.ContentType.Id == TypeContent.Foto)
                 .OrderByDescending(x => x.ChangeDate)
                 .Skip(1);

      foreach (var content in fotos)
      {
        session.Delete(content);
      }

      var signature =
        statement.InsuredPersonData.Contents.Where(x => x.ContentType.Id == TypeContent.Signature)
                 .OrderByDescending(x => x.ChangeDate)
                 .Skip(1);

      foreach (var content in signature)
      {
        session.Delete(content);
      }
    }

    /// <summary>
    /// The save emloyment histry.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    private void SaveEmloymentHistry(Statement statement)
    {
      if (statement.InsuredPersonData.Category == null)
      {
        return;
      }

      EmploymentHistory hist = null;
      if (statement.InsuredPerson.EmploymentHistories != null)
      {
        hist =
          statement.InsuredPerson.EmploymentHistories.FirstOrDefault(
                                                                     x =>
                                                                     x.SourceType.Id
                                                                     == EmploymentSourceType.EmploymentSourceType2);
      }

      if (hist == null)
      {
        hist = new EmploymentHistory
               {
                 InsuredPerson = statement.InsuredPerson, 
                 SourceType =
                   ObjectFactory.GetInstance<IConceptCacheManager>()
                                .GetById(EmploymentSourceType.EmploymentSourceType2)
               };
      }

      hist.Employment = CategoryPerson.IsWorking(statement.InsuredPersonData.Category.Id);

      var dt = statement.DateFiling.HasValue
                 ? statement.DateFiling.Value
                 : statement.DateIssuePolisCertificate.HasValue
                     ? statement.DateIssuePolisCertificate.Value
                     : statement.DateIssueTemporaryCertificate.HasValue
                         ? statement.DateIssueTemporaryCertificate.Value
                         : DateTime.Now;

      hist.Period = periodManager.GetPeriodByMonth(dt);

      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().SaveOrUpdate(hist);
    }

    /// <summary>
    /// The save relation data.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    private void SaveRelationData(Statement statement)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      // ���������� ���������� ����������
      if (statement.ContactInfo != null)
      {
        session.SaveOrUpdate(statement.ContactInfo);
      }

      // ���������� ������ �����������  
      if (statement.Address != null)
      {
        session.SaveOrUpdate(statement.Address);
      }

      // ���������� ������ ����������  
      if (statement.Address2 != null)
      {
        session.SaveOrUpdate(statement.Address2);
      }

      // ���������� InsuredPerson
      if (statement.InsuredPerson != null)
      {
        session.SaveOrUpdate(statement.InsuredPerson);
      }

      // ���������� InsuredPersonData
      if (statement.InsuredPersonData != null)
      {
        session.SaveOrUpdate(statement.InsuredPersonData);

        // ���������� Contents
        if (statement.InsuredPersonData.Contents != null)
        {
          SaveContents(statement);
        }
      }

      // ���������� ��������� ���
      if (statement.DocumentUdl != null)
      {
        session.SaveOrUpdate(statement.DocumentUdl);
      }

      // ���������� ��������� ��������������� �����������
      if (statement.DocumentRegistration != null)
      {
        session.SaveOrUpdate(statement.DocumentRegistration);
      }

      // ���������� �������������
      if (statement.Representative != null)
      {
        // ���������� ��������� �������������
        if (statement.Representative.Document != null)
        {
          session.SaveOrUpdate(statement.Representative.Document);
        }

        session.SaveOrUpdate(statement.Representative);
      }

      // ���������� ��������� 
      if (statement.ResidencyDocument != null)
      {
        session.SaveOrUpdate(statement.ResidencyDocument);
      }

      if (statement.MedicalInsurances != null)
      {
        foreach (var medicalInsurance in statement.MedicalInsurances)
        {
          session.SaveOrUpdate(medicalInsurance);
        }
      }
    }

    /// <summary>
    /// The set auto property.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="pdpCode">
    /// The pdp Code.
    /// </param>
    private void SetAutoProperty(Statement statement, string pdpCode)
    {
      if (statement.DateFiling.HasValue)
      {
        statement.DateFiling = statement.DateFiling.Value.Date.Add(DateTime.Now.TimeOfDay);
      }

      // Status
      if (statement.Status == null)
      {
        statement.Status = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(StatusStatement.New);
      }
      else
      {
        if (statement.Status.Id == StatusStatement.Cancelled || statement.Status.Id == StatusStatement.Declined)
        {
          statement.Status = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(StatusStatement.New);
        }

        if (statement.Status.Id == StatusStatement.New)
        {
          statement.IsExportTemp = false;
          statement.IsExportPolis = false;
        }
      }

      // ���� �����
      TrimStatementData(statement);

      // ����������� ������
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      if (statement.UserId == null)
      {
        statement.UserId = currentUser.Id;
      }

      // ���������� ���
      if (statement.CauseFiling != null && statement.CauseFiling.Id != CauseReinsurance.Initialization)
      {
        if (statement.PointDistributionPolicy == null && currentUser != null
            && currentUser.PointDistributionPolicyId != null)
        {
          statement.PointDistributionPolicy =
            ObjectFactory.GetInstance<IOrganisationCacheManager>().GetById(currentUser.PointDistributionPolicyId.Value);
        }
      }

      // ����������� ������ �������
      if (statement.InsuredPerson != null && statement.InsuredPerson.Status == null)
      {
        statement.InsuredPerson.Status = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(StatusPerson.Active);
      }

      if (statement.MedicalInsurances != null)
      {
        foreach (var medicalInsurance in statement.MedicalInsurances.Where(x => x.Smo == null))
        {
          medicalInsurance.Smo = statement.PointDistributionPolicy.Parent;
        }
      }
    }

    /// <summary>
    /// ������� ��� ������� �� ����� � ��������� ������ ������ ���� ������ ������ ���������� ���� ��� ����
    /// </summary>
    /// <param name="value">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string TrimInside(string value)
    {
      if (!string.IsNullOrEmpty(value))
      {
        value = value.Trim();
        var last = value.Split(' ');
        last = last.Where(x => !string.IsNullOrEmpty(x)).Select(x => x.PadRight(1, ' ')).ToArray();
        return string.Join(" ", last).Trim();
      }

      return string.Empty;
    }

    /// <summary>
    /// ������� ��� ������� �� ����� � ��������� ������ ������ ���� ������ ������ ���������� ���� ��� ���� ��� ����� ������ �
    ///   ��� �����
    /// </summary>
    /// <param name="document">
    /// </param>
    private void TrimInsideDocument(Document document)
    {
      if (document == null)
      {
        return;
      }

      document.IssuingAuthority = TrimInside(document.IssuingAuthority);
      document.Number = TrimInside(document.Number);
      document.Series = TrimInside(document.Series);
    }

    #endregion
  }
}