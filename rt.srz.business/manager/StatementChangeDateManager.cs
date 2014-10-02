//-------------------------------------------------------------------------------------
// <copyright file="StatementChangeDateManager.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using StructureMap;
using rt.srz.model.srz;
using rt.srz.business.manager.cache;

namespace rt.srz.business.manager
{
  /// <summary>
  /// The StatementChangeDateManager.
  /// </summary>
  public partial class StatementChangeDateManager
  {
    #region Fields
    private bool incrementVersion = false;
    #endregion

    /// <summary>
    /// ��������� ������� ��������� ��������� ������ � ������ ���������
    /// </summary>
    /// <param name="newStatement"></param>
    /// <param name="oldStatement"></param>
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

      //�������� ���������� ��������� ���������
      Statement oldStatement = null;
      using (var historySession = ObjectFactory.GetInstance<ISessionFactory>().OpenSession())
      {
        oldStatement = historySession
          .QueryOver<Statement>()
          .Where(x => x.Id == newStatement.Id)
          .List()
          .FirstOrDefault();

        if (oldStatement == null)
        {
          return false;
        }

        incrementVersion = false;
        if (newStatement.NumberPolicy != oldStatement.NumberPolicy) // ���
        {
          SaveHistoryRecord(newStatement, TypeFields.Enp, oldStatement.NumberPolicy);
        }

        if (newStatement.InsuredPersonData.FirstName != oldStatement.InsuredPersonData.FirstName) // ���
        {
          SaveHistoryRecord(newStatement, TypeFields.FirstName, oldStatement.InsuredPersonData.FirstName);
        }

        if (newStatement.InsuredPersonData.LastName != oldStatement.InsuredPersonData.LastName) // �������
        {
          SaveHistoryRecord(newStatement, TypeFields.LastName, oldStatement.InsuredPersonData.LastName);
        }

        if (newStatement.InsuredPersonData.MiddleName != oldStatement.InsuredPersonData.MiddleName) // ��������
        {
          SaveHistoryRecord(newStatement, TypeFields.MiddleName, oldStatement.InsuredPersonData.MiddleName);
        }

        if (newStatement.InsuredPersonData.Birthday != oldStatement.InsuredPersonData.Birthday) // ���� ��������
        {
          SaveHistoryRecord(newStatement, TypeFields.Birthday, oldStatement.InsuredPersonData.Birthday.Value.ToShortDateString());
        }

        if (newStatement.InsuredPersonData.Birthplace != oldStatement.InsuredPersonData.Birthplace) // ����� ��������
        {
          SaveHistoryRecord(newStatement, TypeFields.Birthplace, oldStatement.InsuredPersonData.Birthplace);
        }

        if (newStatement.InsuredPersonData.Gender.Id != oldStatement.InsuredPersonData.Gender.Id) // ���
        {
          SaveHistoryRecord(newStatement, TypeFields.GenderId, oldStatement.InsuredPersonData.Gender.Id.ToString());
        }

        if (newStatement.InsuredPersonData.Snils != oldStatement.InsuredPersonData.Snils) // �����
        {
          SaveHistoryRecord(newStatement, TypeFields.Snils, oldStatement.InsuredPersonData.Snils);
        }

        if (newStatement.DocumentUdl.DocumentType.Id != oldStatement.DocumentUdl.DocumentType.Id) // ��� ���������
        {
          SaveHistoryRecord(newStatement, TypeFields.DocumentTypeId, oldStatement.DocumentUdl.DocumentType.Id.ToString());
        }

        if (newStatement.DocumentUdl.Series != oldStatement.DocumentUdl.Series) // ����� ���������
        {
          SaveHistoryRecord(newStatement, TypeFields.DocumentSeries, oldStatement.DocumentUdl.Series);
        }

        if (newStatement.DocumentUdl.Number != oldStatement.DocumentUdl.Number) // ����� ���������
        {
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

    /// <summary>
    /// ����������� ������� ��������� ��������� ������ � ������ ���������
    /// </summary>
    /// <param name="newStatement"></param>
    /// <param name="oldStatement"></param>
    public void ReplicateStatementChangeHistory(Statement statement)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      // 
      Delete(x => x.Statement.Id == statement.Id);

      // ������ ����� �������
      foreach (var changeDate in statement.StatementChangeDates)
      {
        changeDate.Statement = statement;
        session.Save(changeDate);
      }
    }

    /// <summary>
    /// ������� � ��������� ������ �� ������� ��������� ��������� ������
    /// </summary>
    /// <param name="newStatement"></param>
    /// <param name="typeField"></param>
    /// <param name="oldData"></param>
    private void SaveHistoryRecord(Statement newStatement, int typeField, string oldData)
    {
      // ������
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      // ������� ������
      StatementChangeDate change = new StatementChangeDate();
      change.Statement = newStatement;
      change.Field = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(typeField);
      change.Version = newStatement.Version;

      // �������� ���� � �������� ����������
      if (string.IsNullOrEmpty(oldData))
        oldData = string.Empty;
      change.Datum = oldData;

      // ��������� � ������
      session.Save(change);

      incrementVersion = true;
    }
  }
}