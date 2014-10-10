// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementChangeDateManager.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The StatementChangeDateManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;
  using System.Linq;

  using NHibernate;

  using rt.srz.business.manager.cache;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  ///   The StatementChangeDateManager.
  /// </summary>
  public partial class StatementChangeDateManager
  {
    #region Fields

    /// <summary>
    ///   The increment Version.
    /// </summary>
    private bool incrementVersion;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� ������� ��������� ��������� ������ � ������ ���������
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    public void ReplicateStatementChangeHistory(Statement statement)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      Delete(x => x.Statement.Id == statement.Id);

      // ������ ����� �������
      foreach (var changeDate in statement.StatementChangeDates)
      {
        changeDate.Statement = statement;
        session.Save(changeDate);
      }
    }

    /// <summary>
    /// ��������� ������� ��������� ��������� ������ � ������ ���������
    /// </summary>
    /// <param name="newStatement">
    /// The new Statement.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool SaveStatementChangeHistory(Statement newStatement)
    {
      if (newStatement == null || newStatement.Id == Guid.Empty)
      {
        return false;
      }

      // �������������� ������ ��� ������ ���������
      if (newStatement.Version == 0)
      {
        newStatement.Version++;
      }

      // �������� ���������� ��������� ���������
      Statement oldStatement = null;
      using (var historySession = ObjectFactory.GetInstance<ISessionFactory>().OpenSession())
      {
        oldStatement = historySession.QueryOver<Statement>().Where(x => x.Id == newStatement.Id).List().FirstOrDefault();

        if (oldStatement == null)
        {
          return false;
        }

        incrementVersion = false;
        if (newStatement.NumberPolicy != oldStatement.NumberPolicy)
        {
          // ���
          SaveHistoryRecord(newStatement, TypeFields.Enp, oldStatement.NumberPolicy);
        }

        if (newStatement.InsuredPersonData.FirstName != oldStatement.InsuredPersonData.FirstName)
        {
          // ���
          SaveHistoryRecord(newStatement, TypeFields.FirstName, oldStatement.InsuredPersonData.FirstName);
        }

        if (newStatement.InsuredPersonData.LastName != oldStatement.InsuredPersonData.LastName)
        {
          // �������
          SaveHistoryRecord(newStatement, TypeFields.LastName, oldStatement.InsuredPersonData.LastName);
        }

        if (newStatement.InsuredPersonData.MiddleName != oldStatement.InsuredPersonData.MiddleName)
        {
          // ��������
          SaveHistoryRecord(newStatement, TypeFields.MiddleName, oldStatement.InsuredPersonData.MiddleName);
        }

        if (newStatement.InsuredPersonData.Birthday != oldStatement.InsuredPersonData.Birthday)
        {
          // ���� ��������
          SaveHistoryRecord(
                            newStatement, 
                            TypeFields.Birthday, 
                            oldStatement.InsuredPersonData.Birthday.Value.ToShortDateString());
        }

        if (newStatement.InsuredPersonData.Birthplace != oldStatement.InsuredPersonData.Birthplace)
        {
          // ����� ��������
          SaveHistoryRecord(newStatement, TypeFields.Birthplace, oldStatement.InsuredPersonData.Birthplace);
        }

        if (newStatement.InsuredPersonData.Gender.Id != oldStatement.InsuredPersonData.Gender.Id)
        {
          // ���
          SaveHistoryRecord(newStatement, TypeFields.GenderId, oldStatement.InsuredPersonData.Gender.Id.ToString());
        }

        if (newStatement.InsuredPersonData.Snils != oldStatement.InsuredPersonData.Snils)
        {
          // �����
          SaveHistoryRecord(newStatement, TypeFields.Snils, oldStatement.InsuredPersonData.Snils);
        }

        if (newStatement.DocumentUdl.DocumentType.Id != oldStatement.DocumentUdl.DocumentType.Id)
        {
          // ��� ���������
          SaveHistoryRecord(
                            newStatement, 
                            TypeFields.DocumentTypeId, 
                            oldStatement.DocumentUdl.DocumentType.Id.ToString());
        }

        if (newStatement.DocumentUdl.Series != oldStatement.DocumentUdl.Series)
        {
          // ����� ���������
          SaveHistoryRecord(newStatement, TypeFields.DocumentSeries, oldStatement.DocumentUdl.Series);
        }

        if (newStatement.DocumentUdl.Number != oldStatement.DocumentUdl.Number)
        {
          // ����� ���������
          SaveHistoryRecord(newStatement, TypeFields.DocumentNumber, oldStatement.DocumentUdl.Number);
        }

        // �������������� ������
        if (incrementVersion)
        {
          newStatement.Version++;
        }

        return true;
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// ������� � ��������� ������ �� ������� ��������� ��������� ������
    /// </summary>
    /// <param name="newStatement">
    /// The new Statement.
    /// </param>
    /// <param name="typeField">
    /// The type Field.
    /// </param>
    /// <param name="oldData">
    /// The old Data.
    /// </param>
    private void SaveHistoryRecord(Statement newStatement, int typeField, string oldData)
    {
      // ������
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      // ������� ������
      var change = new StatementChangeDate();
      change.Statement = newStatement;
      change.Field = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(typeField);
      change.Version = newStatement.Version;

      // �������� ���� � �������� ����������
      if (string.IsNullOrEmpty(oldData))
      {
        oldData = string.Empty;
      }

      change.Datum = oldData;

      // ��������� � ������
      session.Save(change);

      incrementVersion = true;
    }

    #endregion
  }
}