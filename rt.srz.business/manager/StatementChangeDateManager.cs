// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementChangeDateManager.cs" company="РусБИТех">
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
    /// Реплицирует историю изменения приватных данных и данных документа
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    public void ReplicateStatementChangeHistory(Statement statement)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      Delete(x => x.Statement.Id == statement.Id);

      // запись новой истории
      foreach (var changeDate in statement.StatementChangeDates)
      {
        changeDate.Statement = statement;
        session.Save(changeDate);
      }
    }

    /// <summary>
    /// Сохраняет историю изменения приватных данных и данных документа
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

      // инкрементируем версию для нового заявления
      if (newStatement.Version == 0)
      {
        newStatement.Version++;
      }

      // Получаем предыдущее состояние заявления
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
          // ЕНП
          SaveHistoryRecord(newStatement, TypeFields.Enp, oldStatement.NumberPolicy);
        }

        if (newStatement.InsuredPersonData.FirstName != oldStatement.InsuredPersonData.FirstName)
        {
          // Имя
          SaveHistoryRecord(newStatement, TypeFields.FirstName, oldStatement.InsuredPersonData.FirstName);
        }

        if (newStatement.InsuredPersonData.LastName != oldStatement.InsuredPersonData.LastName)
        {
          // Фамилия
          SaveHistoryRecord(newStatement, TypeFields.LastName, oldStatement.InsuredPersonData.LastName);
        }

        if (newStatement.InsuredPersonData.MiddleName != oldStatement.InsuredPersonData.MiddleName)
        {
          // Отчество
          SaveHistoryRecord(newStatement, TypeFields.MiddleName, oldStatement.InsuredPersonData.MiddleName);
        }

        if (newStatement.InsuredPersonData.Birthday != oldStatement.InsuredPersonData.Birthday)
        {
          // Дата рождения
          SaveHistoryRecord(
                            newStatement, 
                            TypeFields.Birthday, 
                            oldStatement.InsuredPersonData.Birthday.Value.ToShortDateString());
        }

        if (newStatement.InsuredPersonData.Birthplace != oldStatement.InsuredPersonData.Birthplace)
        {
          // Место рождения
          SaveHistoryRecord(newStatement, TypeFields.Birthplace, oldStatement.InsuredPersonData.Birthplace);
        }

        if (newStatement.InsuredPersonData.Gender.Id != oldStatement.InsuredPersonData.Gender.Id)
        {
          // Пол
          SaveHistoryRecord(newStatement, TypeFields.GenderId, oldStatement.InsuredPersonData.Gender.Id.ToString());
        }

        if (newStatement.InsuredPersonData.Snils != oldStatement.InsuredPersonData.Snils)
        {
          // СНИЛС
          SaveHistoryRecord(newStatement, TypeFields.Snils, oldStatement.InsuredPersonData.Snils);
        }

        if (newStatement.DocumentUdl.DocumentType.Id != oldStatement.DocumentUdl.DocumentType.Id)
        {
          // Тип документа
          SaveHistoryRecord(
                            newStatement, 
                            TypeFields.DocumentTypeId, 
                            oldStatement.DocumentUdl.DocumentType.Id.ToString());
        }

        if (newStatement.DocumentUdl.Series != oldStatement.DocumentUdl.Series)
        {
          // Серия документа
          SaveHistoryRecord(newStatement, TypeFields.DocumentSeries, oldStatement.DocumentUdl.Series);
        }

        if (newStatement.DocumentUdl.Number != oldStatement.DocumentUdl.Number)
        {
          // Номер документа
          SaveHistoryRecord(newStatement, TypeFields.DocumentNumber, oldStatement.DocumentUdl.Number);
        }

        // Инкрементируем версию
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
    /// Создает и сохраняем запись об истории изменения приватных данных
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
      // Сессия
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      // Создаем запись
      var change = new StatementChangeDate();
      change.Statement = newStatement;
      change.Field = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(typeField);
      change.Version = newStatement.Version;

      // Возможны поля с нулевыми значениями
      if (string.IsNullOrEmpty(oldData))
      {
        oldData = string.Empty;
      }

      change.Datum = oldData;

      // Сохраняем в сессии
      session.Save(change);

      incrementVersion = true;
    }

    #endregion
  }
}